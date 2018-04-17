using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MarkSFrancis.Web.Models.Request
{
    public class ApiRequestMessage
    {
        public string Content { get; set; }

        public string ContentType { get; set; }

        public string Url { get; set; }

        public HttpMethod Method { get; set; }

        public IReadOnlyDictionary<string, string> Headers { get; set; }

        public HttpRequestMessage ToRequestMessage()
        {
            var request = new HttpRequestMessage(Method, Url);

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
