using System;
using System.Collections.Concurrent;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// A lazy loaded dictionary, which is thread-safe
    /// </summary>
    /// <typeparam name="TKey">The type of identifier for this value</typeparam>
    /// <typeparam name="TValue">The type of data in this table</typeparam>
    public class LazyDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, CachedItem<TValue>> _cache;

        private readonly Func<TKey, TValue> _getFromExternalSource;

        /// <summary>
        /// The maximum lifetime for entries in the cache. This is <see langword="null"/> if there is no maximum lifetime
        /// </summary>
        public TimeSpan? ValuesMaximumLifetime { get; }

        /// <summary>
        /// Whether this <see cref="LazyDictionary{TKey, TValue}"/> has been set up with an auto-expiration for entries
        /// </summary>
        public bool ValuesHaveMaximumLifetime => ValuesMaximumLifetime.HasValue;

        /// <summary>
        /// The number of items cached. This includes expired records if a timeout has been set
        /// </summary>
        public int CachedCount => _cache.Count;

        /// <summary>
        /// Create a new <see cref="LazyDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="getFromExternalSource">The method to use when loading an item from an external source. This is called only once per value, when the value is first loaded</param>
        public LazyDictionary(Func<TKey, TValue> getFromExternalSource)
        {
            _getFromExternalSource = getFromExternalSource ?? throw new ArgumentNullException(nameof(getFromExternalSource));

            ValuesMaximumLifetime = null;
            _cache = new ConcurrentDictionary<TKey, CachedItem<TValue>>();
        }

        /// <summary>
        /// Create a new <see cref="LazyDictionary{TKey,TValue}"/> with a maximum lifetime for any entries
        /// </summary>
        /// <param name="getFromExternalSource">The method to use when getting an itemfrom an external source. This is called once per value when the value is first loaded, and when the value is loaded, but has expired</param>
        /// <param name="cacheLifetime">The maximum lifetime for any record in the cache. If a record is older than the maximum lifetime, it is automatically reloaded from the external source next time it is requested</param>
        public LazyDictionary(Func<TKey, TValue> getFromExternalSource, TimeSpan cacheLifetime)
        {
            _getFromExternalSource = getFromExternalSource ?? throw new ArgumentNullException(nameof(getFromExternalSource));

            ValuesMaximumLifetime = cacheLifetime;
            _cache = new ConcurrentDictionary<TKey, CachedItem<TValue>>();
        }

        /// <summary>
        /// Add or update an value in the cache
        /// </summary>
        /// <param name="key">The key to the value to add or update</param>
        /// <param name="value">The value to add or update</param>
        public void AddOrUpdate(TKey key, TValue value)
        {
            var cachedValue = new CachedItem<TValue>(value);

            _cache.AddOrUpdate(key, cachedValue, (_, old) => cachedValue);
        }

        /// <summary>
        /// Get the value which belongs to <paramref name="key"/> from the cache, or loads it from the external source if it does not exist or has expired
        /// </summary>
        /// <param name="key">The key to the item to get</param>
        /// <returns>Returns the item belonging to <paramref name="key"/></returns>
        public TValue Get(TKey key)
        {
            CachedItem<TValue> cached = null;

            _cache.AddOrUpdate(key, id =>
            {
                // Insert into cache
                return cached = new CachedItem<TValue>(_getFromExternalSource(key));
            }, (id, old) =>
            {
                if (old.Expired(ValuesMaximumLifetime))
                {
                    // Update cache
                    return cached = new CachedItem<TValue>(_getFromExternalSource(key));
                }
                else
                {
                    // Don't need to update
                    return cached = old;
                }
            });

            return cached.Value;
        }

        /// <summary>
        /// Remove an value from the cache
        /// </summary>
        /// <param name="key">The key to the item to remove from the cache</param>
        public bool TryRemove(TKey key)
        {
            return _cache.TryRemove(key, out _);
        }

        /// <summary>
        /// Forcibly clears all items from the cache, regardless of any cache lifetime settings
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        private class CachedItem<T>
        {
            private static DateTime Now => DateTime.UtcNow;

            public CachedItem()
            {
                CachedOn = Now;
            }

            public CachedItem(T value) : this()
            {
                Value = value;
            }

            public T Value { get; set; }

            public DateTime CachedOn { get; set; }

            public bool Expired(TimeSpan? maxLifetime)
            {
                if (maxLifetime == null)
                {
                    return false;
                }

                return CachedOn.Add(maxLifetime.Value) < Now;
            }
        }
    }
}