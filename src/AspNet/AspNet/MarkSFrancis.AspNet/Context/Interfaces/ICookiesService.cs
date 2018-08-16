using System.Web;

namespace MarkSFrancis.AspNet.Context.Interfaces
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
        HttpCookie Get(string key);

        /// <summary>
        /// Append a cookie to the response
        /// </summary>
        /// <param name="cookie">The cookie to append</param>
        void Append(HttpCookie cookie);

        /// <summary>
        /// Append a cookie with a key to the response
        /// </summary>
        /// <param name="key">The key to store the cookie with</param>
        /// <param name="value">The value of the cookie</param>
        void Append(string key, string value);
    }
}