using System;
using System.Linq;
using System.Web;

namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public class CookiesService : BaseContextService, IContextMetaService<string, HttpCookie>
    {
        public CookiesService(HttpRequestBase request, HttpResponseBase response) : base(request, response)
        {
        }

        protected HttpCookieCollection RequestCookies => Request.Cookies;
        protected HttpCookieCollection ResponseCookies => Response.Cookies;

        public HttpCookie Get(string cookieKey)
        {
            return RequestCookies[cookieKey];
        }

        public void Set(string cookieKey, HttpCookie cookie)
        {
            if (cookieKey != cookie.Name)
            {
                throw new ArgumentException($"{nameof(cookieKey)} must match the name of the cookie. The key given was \"{cookieKey}\" and the cookie name was \"{cookie.Name}\"");
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
