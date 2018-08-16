using MarkSFrancis.AspNet.Context.Interfaces;
using System.Web;

namespace MarkSFrancis.AspNet.Context
{
    /// <summary>
    /// A service for managing getting cookies from a request, and adding cookies to a response
    /// </summary>
    public class CookiesService : BaseContextMetaService, ICookiesService
    {
        /// <summary>
        /// The cookies contained within the request
        /// </summary>
        protected HttpCookieCollection RequestCookies => Request.Cookies;

        /// <summary>
        /// The cookies contained within the response
        /// </summary>
        protected HttpCookieCollection ResponseCookies => Response.Cookies;

        /// <summary>
        /// Get a cookie by key from the request
        /// </summary>
        /// <param name="key">The key to the cookie to get</param>
        /// <returns>The value of the specified cookie</returns>
        public HttpCookie Get(string key)
        {
            return RequestCookies[key];
        }

        /// <summary>
        /// Append a cookie to the response
        /// </summary>
        /// <param name="cookie">The cookie to append</param>
        public void Append(HttpCookie cookie)
        {
            ResponseCookies.Add(cookie);
        }

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        public void Append(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);

            Append(cookie);
        }
    }
}
