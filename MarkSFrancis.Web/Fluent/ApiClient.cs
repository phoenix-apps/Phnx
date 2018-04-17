using System.Collections.Generic;

namespace MarkSFrancis.Web.Fluent
{
    /// <summary>
    /// An API client helper, used to generate requests
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// Create a new API request
        /// </summary>
        /// <param name="url">The url to point the request to</param>
        /// <param name="urlSegments">Any sub-url path segments. These are escaped and then joined onto the end of the url, with a "/" between each one</param>
        /// <returns>A fluent request builder</returns>
        public FluentRequest CreateRequest(string url, params string[] urlSegments)
        {
            return new FluentRequest()
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
            return new FluentRequest()
                .UseUrl(url, urlSegments);
        }
    }
}
