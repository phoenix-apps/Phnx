using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Services.Cache
{
    public interface ICacheEntry<out T>
    {
        string Key { get; }

        CacheItemPolicy Policy { get; }

        T LoadFromExternalSource();
    }
}