using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MarkSFrancis.Web.Models.Request
{
    /// <summary>
    /// Represents the core features of an API request
    /// </summary>
    public class ApiRequestMessage
    {
        /// <summary>
        /// The body content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The body content type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The URL to send this request to
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The <see cref="HttpMethod"/> to use when sending this request
        /// </summary>
        /// <remarks>HTTP verbs are case sensitive. <see href="http://www.ietf.org/rfc/rfc2616.txt"/></remarks>
        public string Method { get; set; }

        /// <summary>
        /// The headers to use in this request
        /// </summary>
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Convert this object to a <see cref="HttpRequestMessage"/>
        /// </summary>
        /// <returns></returns>
        public HttpRequestMessage ToRequestMessage()
        {
            var method = new HttpMethod(Method);

            var request = new HttpRequestMessage(method, Url);

            if (Headers != null)
            {
                // Append headers
                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.Content = new StringContent(Content, Encoding.UTF8, ContentType);

            return request;
        }
    }
}
