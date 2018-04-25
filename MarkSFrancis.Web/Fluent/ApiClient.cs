using MarkSFrancis.Web.Services;
using MarkSFrancis.Web.Services.Interfaces;
using System.Collections.Generic;

namespace MarkSFrancis.Web.Fluent
{
    /// <summary>
    /// An API client helper, used to generate requests
    /// </summary>
    public class ApiClient
    {
        private readonly IHttpRequestService _apiRequestService;

        /// <summary>
        /// Create a new <see cref="ApiClient"/> using <see cref="HttpRequestService"/> to handle sending and recieving over HTTP
        /// </summary>
        public ApiClient()
        {
            _apiRequestService = new HttpRequestService();
        }

        /// <summary>
        /// Create a new <see cref="ApiClient"/> with a custom or test implementation of <see cref="IHttpRequestService"/>
        /// </summary>
        /// <param name="apiRequestService"></param>
        public ApiClient(IHttpRequestService apiRequestService)
        {
            _apiRequestService = apiRequestService;
        }

        /// <summary>
        /// Create a new API request
        /// </summary>
        /// <param name="url">The url to point the request to</param>
        /// <param name="urlSegments">Any sub-url path segments. These are escaped and then joined onto the end of the url, with a "/" between each one</param>
        /// <returns>A fluent request builder</returns>
        public FluentRequest CreateRequest(string url, params string[] urlSegments)
        {
            return new FluentRequest(_apiRequestService)
                .UseUrl(url, urlSegments);
        }

        /// <summary>
        /// Create a new API request
        /// </summary>
        /// <param name="url">The url to point the request to</param>
        /// <param name="urlSegments">Any sub-url path segments. These are escaped and then joined onto the end of the url, with a "/" between each one</param>
        /// <returns>A fluent request builder</returns>
        public FluentRequest CreateRequest(string url, IEnumerable<string> urlSegments)
        {
            return new FluentRequest(_apiRequestService)
                .UseUrl(url, urlSegments);
        }
    }
}
