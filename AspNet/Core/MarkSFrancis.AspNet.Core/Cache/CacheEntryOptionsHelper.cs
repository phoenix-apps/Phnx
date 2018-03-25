using System;
using Microsoft.Extensions.Caching.Memory;

namespace MarkSFrancis.AspNet.Core.Cache
{
    public static class CacheEntryOptionsHelper
    {
        public static MemoryCacheEntryOptions TimeAfterNow(TimeSpan time)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = time
            };
        }

        public static MemoryCacheEntryOptions Time(DateTimeOffset offset)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = offset
            };
        }

        public static MemoryCacheEntryOptions MidnightTonight =>
            Time(DateTime.Today.AddDays(1));
    }
}
