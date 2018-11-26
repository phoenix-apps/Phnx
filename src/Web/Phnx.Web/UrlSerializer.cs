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
                {
                    string valueAsString;

                    if (x.Value is null)
                    {
                        valueAsString = string.Empty;
                    }
                    else
                    {
                        valueAsString = JsonConvert.SerializeObject(x.Value).Trim('\"');
                    }

                    return string.Concat(
                        Uri.EscapeDataString(x.Key),
                        "=",
                        Uri.EscapeDataString(valueAsString)
                        );
                })
            );

            return queryString;
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
    }
}
