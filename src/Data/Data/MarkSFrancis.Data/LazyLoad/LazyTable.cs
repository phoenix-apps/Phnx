using System;
using System.Collections.Concurrent;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A lazy loaded table
    /// </summary>
    /// <typeparam name="TKey">The type of identifier for this entry</typeparam>
    /// <typeparam name="TEntry">The type of data in this table</typeparam>
    public class LazyData<TKey, TEntry> where TEntry : IPrimaryKeyDataModel<TKey>
    {
        private readonly ConcurrentDictionary<TKey, TEntry> _cache;

        private readonly Func<TKey, TEntry> _getSingleFromTable;

        /// <summary>
        /// The number of items cached
        /// </summary>
        public int CachedCount => _cache.Count;

        /// <summary>
        /// Create a new <see cref="LazyData{TKey,TEntry}"/>
        /// </summary>
        /// <param name="getSingleFromDatabase">The method to use when getting an item for the first time from the database</param>
        public LazyData(Func<TKey, TEntry> getSingleFromDatabase)
        {
            _getSingleFromTable = getSingleFromDatabase;
            _cache = new ConcurrentDictionary<TKey, TEntry>();
        }

        /// <summary>
        /// Add or update an entry in the cache
        /// </summary>
        /// <param name="entry">The entry to add or update</param>
        public void AddOrUpdate(TEntry entry)
        {
            _cache.AddOrUpdate(entry.Id, entry, (oldVal, newVal) => entry);
        }

        /// <summary>
        /// Get an entry from the cache, loading it if it does not exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntry Get(TKey id)
        {
            return _cache.GetOrAdd(id, _getSingleFromTable);
        }

        /// <summary>
        /// Remove an entry from the cache
        /// </summary>
        /// <param name="id"></param>
        public void Remove(TKey id)
        {
            _cache.TryRemove(id, out _);
        }

        /// <summary>
        /// Clear the cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }
    }
}