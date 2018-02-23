using System;
using System.Collections.Generic;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A three-dimensional cache that can store up to an entire database including all its tables using each type and key to get each item from the cache
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
        /// The total number of records across all types stored in the cache
        /// </summary>
        public static int TotalItemsCached
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
        /// Get a given value from the cache
        /// </summary>
        /// <typeparam name="TKey">The type of the unique identifier for this item</typeparam>
        /// <typeparam name="TObject">The type of object to load from the cache</typeparam>
        /// <param name="id">The unique identifier for this item</param>
        /// <param name="getFunc">The method to call if the given item is not already in the cache</param>
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
        /// Gets the total number of items cached for this given type
        /// </summary>
        /// <param name="type">The type to get the number of records cached for</param>
        /// <returns></returns>
        public static int TotalItemsCachedForType(Type type)
        {
            lock (CacheLock)
            {
                return CacheManager.TryGetValue(type, out var typeDictionary) ? typeDictionary.Count : 0;
            }
        }

        /// <summary>
        /// Clear all items from the cache, forcing all new requests to be reloaded into the cache
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
        /// Clear all items from the cache for a given type, forcing all new requests for that type to be reloaded into the cache
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