using MarkSFrancis.AspNet.Context.Interfaces;
using System.Collections.Specialized;

namespace MarkSFrancis.AspNet.Context
{
    /// <summary>
    /// A service for managing getting headers from a request, and setting headers in the response
    /// </summary>
    public class HeadersService : BaseContextMetaService, IHeadersService
    {
        /// <summary>
        /// The headers contained within the request
        /// </summary>
        protected NameValueCollection RequestHeaders => Request.Headers;

        /// <summary>
        /// The headers contained within the response
        /// </summary>
        protected NameValueCollection ResponseHeaders => Response.Headers;

        /// <summary>
        /// Get a header by key from the request
        /// </summary>
        /// <param name="key">The key to the header to get</param>
        /// <returns>The value of the specified header</returns>
        public string Get(string key)
        {
            return RequestHeaders[key];
        }

        /// <summary>
        /// Set a header in the response
        /// </summary>
        /// <param name="key">The key to the header to set</param>
        /// <param name="value">The value of the header</param>
        public void Set(string key, string value)
        {
            ResponseHeaders[key] = value;
        }
    }
}
