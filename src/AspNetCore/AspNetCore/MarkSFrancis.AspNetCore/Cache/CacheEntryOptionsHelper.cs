using Microsoft.Extensions.Caching.Memory;
using System;

namespace MarkSFrancis.AspNetCore.Cache
{
    /// <summary>
    /// A series of helpers for quickly generating cache storage options
    /// </summary>
    public static class CacheEntryOptionsHelper
    {
        /// <summary>
        /// Create a cache entry option that specifies that the entry should expire after a set amount of time
        /// </summary>
        /// <param name="time">The lifetime of the cache entry</param>
        /// <returns>A cache entry option that specifies that the entry should expire after a set amount of time</returns>
        public static MemoryCacheEntryOptions TimeAfterNow(TimeSpan time)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = time
            };
        }

        /// <summary>
        /// Create a cache entry option that specifies that the entry should expire at a set time of day
        /// </summary>
        /// <param name="offset">The time at which the cache entry should expire</param>
        /// <returns>A cache entry option that specifies that the entry should expire at a set time of day</returns>
        public static MemoryCacheEntryOptions Time(DateTimeOffset offset)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = offset
            };
        }

        /// <summary>
        /// A cache entry option that specifies that the entry should expire at midnight tonight
        /// </summary>
        public static MemoryCacheEntryOptions MidnightTonight =>
            Time(DateTime.Today.AddDays(1));
    }
}
