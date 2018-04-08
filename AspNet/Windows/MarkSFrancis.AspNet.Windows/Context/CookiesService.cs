using System.Linq;
using System.Web;
using MarkSFrancis.AspNet.Windows.Context.Interfaces;
using MarkSFrancis.AspNet.Windows.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public class CookiesService : BaseContextMetaService, ICookiesService
    {
        protected HttpCookieCollection RequestCookies => Request.Cookies;
        protected HttpCookieCollection ResponseCookies => Response.Cookies;

        public CookiesService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public HttpCookie Get(string cookieKey)
        {
            return RequestCookies[cookieKey];
        }

        public void Set(string cookieKey, HttpCookie cookie)
        {
            if (cookieKey != cookie.Name)
            {
                throw ErrorFactory.Default.InvalidCookieKey(cookieKey, cookie.Name);
            }

            if (ResponseCookies.AllKeys.Contains(cookieKey))
            {
                ResponseCookies.Set(cookie);
            }
            else
            {
                ResponseCookies.Add(cookie);
            }
        }
    }
}
