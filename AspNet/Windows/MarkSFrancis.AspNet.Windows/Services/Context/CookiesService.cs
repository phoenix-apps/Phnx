using System.Web;

namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public class CookiesService : BaseContextService
    {
        public CookiesService(HttpRequestBase request, HttpResponseBase response) : base(request, response)
        {
        }

        protected HttpCookie Get(string cookieKey)
        {
            return Request.Cookies[cookieKey];
        }

        protected void Set(string cookieKey, HttpCookie cookie)
        {
            Response.Cookies.Set(cookie);
        }
    }
}
