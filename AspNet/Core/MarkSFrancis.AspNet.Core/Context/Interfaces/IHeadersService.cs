namespace MarkSFrancis.AspNet.Core.Context.Interfaces
{
    public interface IHeadersService
    {
        string Get(string key);
        void Set(string key, string value);
    }
}