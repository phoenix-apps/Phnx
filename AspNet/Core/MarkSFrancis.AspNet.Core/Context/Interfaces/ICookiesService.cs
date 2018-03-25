namespace MarkSFrancis.AspNet.Core.Context.Interfaces
{
    public interface ICookiesService
    {
        string Get(string cookieKey);
        void Append(string key, string value);
    }
}