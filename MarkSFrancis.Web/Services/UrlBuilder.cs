using MarkSFrancis.Reflection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Web.Services
{
    /// <summary>
    /// Helps to build URLs from query strings and URL path segments
    /// </summary>
    public static class UrlBuilder
    {
        /// <summary>
        /// Convert an <see cref="object"/> to a query string
        /// </summary>
        /// <param name="query">The <see cref="object"/> to convert</param>
        /// <returns>A query string representation version of the properties of <paramref name="query"/></returns>
        public static string ToQueryString(object query)
        {
            if (query == null)
            {
                return string.Empty;
            }

            // Get all properties on the object
            var properties = query.GetType()
                .GetPropertyFieldInfos(getFields: false)
                .Where(x => x.CanGet)
                .ToDictionary(x => x.Name, x => x.GetValue(query));

            // Get names for all IEnumerable properties (excl. string)
            var collectionProperties = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in collectionProperties)
            {
                var enumerable = (IEnumerable)key.Value;

                properties[key.Key] = string.Join(",", (string[])enumerable);
            }

            // Concat all key/value pairs into a string separated by ampersand
            string queryString = string.Join("&",
                properties.Select(x =>
                    string.Concat(
                        Uri.EscapeDataString(x.Key),
                        "=",
                        Uri.EscapeDataString(x.Value?.ToString() ?? "")
                        )
                    )
                );

            return queryString;
        }

        /// <summary>
        /// Set the query string of a given url to a specified value, replacing the existing query string
        /// </summary>
        /// <param name="url">The url to set the query string of</param>
        /// <param name="queryString">The query string to set</param>
        /// <returns>The url with the query string set to <paramref name="queryString"/></returns>
        public static string SetQueryString(string url, string queryString)
        {
            if (url.Contains('?'))
            {
                url = url.Substring(0, url.IndexOf('?'));
            }

            if (queryString == null)
            {
                return url;
            }

            return url + "?" + (queryString ?? "");
        }

        /// <summary>
        /// Join url path segments together, sanitising their values to URL segments, and joining them up with "/" inbetween each
        /// </summary>
        /// <param name="baseUrl">The base URL to append to (e.g http://www.contoso.com)</param>
        /// <param name="pathSegments">The URL segments to append to the base URL</param>=
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(string baseUrl, params string[] pathSegments)
        {
            return ToUrl(baseUrl, true, pathSegments);
        }

        /// <summary>
        /// Join url path segments together, sanitising their values to URL segments, and joining them up with "/" inbetween each
        /// </summary>
        /// <param name="baseUrl">The base URL to append to (e.g http://www.contoso.com)</param>
        /// <param name="sanitisePathSegments">Whether to sanitise path segments</param>
        /// <param name="pathSegments">The URL segments to append to the base URL</param>=
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(string baseUrl, bool sanitisePathSegments, params string[] pathSegments)
        {
            return ToUrl(baseUrl, sanitisePathSegments, (IEnumerable<string>)pathSegments);
        }

        /// <summary>
        /// Join url path segments together, sanitising their values to URL segments, and joining them up with "/" inbetween each
        /// </summary>
        /// <param name="baseUrl">The base URL to append to (e.g http://www.contoso.com)</param>
        /// <param name="pathSegments">The URL segments to append to the base URL</param>
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(string baseUrl, IEnumerable<string> pathSegments)
        {
            return ToUrl(baseUrl, true, pathSegments);
        }

        /// <summary>
        /// Join url path segments together, optionally sanitising their values to URL segments, and joining them up with "/" inbetween each
        /// </summary>
        /// <param name="baseUrl">The base URL to append to (e.g http://www.contoso.com)</param>
        /// <param name="sanitisePathSegments">Whether to sanitise path segments</param>
        /// <param name="pathSegments">The URL segments to append to the base URL</param>
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(string baseUrl, bool sanitisePathSegments, IEnumerable<string> pathSegments)
        {
            IEnumerable<string> escapedPathSegments;

            if (sanitisePathSegments)
            {
                escapedPathSegments = pathSegments.Select(Uri.EscapeDataString);
            }
            else
            {
                escapedPathSegments = pathSegments;
            }

            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }

            var url = baseUrl + string.Join("/", escapedPathSegments);

            // Check if there were any path segments
            if (url == baseUrl)
            {
                // Remove trailing "/"
                return url.Substring(0, url.Length - 1);
            }

            return url;
        }
    }
}
