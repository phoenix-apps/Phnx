using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
            _message = message;
            _body = new Lazy<string>(LoadBody);
        }

        private readonly HttpResponseMessage _message;

        private readonly Lazy<string> _body;

        /// <summary>
        /// The status code of the response message
        /// </summary>
        public HttpStatusCode StatusCode => _message.StatusCode;

        /// <summary>
        /// Whether the status code is a sucess status code (200-299)
        /// </summary>
        public bool IsSuccessStatusCode => _message.IsSuccessStatusCode;

        /// <summary>
        /// The headers contained within the response
        /// </summary>
        public HttpResponseHeaders Headers => _message.Headers;

        /// <summary>
        /// The body of the response
        /// </summary>
        public string Body => _body.Value;

        private string LoadBody()
        {
            using (HttpContent content1 = _message.Content)
            {
                return content1.ReadAsStringAsync().Result;
            }
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
            return new HttpRequestException(
                $"{(int)StatusCode} ({StatusCode}), Body: {Body}");
        }
    }
}