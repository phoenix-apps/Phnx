using MarkSFrancis.AspNetCore.Context.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNetCore.Context
{
    /// <summary>
    /// A service for managing getting cookies from a request, and adding cookies to a response
    /// </summary>
    public class CookiesService : BaseContextMetaService, ICookiesService
    {
        /// <summary>
        /// The cookies contained within the request
        /// </summary>
        protected IRequestCookieCollection RequestCookies => Request.Cookies;

        /// <summary>
        /// The cookies contained within the response
        /// </summary>
        protected IResponseCookies ResponseCookies => Response.Cookies;

        /// <summary>
        /// Create a new <see cref="CookiesService"/> using a <see cref="IHttpContextAccessor"/> to access the request and response cookies
        /// </summary>
        /// <param name="httpContextAccessor">The accessor for the current <see cref="HttpContext"/></param>
        public CookiesService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// Get a cookie by key from the request
        /// </summary>
        /// <param name="key">The key to the cookie to get</param>
        /// <returns>The value of the specified cookie</returns>
        public string Get(string key)
        {
            return RequestCookies[key];
        }

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        public void Append(string key, string value)
        {
            ResponseCookies.Append(key, value);
        }

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        /// <param name="options">The storage (such as expiration) options for the cookie</param>
        public void Append(string key, string value, CookieOptions options)
        {
            ResponseCookies.Append(key, value, options);
        }
    }
}
