using Phnx.Web.Models.Response;
using Phnx.Web.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Phnx.Web.Fluent
{
    /// <summary>
    /// A fluent web request builder to help create a <see cref="HttpRequestMessage"/>
    /// </summary>
    public class FluentRequest
    {
        private readonly IHttpRequestService _apiRequestService;

        /// <summary>
        /// The <see cref="HttpRequestMessage"/> being built by this fluent request
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        internal FluentRequest(IHttpRequestService apiRequestService)
        {
            _apiRequestService = apiRequestService;
            Request = new HttpRequestMessage();
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="builder">The method to use to build the url</param>
        /// <returns>A fluent request url builder</returns>
        public FluentRequest UseUrl(Action<FluentRequestUrl> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var fluentUrl = new FluentRequestUrl(this);
            builder(fluentUrl);
            fluentUrl.Build();

            return this;
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="url">The url to send the request to</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest UseUrl(string url)
        {
            Request.RequestUri = new Uri(url);

            return this;
        }

        /// <summary>
        /// Use a specified body for the request
        /// </summary>
        /// <returns>A fluent request body builder</returns>
        public FluentRequestBody WithBody()
        {
            return new FluentRequestBody(this);
        }

        /// <summary>
        /// Add a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest SetHeaders(HttpRequestHeaders headers)
        {
            Request.Headers.Clear();

            foreach (var header in headers)
            {
                Request.Headers.Add(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Add a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest SetHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            foreach (var header in headers)
            {
                Request.Headers.Add(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Add a specified header
        /// </summary>
        /// <param name="key">The key for the header to append to the current headers in the request</param>
        /// <param name="value">The value for the header to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest AppendHeader(string key, string value)
        {
            Request.Headers.Add(key, value);

            return this;
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        public async Task<ApiResponseMessage> Send(HttpMethod method)
        {
            Request.Method = method;

            var response = await _apiRequestService.SendAsync(Request);

            return new ApiResponseMessage(response);
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        public Task<ApiResponseMessage> Send(string method)
        {
            return Send(new HttpMethod(method));
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        public async Task<ApiJsonResponseMessage<TResponse>> SendWithJsonResponse<TResponse>(HttpMethod method)
        {
            Request.Method = method;

            var response = await _apiRequestService.SendAsync(Request);

            return new ApiJsonResponseMessage<TResponse>(response);
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        public Task<ApiJsonResponseMessage<TResponse>> SendWithJsonResponse<TResponse>(string method)
        {
            return SendWithJsonResponse<TResponse>(new HttpMethod(method));
        }
    }
}
