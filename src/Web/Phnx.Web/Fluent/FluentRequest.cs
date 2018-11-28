using Phnx.Web.Models;
using Phnx.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        /// <summary>
        /// Creates a new <see cref="FluentRequest"/> with <paramref name="apiRequestService"/> used for sending the requests
        /// </summary>
        /// <param name="apiRequestService">The request sender</param>
        internal FluentRequest(IHttpRequestService apiRequestService)
        {
            Debug.Assert(apiRequestService != null);

            _apiRequestService = apiRequestService;
            Request = new HttpRequestMessage();
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="builder">The method to use to build the url</param>
        /// <returns>A fluent request url builder</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="builder"/> creates an invalid Uri</exception>
        public FluentRequest UseUrl(Action<FluentRequestUrl> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var fluentUrl = new FluentRequestUrl(this);
            builder(fluentUrl);

            var url = fluentUrl.Build();
            Request.RequestUri = new Uri(url);

            return this;
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="url">The url to send the request to</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public FluentRequest UseUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

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
        public FluentRequest SetHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            Request.Headers.Clear();

            if (headers is null)
            {
                return this;
            }

            foreach (var header in headers)
            {
                AppendHeader(header.Key, header.Value);
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
            Request.Headers.Clear();

            if (headers is null)
            {
                return this;
            }

            AppendHeaders(headers);

            return this;
        }

        /// <summary>
        /// Add a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest AppendHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            if (headers is null)
            {
                return this;
            }

            foreach (var header in headers)
            {
                AppendHeader(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Add a specified header
        /// </summary>
        /// <param name="key">The key for the header to append to the current headers in the request</param>
        /// <param name="value">The value for the header to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> is empty or whitespace</exception>
        public FluentRequest AppendHeader(string key, IEnumerable<string> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"{key} cannot be null or whitespace", nameof(key));
            }
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Request.Headers.Add(key, value);

            return this;
        }

        /// <summary>
        /// Add a specified header
        /// </summary>
        /// <param name="key">The key for the header to append to the current headers in the request</param>
        /// <param name="value">The value for the header to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> is empty or whitespace</exception>
        public FluentRequest AppendHeader(string key, string value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"{key} cannot be null or whitespace", nameof(key));
            }

            Request.Headers.Add(key, value);

            return this;
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <see langword="null"/></exception>
        public async Task<ApiResponse> Send(HttpMethod method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Request.Method = method;

            var response = await _apiRequestService.SendAsync(Request);

            return new ApiResponse(response);
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="method"/> is empty or whitespace</exception>
        public Task<ApiResponse> Send(string method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException($"{method} cannot be null or whitespace", nameof(method));
            }

            return Send(new HttpMethod(method));
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <see langword="null"/></exception>
        public async Task<ApiResponseJson<TResponse>> SendWithJsonResponse<TResponse>(HttpMethod method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Request.Method = method;

            var response = await _apiRequestService.SendAsync(Request);

            return new ApiResponseJson<TResponse>(response);
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="method"/> is empty or whitespace</exception>
        public Task<ApiResponseJson<TResponse>> SendWithJsonResponse<TResponse>(string method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException($"{method} cannot be null or whitespace", nameof(method));
            }

            return SendWithJsonResponse<TResponse>(new HttpMethod(method));
        }

        /// <summary>
        /// Converts this request to a string representation of itself
        /// </summary>
        /// <returns>A string representation of the request</returns>
        public override string ToString()
        {
            StringBuilder description = new StringBuilder();
            description.Append("URL: ");
            if (Request.RequestUri != null)
            {
                description.Append(Request.RequestUri.ToString());
            }
            else
            {
                description.Append("null");
            }

            description.Append("; Content: ");
            if (Request.Content != null)
            {
                var sc = Request.Content as StringContent;

                if (sc is null)
                {
                    description.Append("null");
                }
                else
                {
                    description.Append(sc.Headers.ContentType.MediaType);
                }
            }
            else
            {
                description.Append("null");
            }

            description.Append("; Custom Headers: {Count=");
            if (Request.Headers != null)
            {
                description.Append(Request.Headers.Count());
            }
            else
            {
                description.Append("0");
            }
            description.Append("}");

            return description.ToString();
        }
    }
}
