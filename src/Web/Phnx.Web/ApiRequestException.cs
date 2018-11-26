using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Phnx.Web
{
    /// <summary>
    /// An exception that represents a bad status code response
    /// </summary>
    public class ApiRequestException : HttpRequestException
    {
        /// <summary>
        /// Create a new <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="apiUrl">The api URL that the error came from</param>
        /// <param name="statusCode">The HTTP status code that could not be handled</param>
        /// <param name="headers">The HTTP headers contained in the response</param>
        /// <param name="body">The body of the response</param>
        public ApiRequestException(string apiUrl, HttpStatusCode statusCode, HttpResponseHeaders headers, string body) : base()
        {
            ApiUrl = apiUrl;
            StatusCode = statusCode;
            Body = body;
            Headers = headers;
        }

        /// <summary>
        /// The API URL that the response came from
        /// </summary>
        public string ApiUrl { get; }

        /// <summary>
        /// The status code response that the server gave
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The body of the response
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The HTTP headers of the response
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// Convert this exception to a string that contains the status code and body of the error
        /// </summary>
        /// <returns>A string that contains the status code and body of the error</returns>
        public override string ToString()
        {
            StringBuilder errorMessage = new StringBuilder();
            errorMessage.AppendLine($"Response error from {ApiUrl}");
            errorMessage.Append($"{(int)StatusCode} ({StatusCode})");

            if (Headers != null)
            {
                errorMessage.AppendLine();
                errorMessage.AppendLine("Headers: {");
                foreach (var header in Headers)
                {
                    errorMessage.AppendLine($"\t{header.Key}: \"{string.Join(", ", header.Value)}\"");
                }
                errorMessage.Append("}");
            }

            if (Body != null)
            {
                errorMessage.AppendLine();
                errorMessage.AppendLine("Body: ");
                errorMessage.Append(Body);
            }

            return errorMessage.ToString();
        }
    }
}
