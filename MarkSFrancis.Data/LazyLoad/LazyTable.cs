using System;
using System.Collections.Concurrent;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A lazy loaded table
    /// </summary>
    /// <typeparam name="TKey">The type of identifier for this entry</typeparam>
    /// <typeparam name="TEntry">The type of data in this table</typeparam>
    public class LazyTable<TKey, TEntry> where TEntry : IPrimaryKeyDataModel<TKey>
    {
        /// <summary>
        /// The name of the lazy loaded table
        /// </summary>
        public string UniqueTableName { get; }

        private readonly ConcurrentDictionary<TKey, TEntry> _cache;

        private readonly Func<TKey, TEntry> _getSingleFromDatabase;

        /// <summary>
        /// The number of items cached
        /// </summary>
        public int CachedCount => _cache.Count;

        /// <summary>
        /// Create a new <see cref="LazyTable{TId,TEntry}"/>
        /// </summary>
        /// <param name="uniqueTableName">The name of this table</param>
        /// <param name="getSingleFromDatabase">The method to use when getting an item for the first time from the database</param>
        public LazyTable(string uniqueTableName, Func<TKey, TEntry> getSingleFromDatabase)
        {
            UniqueTableName = uniqueTableName;
            _getSingleFromDatabase = getSingleFromDatabase;
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
            return _cache.GetOrAdd(id, _getSingleFromDatabase);
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

        /// <summary>
        /// Get the hash code for this <see cref="object"/>. Based on the <see cref="UniqueTableName"/>
        /// </summary>
        /// <returns>A hash code for this <see cref="object"/></returns>
        public override int GetHashCode()
        {
            return UniqueTableName.GetHashCode();
        }

        /// <summary>
        /// Check whether this item matches another. Based on the <see cref="UniqueTableName"/>. Returns <see langword="false"/> if the object being compared to is not a <see cref="LazyTable{TKey,TEntry}"/>
        /// </summary>
        /// <returns>Whether this matches another <see cref="object"/></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is LazyTable<TKey, TEntry> table))
            {
                return false;
            }

            return table.UniqueTableName == UniqueTableName;
        }
    }
}