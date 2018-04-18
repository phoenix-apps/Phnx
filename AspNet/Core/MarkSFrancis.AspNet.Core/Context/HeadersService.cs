using MarkSFrancis.AspNet.Core.Context.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    /// <summary>
    /// A service for managing getting headers from a request, and setting headers in the response
    /// </summary>
    public class HeadersService : BaseContextMetaService, IHeadersService
    {
        /// <summary>
        /// The headers contained within the request
        /// </summary>
        protected IHeaderDictionary RequestHeaders => Request.Headers;

        /// <summary>
        /// The headers contained within the response
        /// </summary>
        protected IHeaderDictionary ResponseHeaders => Request.Headers;

        /// <summary>
        /// Create a new <see cref="HeadersService"/> using a <see cref="IHttpContextAccessor"/> to access the request and response headers
        /// </summary>
        /// <param name="httpContextAccessor">The accessor for the current <see cref="HttpContext"/></param>
        public HeadersService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

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
