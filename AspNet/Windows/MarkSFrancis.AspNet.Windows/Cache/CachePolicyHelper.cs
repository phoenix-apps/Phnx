using System;
using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Cache
{
    public static class CachePolicyHelper
    {
        private static DateTime Now => DateTime.Now;

        public static CacheItemPolicy TimeAfterNow(TimeSpan time)
        {
            return Time(new DateTimeOffset(Now, time));
        }

        public static CacheItemPolicy Time(DateTimeOffset offset)
        {
            return new CacheItemPolicy
            {
                AbsoluteExpiration = offset
            };
        }

        public static CacheItemPolicy MidnightTonight =>
            Time(DateTime.Today.AddDays(1));
    }
}
