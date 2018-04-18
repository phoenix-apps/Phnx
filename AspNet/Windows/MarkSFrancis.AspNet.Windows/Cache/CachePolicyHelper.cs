using System;
using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Cache
{
    /// <summary>
    /// A series of helpers for quickly generating cache storage policies
    /// </summary>
    public static class CachePolicyHelper
    {
        private static DateTime Now => DateTime.Now;

        /// <summary>
        /// Create a cache policy that specifies that the entry should expire after a set amount of time
        /// </summary>
        /// <param name="time">The lifetime of the cache entry</param>
        /// <returns>A cache policy that specifies that the entry should expire after a set amount of time</returns>
        public static CacheItemPolicy TimeAfterNow(TimeSpan time)
        {
            return Time(new DateTimeOffset(Now, time));
        }

        /// <summary>
        /// Create a cache policy that specifies that the entry should expire at a set time of day
        /// </summary>
        /// <param name="offset">The time at which the cache entry should expire</param>
        /// <returns>A cache policy that specifies that the entry should expire at a set time of day</returns>
        public static CacheItemPolicy Time(DateTimeOffset offset)
        {
            return new CacheItemPolicy
            {
                AbsoluteExpiration = offset
            };
        }

        /// <summary>
        /// A cache policy that specifies that the entry should expire at midnight tonight
        /// </summary>
        public static CacheItemPolicy MidnightTonight =>
            Time(DateTime.Today.AddDays(1));
    }
}
