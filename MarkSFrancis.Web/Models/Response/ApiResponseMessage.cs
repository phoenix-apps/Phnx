using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Models.Response
{
    /// <summary>
    /// Represents an API response
    /// </summary>
    public class ApiResponseMessage
    {
        /// <summary>
        /// Create a new <see cref="ApiResponseMessage"/> from a <see cref="HttpResponseMessage"/>, copying the relevant values
        /// </summary>
        /// <param name="message">The message to create the response from</param>
        public ApiResponseMessage(HttpResponseMessage message)
        {
            Message = message;
        }

        /// <summary>
        /// The HTTP response message
        /// </summary>
        public HttpResponseMessage Message { get; }

        private string body;
        private bool bodyHasBeenLoaded;

        /// <summary>
        /// The status code of the response message
        /// </summary>
        public HttpStatusCode StatusCode => Message.StatusCode;

        /// <summary>
        /// Whether the status code is a sucess status code (200-299)
        /// </summary>
        public bool IsSuccessStatusCode => Message.IsSuccessStatusCode;

        /// <summary>
        /// The headers contained within the response
        /// </summary>
        public HttpResponseHeaders Headers => Message.Headers;

        /// <summary>
        /// Get the body of the response asyncronously
        /// </summary>
        public async Task<string> GetBodyAsStringAsync()
        {
            if (!bodyHasBeenLoaded)
            {
                using (HttpContent content = Message.Content)
                {
                    body = await content.ReadAsStringAsync();
                }

                bodyHasBeenLoaded = true;
            }

            return body;
        }

        /// <summary>
        /// Throw an exception if <see cref="IsSuccessStatusCode"/> is false
        /// </summary>
        public void ThrowIfNotSuccessStatus()
        {
            if (!IsSuccessStatusCode)
            {
                throw CreateError();
            }
        }

        /// <summary>
        /// Throw an exception if the <see cref="StatusCode"/> is not one of a range of values
        /// </summary>
        /// <param name="successCodes">The range of values to check</param>
        public void ThrowIfStatusCodeIsNot(IEnumerable<HttpStatusCode> successCodes)
        {
            if (!successCodes.Contains(StatusCode))
            {
                throw CreateError();
            }
        }

        /// <summary>
        /// Throw an exception if the <see cref="StatusCode"/> is not one of a range of values
        /// </summary>
        /// <param name="successCodes">The range of values to check</param>
        public void ThrowIfStatusCodeIsNot(params HttpStatusCode[] successCodes)
        {
            if (!successCodes.Contains(StatusCode))
            {
                throw CreateError();
            }
        }

        private HttpRequestException CreateError()
        {
            StringBuilder errorMessage = new StringBuilder();
            errorMessage.Append($"{(int)StatusCode} ({StatusCode}");

            if (bodyHasBeenLoaded)
            {
                var body = GetBodyAsStringAsync().Result;

                errorMessage.Append($", Body: {body}");
            }

            return new HttpRequestException(errorMessage.ToString());
        }
    }
}
