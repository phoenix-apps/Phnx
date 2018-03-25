namespace MarkSFrancis.AspNet.Windows.Context.Interfaces
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}