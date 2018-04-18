using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context.Interfaces
{
    /// <summary>
    /// A service for managing getting cookies from a request, and adding cookies to a response
    /// </summary>
    public interface ICookiesService
    {
        /// <summary>
        /// Get a cookie by key from the request
        /// </summary>
        /// <param name="key">The key to the cookie to get</param>
        /// <returns>The value of the specified cookie</returns>
        string Get(string key);

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        void Append(string key, string value);

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        /// <param name="options">The storage (such as expiration) options for the cookie</param>
        void Append(string key, string value, CookieOptions options);
    }
}