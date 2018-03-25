using Microsoft.Extensions.Caching.Memory;

namespace MarkSFrancis.AspNet.Core.Cache.Interfaces
{
    public interface ICacheEntry<out T>
    {
        string Key { get; }

        MemoryCacheEntryOptions Options { get; }

        T LoadFromExternalSource();
    }
}