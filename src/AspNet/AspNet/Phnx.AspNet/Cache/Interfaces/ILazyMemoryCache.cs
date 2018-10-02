namespace Phnx.AspNet.Cache.Interfaces
{
    /// <summary>
    /// A memory cache with lazy loading features
    /// </summary>
    public interface ILazyMemoryCache
    {
        /// <summary>
        /// Get a value from the cache, or load it from the external source if it is not yet loaded into the cache
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="entry">The cache entry containing the loading configuration</param>
        /// <returns>The loaded value</returns>
        T Get<T>(ICacheEntry<T> entry);

        /// <summary>
        /// Load a value into the cache
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="entry">The cache entry containing the loading configuration</param>
        /// <returns>The loaded value</returns>
        T Load<T>(ICacheEntry<T> entry);
    }
}
