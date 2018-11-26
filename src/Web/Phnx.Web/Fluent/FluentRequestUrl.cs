using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Phnx.Web.Fluent
{
    /// <summary>
    /// A fluent request url builder to help with fluent web requests
    /// </summary>
    public class FluentRequestUrl
    {
        internal FluentRequestUrl(FluentRequest request)
        {
            Debug.Assert(request != null);

            this.request = request;
        }

        private readonly FluentRequest request;

        private string baseUrl;
        private string queryUrl;
        private string pathUrl;

        /// <summary>
        /// Use a base url for the request
        /// </summary>
        /// <param name="url">The url to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Base(string url)
        {
            baseUrl = url;

            return this;
        }

        /// <summary>
        /// Use a query string for the request
        /// </summary>
        /// <param name="query">The query string to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Query(string query)
        {
            queryUrl = query;

            return this;
        }

        /// <summary>
        /// Use a query string for the request
        /// </summary>
        /// <param name="query">The object to convert and use as the query string</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Query(object query)
        {
            return Query(UrlSerializer.ToQueryString(query));
        }

        /// <summary>
        /// Use a relative path for the request (does not replace any existing base url)
        /// </summary>
        /// <param name="sanitiseSegments">Whether to Uri sanitise each of the path segments</param>
        /// <param name="pathSegments">The path segments to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Path(bool sanitiseSegments, params string[] pathSegments)
        {
            return Path(sanitiseSegments, (IEnumerable<string>)pathSegments);
        }

        /// <summary>
        /// Use a relative path for the request (does not replace any existing base url)
        /// </summary>
        /// <param name="sanitiseSegments">Whether to Uri sanitise each of the path segments</param>
        /// <param name="pathSegments">The path segments to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Path(bool sanitiseSegments, IEnumerable<string> pathSegments)
        {
            pathUrl = UrlSerializer.ToUrl(pathSegments, sanitiseSegments);

            return this;
        }

        /// <summary>
        /// Build the url together
        /// </summary>
        /// <returns>The underlying <see cref="FluentRequest"/></returns>
        internal string Build()
        {
            StringBuilder url = new StringBuilder();
            if (!string.IsNullOrEmpty(baseUrl))
            {
                url.Append(baseUrl);
            }

            if (!string.IsNullOrEmpty(pathUrl))
            {
                if (baseUrl.Length > 0 && !baseUrl.EndsWith("/"))
                {
                    url.Append("/");
                }

                url.Append(pathUrl);
            }

            if (!string.IsNullOrEmpty(queryUrl))
            {
                if (!string.IsNullOrEmpty(baseUrl) || !string.IsNullOrEmpty(pathUrl))
                {
                    url.Append("?");
                }

                url.Append(queryUrl);
            }

            return url.ToString();
        }
    }
}
