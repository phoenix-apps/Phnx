using System.Runtime.Caching;

namespace MarkSFrancis.AspNet.Windows.Services.Cache
{
    public interface ICacheEntryGroupChild<T> : ICacheEntryGroupChild
    {
    }

    public interface ICacheEntryGroupChild
    {
        string Key { get; }

        CacheItemPolicy Policy { get; }
    }
}