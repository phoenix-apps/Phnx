using MarkSFrancis.AspNet.Core.Context.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    public class CookiesService : BaseContextMetaService, ICookiesService
    {
        protected IRequestCookieCollection RequestCookies => Request.Cookies;
        protected IResponseCookies ResponseCookies => Response.Cookies;

        public CookiesService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public string Get(string cookieKey)
        {
            return RequestCookies[cookieKey];
        }

        public void Append(string key, string value)
        {
            ResponseCookies.Append(key, value);
        }
    }
}
