using Newtonsoft.Json;
using Phnx.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Web
{
    /// <summary>
    /// Helps to build URLs from query strings and URL path segments
    /// </summary>
    public static class UrlSerializer
    {
        /// <summary>
        /// Convert an <see cref="object"/> to a query string
        /// </summary>
        /// <param name="query">The <see cref="object"/> to convert</param>
        /// <returns>A query string representation version of the properties of <paramref name="query"/></returns>
        public static string ToQueryString(object query)
        {
            if (query is null)
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

            // Concat all key/value pairs into a string separated by ampersand
            string queryString = string.Join("&",
                properties.Select(x =>
                {
                    return string.Concat(
                        Uri.EscapeDataString(x.Key),
                        "=",
                        SerializeForUrl(x.Value)
                        );
                })
            );

            return queryString;
        }

        /// <summary>
        /// Map an object <see cref="object"/> onto a path template
        /// </summary>
        /// <param name="pathTemplate">The path template to use (such as "api/people/{id}")</param>
        /// <param name="parameters">The object to overlay onto the path template</param>
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(string pathTemplate, object parameters)
        {
            if (pathTemplate is null)
            {
                return string.Empty;
            }
            if (parameters is null)
            {
                return pathTemplate;
            }

            var properties = parameters.GetType()
                .GetPropertyFieldInfos(getFields: false)
                .Where(x => x.CanGet)
                .ToDictionary(x => x.Name, x =>
                {
                    var value = x.GetValue(parameters);

                    var serialized = SerializeForUrl(value);
                    return serialized;
                });

            string url = pathTemplate;
            foreach (var param in properties)
            {
                url = url.Replace("{" + param.Key + "}", param.Value);
            }

            return url;
        }

        /// <summary>
        /// Join url path segments together, optionally sanitising their values to URL segments, and joining them up with "/" inbetween each
        /// </summary>
        /// <param name="sanitisePathSegments">Whether to sanitise path segments</param>
        /// <param name="pathSegments">The URL segments to append to the base URL</param>
        /// <returns>The fully qualified URL with all the path segments escaped and appended</returns>
        public static string ToUrl(IEnumerable<string> pathSegments, bool sanitisePathSegments)
        {
            if (pathSegments is null)
            {
                return string.Empty;
            }

            IEnumerable<string> escapedPathSegments;

            if (sanitisePathSegments)
            {
                escapedPathSegments = pathSegments.Select(Uri.EscapeDataString);
            }
            else
            {
                escapedPathSegments = pathSegments;
            }

            var url = string.Join("/", escapedPathSegments);

            return url;
        }

        /// <summary>
        /// Serialize a value for use as a URL or query string parameter
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <returns><paramref name="value"/> serialized for use as a URL or query string parameter</returns>
        public static string SerializeForUrl(object value)
        {
            if (value is null)
            {
                return string.Empty;
            }
            else
            {
                var serialized = JsonConvert.SerializeObject(value).Trim('\"');

                return Uri.EscapeDataString(serialized);
            }
        }
    }
}
