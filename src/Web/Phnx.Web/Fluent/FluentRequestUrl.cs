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
        /// Set the base url for the request
        /// </summary>
        /// <param name="url">The url to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Base(string url)
        {
            baseUrl = url;

            return this;
        }

        /// <summary>
        /// Set the query string for the request
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
        /// Set the relative path for the request
        /// </summary>
        /// <param name="path">The relative path</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Path(string path)
        {
            pathUrl = path;

            return this;
        }

        /// <summary>
        /// Set the relative path for the request
        /// </summary>
        /// <param name="sanitiseSegments">Whether to Uri sanitise each of the path segments</param>
        /// <param name="pathSegments">The path segments to use</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Path(bool sanitiseSegments, params string[] pathSegments) =>
            Path(sanitiseSegments, (IEnumerable<string>)pathSegments);

        /// <summary>
        /// Set the relative path for the request
        /// </summary>
        /// <param name="pathTemplate">The path template to use (such as "api/people/{id}")</param>
        /// <param name="parameters">The object to overlay onto the path template</param>
        /// <returns>This <see cref="FluentRequestUrl"/></returns>
        public FluentRequestUrl Path(string pathTemplate, object parameters)
        {
            pathUrl = UrlSerializer.ToUrl(pathTemplate, parameters);

            return this;
        }

        /// <summary>
        /// Set the relative path for the request
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
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                url.Append(baseUrl);
            }

            if (!string.IsNullOrWhiteSpace(pathUrl))
            {
                if (baseUrl != null && baseUrl.Length > 0 && !baseUrl.EndsWith("/"))
                {
                    url.Append("/");
                }

                url.Append(pathUrl);
            }

            if (!string.IsNullOrWhiteSpace(queryUrl))
            {
                if (url.Length > 0)
                {
                    url.Append("?");
                }

                url.Append(queryUrl);
            }

            return url.ToString();
        }
    }
}
