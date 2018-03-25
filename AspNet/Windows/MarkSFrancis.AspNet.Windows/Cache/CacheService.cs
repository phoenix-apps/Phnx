using System.Runtime.Caching;
using MarkSFrancis.AspNet.Windows.Cache.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Cache
{
    public class CacheService : ICacheService
    {
        protected ObjectCache Cache { get; }

        public CacheService()
        {
            Cache = MemoryCache.Default;
        }

        public CacheService(ObjectCache cacheToUse)
        {
            Cache = cacheToUse;
        }

        /// <summary>
        /// Get a value from the cache, or load it from the external source if it is not yet loaded into the cache
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="entry">The cache entry containing the loading configuration</param>
        /// <returns>The loaded value</returns>
        public T Get<T>(ICacheEntry<T> entry)
        {
            if (!Cache.Contains(entry.Key))
            {
                // Lazy load
                return Load(entry);
            }

            return (T)Cache[entry.Key];
        }

        /// <summary>
        /// Load a value into the cache
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="entry">The cache entry containing the loading configuration</param>
        /// <returns>The loaded value</returns>
        public T Load<T>(ICacheEntry<T> entry)
        {
            T value = entry.LoadFromExternalSource();

            return Set(entry, value);
        }

        /// <summary>
        /// Set a value to the cache
        /// </summary>
        /// <typeparam name="T">The type of value to set</typeparam>
        /// <param name="entry">The cache entry containing the loading configuration</param>
        /// <param name="value">The value to set</param>
        /// <returns>The value set to the cache</returns>
        protected T Set<T>(ICacheEntry<T> entry, T value)
        {
            Cache.Add(entry.Key, value, entry.Policy);

            return value;
        }
    }
}
