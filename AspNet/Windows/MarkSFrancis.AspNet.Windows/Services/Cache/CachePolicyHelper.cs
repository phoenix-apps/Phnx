using System;
using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Services.Cache
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

        public static CacheItemPolicy TimeAfterNow24Hours =>
            TimeAfterNow(TimeSpan.FromHours(24));

        public static CacheItemPolicy MidnightTonight =>
            Time(DateTime.Today.AddDays(1));
    }
}
