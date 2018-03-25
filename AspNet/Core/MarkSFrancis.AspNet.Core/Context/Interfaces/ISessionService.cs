namespace MarkSFrancis.AspNet.Core.Context.Interfaces
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}