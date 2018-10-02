using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Phnx.Data.LazyLoad
{
    /// <summary>
    /// A lazy loaded dictionary, which is thread-safe
    /// </summary>
    /// <typeparam name="TKey">The type of identifier for this value</typeparam>
    /// <typeparam name="TValue">The type of data in this table</typeparam>
    public class LazyDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, CacheEntry> _cache;
        private ConcurrentQueue<Lifetime> _expirationTimes;

        private readonly Func<TKey, TValue> _getFromExternalSource;

        private readonly Timer _expirationTimer;
        private readonly object _expirationTimesLock;
        private bool _expirationTimerIsRunning;

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
        /// Gets <see cref="DateTime.UtcNow"/> for use with managing lifetimes and expiration
        /// </summary>
        private DateTime Now => DateTime.UtcNow;

        /// <summary>
        /// Create a new <see cref="LazyDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="getFromExternalSource">The method to use when loading an item from an external source. This is called only once per value, when the value is first loaded</param>
        public LazyDictionary(Func<TKey, TValue> getFromExternalSource)
        {
            _getFromExternalSource = getFromExternalSource ?? throw new ArgumentNullException(nameof(getFromExternalSource));

            _expirationTimesLock = new object();
            ValuesMaximumLifetime = null;
            _cache = new ConcurrentDictionary<TKey, CacheEntry>();
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
            _cache = new ConcurrentDictionary<TKey, CacheEntry>();

            _expirationTimes = new ConcurrentQueue<Lifetime>();
            _expirationTimesLock = new object();
            _expirationTimerIsRunning = false;
            _expirationTimer = new Timer(ItemExpirationHandler);
        }

        private void ItemExpirationHandler(object state)
        {
            // Remove any expired entries from the lifetimes
            lock (_expirationTimesLock)
            {
                while (_expirationTimes.TryPeek(out var expiredEntry))
                {
                    var now = DateTime.UtcNow;

                    // Double check timer
                    if (!expiredEntry.IsExpired(now, ValuesMaximumLifetime.Value))
                    {
                        // Item not expired, timer was over enthusiastic
                        // Set timer
                        var expiresIn = (expiredEntry.CachedOn + ValuesMaximumLifetime.Value) - now;
                        _expirationTimer.Change((long)expiresIn.TotalMilliseconds, -1L);
                        return;
                    }

                    // Remove consumed time
                    _expirationTimes.TryDequeue(out _);

                    // If item has not been updated since that lifetime expiration
                    if (_cache.TryGetValue(expiredEntry.Key, out var entry) && entry.LastUpdated == expiredEntry.CachedOn)
                    {
                        // Remove expired item
                        _cache.TryRemove(expiredEntry.Key, out _);
                    }
                }
            }

            lock (_expirationTimesLock)
            {
                _expirationTimerIsRunning = false;
            }
        }

        /// <summary>
        /// Add or update an value in the cache
        /// </summary>
        /// <param name="key">The key to the value to add or update</param>
        /// <param name="value">The value to add or update</param>
        public void AddOrUpdate(TKey key, TValue value)
        {
            DateTime now = Now;
            var cachedValue = new CacheEntry(now, value);

            PrepareAutoExpiration(now, key);

            _cache.AddOrUpdate(key, cachedValue, (_, old) => cachedValue);
        }

        private void PrepareAutoExpiration(DateTime creationTime, TKey key)
        {
            if (!ValuesHaveMaximumLifetime)
            {
                return;
            }

            lock (_expirationTimesLock)
            {
                // Do inside lock to try to reduce diff between creation time and expiration timer
                _expirationTimes.Enqueue(new Lifetime(creationTime, key));

                if (!_expirationTimerIsRunning)
                {
                    _expirationTimer.Change((long)ValuesMaximumLifetime.Value.TotalMilliseconds, -1L);
                    _expirationTimerIsRunning = true;
                }
            }
        }

        /// <summary>
        /// Get the value which belongs to <paramref name="key"/> from the cache, or loads it from the external source if it does not exist or has expired
        /// </summary>
        /// <param name="key">The key to the item to get</param>
        /// <returns>Returns the item belonging to <paramref name="key"/></returns>
        public TValue Get(TKey key)
        {
            // ConcurrentDictionary double-calls get if remove is called whilst adding or updating
            lock (_expirationTimesLock)
            {
                var cached = _cache.AddOrUpdate(key, id =>
                {
                    // Insert into cache
                    var value = _getFromExternalSource(key);
                    var now = Now;
                    var cacheEntry = new CacheEntry(now, value);
                    PrepareAutoExpiration(now, key);

                    return cacheEntry;
                }, (id, old) =>
                {
                    if (ValuesHaveMaximumLifetime)
                    {
                        // Check expiry
                        var now = Now;
                        if (old.LastUpdated + ValuesMaximumLifetime.Value <= now)
                        {
                            var value = _getFromExternalSource(key);

                            // replace
                            var cacheEntry = new CacheEntry(now, value);
                            PrepareAutoExpiration(now, key);

                            return cacheEntry;
                        }
                    }

                    // Don't need to update
                    return old;
                });

                return cached.Value;
            }
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
            if (ValuesHaveMaximumLifetime)
            {
                lock (_expirationTimesLock)
                {
                    _cache.Clear();

                    // Drop the existing queue and rebuild a new one
                    _expirationTimes = new ConcurrentQueue<Lifetime>();
                }
            }
            else
            {
                _cache.Clear();
            }
        }

        private class Lifetime
        {
            public Lifetime(DateTime now, TKey key)
            {
                CachedOn = now;
                Key = key;
            }

            public TKey Key { get; set; }

            public DateTime CachedOn { get; set; }

            public bool IsExpired(DateTime now, TimeSpan maxLifetime)
            {
                return CachedOn + maxLifetime < now;
            }
        }

        private class CacheEntry
        {
            public CacheEntry(DateTime lastUpdated, TValue value)
            {
                LastUpdated = lastUpdated;
                Value = value;
            }

            public DateTime LastUpdated { get; }
            public TValue Value { get; }
        }
    }
}
