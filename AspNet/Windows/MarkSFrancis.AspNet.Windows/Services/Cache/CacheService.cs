using System;
using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Services.Cache
{
    public class CacheService
    {
        protected ObjectCache SiteCache { get; }

        public CacheService()
        {
            SiteCache = MemoryCache.Default;
        }

        public CacheService(ObjectCache cacheToUse)
        {
            SiteCache = cacheToUse;
        }

        public T Get<T>(ICacheEntry<T> entry)
        {
            if (!SiteCache.Contains(entry.Key))
            {
                // Lazy load
                return Load(entry);
            }

            return (T)SiteCache[entry.Key];
        }

        public T Get<TGroup, TChild, T>(TGroup group, Func<TGroup, TChild> childSelector)
            where TGroup : ICacheEntryGroup where TChild : ICacheEntryGroupChild<T>
        {
            var entry = childSelector(group);
            if (!SiteCache.Contains(entry.Key))
            {
                Load(group);
            }

            return (T)SiteCache[entry.Key];
        }

        public T Load<T>(ICacheEntry<T> entry)
        {
            T value = entry.LoadFromExternalSource();

            return Set(entry, value);
        }

        public void Load(ICacheEntryGroup group)
        {
            var loaded = group.LoadAllFromExternalSource();

            foreach (var childLoaded in loaded)
            {
                Set(childLoaded.Key, childLoaded.Value);
            }
        }

        private T Set<T>(ICacheEntry<T> entry, T value)
        {
            SiteCache.Add(entry.Key, value, entry.Policy);

            return value;
        }

        private void Set(ICacheEntryGroupChild entry, object value)
        {
            SiteCache.Add(entry.Key, value, entry.Policy);
        }
    }
}
