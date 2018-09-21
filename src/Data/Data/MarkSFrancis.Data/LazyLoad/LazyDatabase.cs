using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A thread-safe lazy loaded database, with tables seperated by model types
    /// </summary>
    public class LazyDatabase
    {
        private readonly ConcurrentDictionary<Type, LazyDictionary<object, object>> _cache;

        public LazyDatabase()
        {
            EntriesDefaultMaximumLifetime = null;
            _cache = new ConcurrentDictionary<Type, LazyDictionary<object, object>>();
        }

        public LazyDatabase(TimeSpan globalLifetime)
        {
            EntriesDefaultMaximumLifetime = globalLifetime;
            _cache = new ConcurrentDictionary<Type, LazyDictionary<object, object>>();
        }

        /// <summary>
        /// Get the total number of tables that have been cached
        /// </summary>
        public int TotalTablesCount => _cache.Count;

        /// <summary>
        /// The default maximum lifetime for entries in the cache. This is <see langword="null"/> if there is no default maximum lifetime
        /// </summary>
        public TimeSpan? EntriesDefaultMaximumLifetime { get; }

        /// <summary>
        /// Whether this <see cref="LazyDictionary{TKey, TEntry}"/> has been set up with an auto-expiration by default for entries
        /// </summary>
        public bool EntriesHaveDefaultMaximumLifetime => EntriesDefaultMaximumLifetime.HasValue;

        /// <summary>
        /// Add a lazy loading table to the dictionary if it is not already loaded
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="getFromExternalSource"></param>
        /// <returns>Whether the table was added to the cache database</returns>
        public bool TryAddTable<TKey, TValue>(Func<TKey, TValue> getFromExternalSource, TimeSpan overrideLifetime)
        {
            var newTable = new LazyDictionary<object, object>(key => getFromExternalSource((TKey)key));
            return _cache.TryAdd(typeof(TValue), newTable);
        }

        private bool TryGetTable<T>(out LazyDictionary<object, object> table)
        {
            return _cache.TryGetValue(typeof(T), out table);
        }

        /// <summary>
        /// Lazy load an entry from the cache
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to load</typeparam>
        /// <typeparam name="TEntry">The type of entry to load</typeparam>
        /// <param name="id">The id of the entry to load</param>
        /// <param name="load">The method to call if the value is not already in the cache</param>
        /// <returns>The lazy loaded value with the primary key of <paramref name="id"/></returns>
        public TEntry Get<TKey, TEntry>(TKey id, Func<TKey, TEntry> load)
        {
            lock (_syncContext)
            {
                var table = GetTable<TEntry>();

                return (TEntry)table.GetOrAdd(id, load);
            }
        }

        /// <summary>
        /// Add or update an entry in the cache
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to add or update</typeparam>
        /// <typeparam name="TEntry">The type of entry to add or update</typeparam>
        /// <param name="id">The id of the entry to add or update</param>
        /// <param name="value">The value to add or update</param>
        public void AddOrUpdate<TKey, TEntry>(TKey id, TEntry value)
        {
            lock (_syncContext)
            {
                var table = GetTable<TEntry>(true);

                table.AddOrUpdate(id, value, (a, b) => value);
            }
        }

        /// <summary>
        /// Remove an entry from the cache
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to remove</typeparam>
        /// <typeparam name="TEntry">The type of entry to remove</typeparam>
        /// <param name="id">The id of the entry to remove</param>
        public void Remove<TKey, TEntry>(TKey id)
        {
            lock (_syncContext)
            {
                var table = GetTable<TEntry>(false);

                if (table == null)
                {
                    return;
                }

                table.TryRemove(id, out _);
            }
        }

        /// <summary>
        /// Clear a table from the cache
        /// </summary>
        /// <typeparam name="T">The type of the table to clear</typeparam>
        public void Clear<T>()
        {
            lock (_syncContext)
            {
                _cache.TryRemove(typeof(T), out _);
            }
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public void Clear()
        {
            lock (_syncContext)
            {
                _cache.Clear();
            }
        }

        /// <summary>
        /// Get the number of items cached in a specific table
        /// </summary>
        /// <typeparam name="T">The type of the table to get the total items cached in</typeparam>
        /// <returns></returns>
        public int TableItemsCachedCount<T>()
        {
            lock (_syncContext)
            {
                var table = GetTable<T>(false);

                if (table == null)
                {
                    return 0;
                }

                return table.Count;
            }
        }
    }
}
