using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Cache.Interfaces
{
    public interface ICacheEntry<out T>
    {
        string Key { get; }

        CacheItemPolicy Policy { get; }

        T LoadFromExternalSource();
    }
}