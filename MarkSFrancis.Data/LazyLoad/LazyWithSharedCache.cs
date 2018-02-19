using System;
using System.Collections.Generic;

namespace MarkSFrancis.Data.LazyLoad
{
    public static class LazyWithSharedCache
    {
        private static Dictionary<Type, Dictionary<object, object>> CacheManager { get; set; }
            = new Dictionary<Type, Dictionary<object, object>>();

        private static readonly object CacheLock = new object();

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
        /// Returns the value if it's in cache, 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="getFunc"></param>
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

        public static int TotalItemsCachedForType(Type type)
        {
            lock (CacheLock)
            {
                return CacheManager.TryGetValue(type, out var typeDictionary) ? typeDictionary.Count : 0;
            }
        }

        public static void ClearAllCache()
        {
            lock (CacheLock)
            {
                CacheManager = new Dictionary<Type, Dictionary<object, object>>();
                CacheIsMaxSize = false;
            }
        }

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