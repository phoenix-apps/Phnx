using System;
using System.Collections.Concurrent;

namespace Phnx.Data.Lazy
{
    /// <summary>
    /// A thread-safe lazy loaded database, with tables seperated by model types
    /// </summary>
    public class LazyDatabase
    {
        private readonly ConcurrentDictionary<Type, LazyDictionary<object, object>> _cache;

        /// <summary>
        /// Create a new <see cref="LazyDatabase"/> without a default cache lifetime policy
        /// </summary>
        public LazyDatabase()
        {
            EntriesDefaultMaximumLifetime = null;
            _cache = new ConcurrentDictionary<Type, LazyDictionary<object, object>>();
        }

        /// <summary>
        /// Create a new <see cref="LazyDatabase"/> with a default cache lifetime policy
        /// </summary>
        /// <param name="globalLifetime"></param>
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
        public bool TryAddTable<TKey, TValue>(Func<TKey, TValue> getFromExternalSource)
        {
            if (getFromExternalSource is null)
            {
                throw new ArgumentNullException(nameof(getFromExternalSource));
            }

            LazyDictionary<object, object> newTable;

            if (EntriesHaveDefaultMaximumLifetime)
            {
                newTable = new LazyDictionary<object, object>(key => getFromExternalSource((TKey)key), EntriesDefaultMaximumLifetime.Value);
            }
            else
            {
                newTable = new LazyDictionary<object, object>(key => getFromExternalSource((TKey)key));
            }

            return _cache.TryAdd(typeof(TValue), newTable);
        }

        /// <summary>
        /// Add a lazy loading table to the dictionary if it is not already loaded
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="getFromExternalSource"></param>
        /// <param name="overrideLifetime">The new lifetime for objects in this table only. This overrides <see cref="EntriesDefaultMaximumLifetime"/>. If this is <see langword="null"/>, this table's entries are set to never expire</param>
        /// <returns>Whether the table was added to the cache database</returns>
        public bool TryAddTable<TKey, TValue>(Func<TKey, TValue> getFromExternalSource, TimeSpan? overrideLifetime)
        {
            LazyDictionary<object, object> newTable;

            if (overrideLifetime.HasValue)
            {
                newTable = new LazyDictionary<object, object>(key => getFromExternalSource((TKey)key), overrideLifetime.Value);
            }
            else
            {
                newTable = new LazyDictionary<object, object>(key => getFromExternalSource((TKey)key));
            }

            return _cache.TryAdd(typeof(TValue), newTable);
        }

        private bool TryGetTable<T>(out LazyDictionary<object, object> table)
        {
            return _cache.TryGetValue(typeof(T), out table);
        }

        /// <summary>
        /// Lazy load an entry from the cache. Returns <see langword="false"/> if the table for <typeparamref name="TEntry"/> is not configured
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to load</typeparam>
        /// <typeparam name="TEntry">The type of entry to load</typeparam>
        /// <param name="id">The id of the entry to load</param>
        /// <param name="value">The value that was loaded</param>
        /// <returns>Whether the value was successfully loaded. This is <see langword="false"/> if the table for <typeparamref name="TEntry"/> is not configured</returns>
        public bool TryGet<TKey, TEntry>(TKey id, out TEntry value)
        {
            if (!TryGetTable<TEntry>(out var table))
            {
                value = default;
                return false;
            }

            value = (TEntry)table.Get(id);
            return true;
        }

        /// <summary>
        /// Lazy load an entry from the cache, configuring the cache table if it's not already configured
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to load</typeparam>
        /// <typeparam name="TEntry">The type of entry to load</typeparam>
        /// <param name="id">The id of the entry to load</param>
        /// <param name="getFromExternalSource">The method to call if the value is not already in the cache</param>
        /// <returns>The lazy loaded value with the primary key of <paramref name="id"/></returns>
        public TEntry Get<TKey, TEntry>(TKey id, Func<TKey, TEntry> getFromExternalSource)
        {
            if (!TryGetTable<TEntry>(out var table))
            {
                if (!TryAddTable(getFromExternalSource) || !TryGetTable<TEntry>(out table))
                {
                    // Table was created on a backing thread. Don't cache the value, just load it directly from the external source
                    return getFromExternalSource(id);
                }
            }

            return (TEntry)table.Get(id);
        }

        /// <summary>
        /// Add or update an entry in the cache
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to add or update</typeparam>
        /// <typeparam name="TEntry">The type of entry to add or update</typeparam>
        /// <param name="id">The id of the entry to add or update</param>
        /// <param name="value">The value to add or update</param>
        public bool TryAddOrUpdate<TKey, TEntry>(TKey id, TEntry value)
        {
            if (!TryGetTable<TEntry>(out var table))
            {
                return false;
            }

            table.AddOrUpdate(id, value);
            return true;
        }

        /// <summary>
        /// Add or update an entry in the cache, configuring the cache table if it's not already configured
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to add or update</typeparam>
        /// <typeparam name="TEntry">The type of entry to add or update</typeparam>
        /// <param name="id">The id of the entry to add or update</param>
        /// <param name="value">The value to add or update</param>
        /// <param name="getFromExternalSource">The method to use to load values from an external source. This is only used if no items have been loaded from this table before, and the table has not been added using <see cref="TryAddTable{TKey, TValue}(Func{TKey, TValue})"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="getFromExternalSource"/> is <see langword="null"/></exception>
        public void AddOrUpdate<TKey, TEntry>(TKey id, TEntry value, Func<TKey, TEntry> getFromExternalSource)
        {
            if (getFromExternalSource is null)
            {
                throw new ArgumentNullException(nameof(getFromExternalSource));
            }

            if (!TryGetTable<TEntry>(out var table))
            {
                TryAddTable(getFromExternalSource);

                if (!TryGetTable<TEntry>(out table))
                {
                    // Threading fuckery, fuck it
                    return;
                }
            }

            table.AddOrUpdate(id, value);
        }

        /// <summary>
        /// Try to remove a table from the cache
        /// </summary>
        /// <typeparam name="TEntry">The type of entries in the table to remove</typeparam>
        /// <returns>Whether the table was removed from the cache</returns>
        public bool TryRemoveTable<TEntry>()
        {
            return _cache.TryRemove(typeof(TEntry), out _);
        }

        /// <summary>
        /// Remove an entry from the cache
        /// </summary>
        /// <typeparam name="TKey">The type of id for the object to remove</typeparam>
        /// <typeparam name="TEntry">The type of entry to remove</typeparam>
        /// <param name="id">The id of the entry to remove</param>
        /// <returns>Whether the record was removed from the cache</returns>
        public bool TryRemove<TKey, TEntry>(TKey id)
        {
            if (!TryGetTable<TEntry>(out var table))
            {
                return false;
            }

            return table.TryRemove(id);
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        /// <summary>
        /// Get the number of items cached in a specific table
        /// </summary>
        /// <typeparam name="T">The type of the table to get the total items cached in</typeparam>
        /// <returns>The number of items cached in a specific table. Returns 0 if the cache is not configured</returns>
        public int TableItemsCachedCount<T>()
        {
            if (!TryGetTable<T>(out var table))
            {
                return 0;
            }

            return table.CachedCount;
        }
    }
}
