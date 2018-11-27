using Phnx.Web.Fluent;
using Phnx.Web.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Services
{
    /// <summary>
    /// Extensions for <see cref="IHttpRequestService"/>
    /// </summary>
    public static class IHttpRequestServiceExtensions
    {
        /// <summary>
        /// Make a GET request to the specified URL
        /// </summary>
        /// <typeparam name="T">The type of data expected to be returned</typeparam>
        /// <param name="requestService">The request service to use when sending requests</param>
        /// <param name="url">The URL to send the request to</param>
        /// <returns>The response from the server</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestService"/> or <paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public static async Task<ApiResponseJson<T>> GetAsync<T>(this IHttpRequestService requestService, string url)
        {
            if (requestService is null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            return await PrepareRequest(requestService, url)
                .SendWithJsonResponse<T>(HttpMethod.Get);
        }

        /// <summary>
        /// Make a POST request to the specified URL
        /// </summary>
        /// <typeparam name="T">The type of data expected to be returned</typeparam>
        /// <param name="requestService">The request service to use when sending requests</param>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="body">The body of the content to send</param>
        /// <returns>The response from the server</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestService"/> or <paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public static async Task<ApiResponseJson<T>> PostAsync<T>(this IHttpRequestService requestService, string url, object body)
        {
            if (requestService is null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            return await PrepareRequest(requestService, url, body)
                .SendWithJsonResponse<T>(HttpMethod.Post);
        }

        /// <summary>
        /// Make a PUT request to the specified URL
        /// </summary>
        /// <typeparam name="T">The type of data expected to be returned</typeparam>
        /// <param name="requestService">The request service to use when sending requests</param>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="body">The body of the content to send</param>
        /// <returns>The response from the server</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestService"/> or <paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public static async Task<ApiResponseJson<T>> PutAsync<T>(this IHttpRequestService requestService, string url, object body)
        {
            if (requestService is null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            return await PrepareRequest(requestService, url, body)
                .SendWithJsonResponse<T>(HttpMethod.Put);
        }

        /// <summary>
        /// Make a PATCH request to the specified URL
        /// </summary>
        /// <typeparam name="T">The type of data expected to be returned</typeparam>
        /// <param name="requestService">The request service to use when sending requests</param>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="body">The body of the content to send</param>
        /// <returns>The response from the server</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestService"/> or <paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public static async Task<ApiResponseJson<T>> PatchAsync<T>(this IHttpRequestService requestService, string url, object body)
        {
            if (requestService is null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            return await PrepareRequest(requestService, url, body)
                .SendWithJsonResponse<T>("PATCH");
        }

        /// <summary>
        /// Make a DELETE request to the specified URL
        /// </summary>
        /// <typeparam name="T">The type of data expected to be returned</typeparam>
        /// <param name="requestService">The request service to use when sending requests</param>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="body">The body of the content to send</param>
        /// <returns>The response from the server</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestService"/> or <paramref name="url"/> is <see langword="null"/></exception>
        /// <exception cref="UriFormatException"><paramref name="url"/> is an invalid Uri</exception>
        public static async Task<ApiResponseJson<T>> DeleteAsync<T>(this IHttpRequestService requestService, string url, object body)
        {
            if (requestService is null)
            {
                throw new ArgumentNullException(nameof(requestService));
            }

            return await PrepareRequest(requestService, url, body)
                .SendWithJsonResponse<T>(HttpMethod.Delete);
        }

        private static FluentRequest PrepareRequest(IHttpRequestService requestService, string url, object body = null)
        {
            Debug.Assert(requestService != null);

            var request = requestService.CreateRequest();
            request.UseUrl(url);

            if (body is null)
            {
                return request;
            }

            return request.WithBody().Json(body);
        }
    }
}
