using MarkSFrancis.Web.Models;
using MarkSFrancis.Web.Models.Request;
using MarkSFrancis.Web.Models.Response;
using MarkSFrancis.Web.Services;
using MarkSFrancis.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Fluent
{
    /// <summary>
    /// A fluent web request builder to help manage all the optional properties presented by an <see cref="ApiRequestMessage"/>
    /// </summary>
    public class FluentRequest
    {
        private readonly IHttpRequestService _apiRequestService;
        private readonly ApiRequestMessage _apiRequest;

        internal FluentRequest(IHttpRequestService apiRequestService)
        {
            _apiRequestService = apiRequestService;
            _apiRequest = new ApiRequestMessage
            {
                Content = string.Empty
            };
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="baseUrl">The base url to send to, or the full url</param>
        /// <param name="urlSegments">The URL path segments to add to the base url. These segments are escaped, and then joined with "/"</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest UseUrl(string baseUrl, params string[] urlSegments)
        {
            return UseUrl(baseUrl, (IEnumerable<string>)urlSegments);
        }

        /// <summary>
        /// Set the url to send the request to
        /// </summary>
        /// <param name="baseUrl">The base url to send to, or the full url</param>
        /// <param name="urlSegments">The URL path segments to add to the base url. These segments are escaped, and then joined with "/"</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest UseUrl(string baseUrl, IEnumerable<string> urlSegments)
        {
            _apiRequest.Url = UrlBuilder.ToUrl(baseUrl, urlSegments);

            return this;
        }

        /// <summary>
        /// Use a specified query string for the URL. This will replace any existing query string
        /// </summary>
        /// <param name="queryString">The query string to use</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithQuery(string queryString)
        {
            _apiRequest.Url = UrlBuilder.SetQueryString(_apiRequest.Url, queryString);

            return this;
        }

        /// <summary>
        /// Use a specified query object for the URL. This will replace any existing query string
        /// </summary>
        /// <param name="query">The object to convert and use as the query string</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithQuery(object query)
        {
            var queryString = UrlBuilder.ToQueryString(query);

            return WithQuery(queryString);
        }

        /// <summary>
        /// Use a JSON format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithJsonBody(object body)
        {
            _apiRequest.Content = JsonConvert.SerializeObject(body);
            _apiRequest.ContentType = ContentType.Json;

            return this;
        }

        /// <summary>
        /// Use a plaintext format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithPlainTextBody(string body)
        {
            _apiRequest.Content = body;
            _apiRequest.ContentType = ContentType.Text;

            return this;
        }

        /// <summary>
        /// Use an url-encoded form format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithUrlFormBody(object body)
        {
            _apiRequest.ContentType = ContentType.Form;
            _apiRequest.Content = UrlBuilder.ToQueryString(body);

            return this;
        }

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="contentType">The type of content to use</param>
        /// <param name="body">The data to send as body content</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest WithBody(string contentType, string body)
        {
            _apiRequest.Content = body;
            _apiRequest.ContentType = contentType;

            return this;
        }

        /// <summary>
        /// Use a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to send with the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest UseHeaders(HttpRequestHeaders headers)
        {
            _apiRequest.Headers = headers;

            return this;
        }

        /// <summary>
        /// Use a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to send with the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest UseHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            _apiRequest.Headers.Clear();

            return AppendHeaders(headers);
        }

        /// <summary>
        /// Add a specified collection of headers
        /// </summary>
        /// <param name="headers">The headers to append to the current headers in the request</param>
        /// <returns>This <see cref="FluentRequest"/></returns>
        public FluentRequest AppendHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            foreach (var header in headers)
            {
                _apiRequest.Headers.Add(header.Key, header.Value);
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
            _apiRequest.Headers.Add(key, value);

            return this;
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        public Task<ApiResponseMessage> Send(HttpMethod method)
        {
            return Send(method.ToString());
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in plaintext format</returns>
        public async Task<ApiResponseMessage> Send(string method)
        {
            _apiRequest.Method = method;

            var response = await _apiRequestService.SendAsync(_apiRequest);

            return new ApiResponseMessage(response);
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        public Task<ApiJsonResponseMessage<TResponse>> SendWithJsonResponse<TResponse>(HttpMethod method)
        {
            return SendWithJsonResponse<TResponse>(method.ToString());
        }

        /// <summary>
        /// Send the request with a JSON format response
        /// </summary>
        /// <typeparam name="TResponse">The format of the data to send</typeparam>
        /// <param name="method">The HTTP method to use when sending the request</param>
        /// <returns>The response from the API in a format ready for JSON deserialization</returns>
        public async Task<ApiJsonResponseMessage<TResponse>> SendWithJsonResponse<TResponse>(string method)
        {
            _apiRequest.Method = method;

            var response = await _apiRequestService.SendAsync(_apiRequest);

            return new ApiJsonResponseMessage<TResponse>(response);
        }
    }
}
