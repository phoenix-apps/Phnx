using System.Web;

namespace MarkSFrancis.AspNet.Windows.Context.Interfaces
{
    public interface ICookiesService
    {
        HttpCookie Get(string cookieKey);
        void Set(string cookieKey, HttpCookie cookie);
    }
}