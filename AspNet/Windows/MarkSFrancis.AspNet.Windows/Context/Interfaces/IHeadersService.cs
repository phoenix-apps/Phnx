namespace MarkSFrancis.AspNet.Windows.Context.Interfaces
{
    public interface IHeadersService
    {
        string Get(string key);
        void Set(string key, string value);
    }
}