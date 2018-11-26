using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Phnx.Web.Models
{
    /// <summary>
    /// Represents an API response
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Create a new <see cref="ApiResponse"/> from a <see cref="HttpResponseMessage"/>, copying the relevant values
        /// </summary>
        /// <param name="message">The message to create the response from</param>
        public ApiResponse(HttpResponseMessage message)
        {
            Message = message;
        }

        /// <summary>
        /// The HTTP response message
        /// </summary>
        public HttpResponseMessage Message { get; }

        private string _bodyCache;
        private bool _bodyHasBeenLoaded;

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
            if (!_bodyHasBeenLoaded)
            {
                using (HttpContent content = Message.Content)
                {
                    _bodyCache = await content.ReadAsStringAsync();
                }

                _bodyHasBeenLoaded = true;
            }

            return _bodyCache;
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
            ThrowIfStatusCodeIsNot((IEnumerable<HttpStatusCode>)successCodes);
        }

        /// <summary>
        /// Throws a <see cref="HttpRequestException"/> with the request body and status code in the exception message
        /// </summary>
        /// <returns></returns>
        private HttpRequestException CreateError()
        {
            GetBodyAsStringAsync().Wait();

            return new ApiRequestException(
                Message.RequestMessage.RequestUri.ToString(),
                StatusCode,
                Headers,
                _bodyCache);
        }
    }
}
