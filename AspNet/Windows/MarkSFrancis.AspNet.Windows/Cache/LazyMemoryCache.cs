using MarkSFrancis.AspNet.Windows.Cache.Interfaces;
using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Cache
{
    /// <summary>
    /// A memory cache with lazy loading features
    /// </summary>
    public class LazyMemoryCache : ILazyMemoryCache
    {
        /// <summary>
        /// The cache used by this <see cref="LazyMemoryCache"/>, where values are cached after loading
        /// </summary>
        protected ObjectCache Cache { get; }

        /// <summary>
        /// Create a new <see cref="LazyMemoryCache"/> using <see cref="MemoryCache.Default"/>
        /// </summary>
        public LazyMemoryCache()
        {
            Cache = MemoryCache.Default;
        }

        /// <summary>
        /// Create a new <see cref="LazyMemoryCache"/> using an <see cref="ObjectCache"/> for the cache storage
        /// </summary>
        /// <param name="cache">The cache to load from and to</param>
        public LazyMemoryCache(ObjectCache cache)
        {
            Cache = cache;
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
