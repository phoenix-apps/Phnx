using System;
using System.Collections.Generic;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A three-dimensional cache that can store up to an entire database including all its tables using each type and key to get each value from the cache
    /// </summary>
    public static class LazyDatabase
    {
        private static Dictionary<Type, Dictionary<object, object>> CacheManager { get; set; }
            = new Dictionary<Type, Dictionary<object, object>>();

        private static readonly object CacheLock = new object();

        /// <summary>
        /// The total number of types stored in the cache
        /// </summary>
        public static int TotalTypesCached
        {
            get
            {
                lock (CacheLock)
                {
                    return CacheManager.Count;
                }
            }
        }

        /// <summary>
        /// The total number of values across all types stored in the cache
        /// </summary>
        public static int TotalValuesCached
        {
            get
            {
                lock (CacheLock)
                {
                    int total = 0;
                    foreach (var keyValuePair in CacheManager)
                    {
                        total += keyValuePair.Value.Count;
                    }
                    return total;
                }
            }
        }

        private static bool CacheIsMaxSize { get; set; }
        
        /// <summary>
        /// Get a value from the cache using a unique identifier
        /// </summary>
        /// <typeparam name="TKey">The type of the unique identifier for this value</typeparam>
        /// <typeparam name="TObject">The type of value to load from the cache</typeparam>
        /// <param name="id">The unique identifier for this value</param>
        /// <param name="getFunc">The method to call if the value is not already in the cache</param>
        /// <returns></returns>
        public static TObject GetValue<TKey, TObject>(TKey id, Func<TKey, TObject> getFunc)
        {
            lock (CacheLock)
            {
                if (!TryGetFromCache(id, out TObject bufferT))
                {
                    bufferT = getFunc(id);
                    AddToCache(id, bufferT);
                }

                return bufferT;
            }
        }

        /// <summary>
        /// Gets the total number of values cached for a type
        /// </summary>
        /// <param name="type">The type to get the number of values cached for</param>
        /// <returns></returns>
        public static int TotalValuesCachedForType(Type type)
        {
            lock (CacheLock)
            {
                return CacheManager.TryGetValue(type, out var typeDictionary) ? typeDictionary.Count : 0;
            }
        }

        /// <summary>
        /// Clear all values from the cache, forcing all new requests to be reloaded into the cache
        /// </summary>
        public static void ClearAllCache()
        {
            lock (CacheLock)
            {
                CacheManager = new Dictionary<Type, Dictionary<object, object>>();
                CacheIsMaxSize = false;
            }
        }

        /// <summary>
        /// Clear all values from the cache for a type, forcing all new requests for that type to be reloaded into the cache when their values are requested
        /// </summary>
        /// <param name="typeToClear"></param>
        public static void ClearAllCache(Type typeToClear)
        {
            lock (CacheLock)
            {
                if (CacheManager.ContainsKey(typeToClear))
                {
                    CacheManager.Remove(typeToClear);
                    CacheIsMaxSize = false;
                }
            }
        }

        private static void AddToCache<TKey, TObject>(TKey id, TObject bufferT)
        {
            if (CacheIsMaxSize)
            {
                return;
            }

            try
            {
                Type tType = typeof(TObject);

                lock (CacheLock)
                {
                    if (!CacheManager.TryGetValue(tType, out var outType))
                    {
                        outType = new Dictionary<object, object>();
                        CacheManager.Add(tType, outType);
                    }

                    if (!outType.TryGetValue(id, out _))
                    {
                        outType.Add(id, bufferT);
                    }
                }
                // Else already in cache
            }
            catch (OutOfMemoryException)
            {
                // don't add to cache as cache is max size
                CacheIsMaxSize = true;
            }
        }

        private static bool TryGetFromCache<TKey, TObject>(TKey id, out TObject bufferT)
        {
            object buffer;
            Type tType = typeof(TObject);

            lock (CacheLock)
            {
                if (!CacheManager.TryGetValue(tType, out var outType))
                {
                    bufferT = default(TObject);
                    return false;
                }

                if (!outType.TryGetValue(id, out buffer))
                {
                    bufferT = default(TObject);
                    return false;
                }
            }

            bufferT = (TObject)buffer;
            return true;
        }
    }
}