using MarkSFrancis.Web.Fluent;
using MarkSFrancis.Web.Models.Request;
using MarkSFrancis.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Services
{
    /// <summary>
    /// The api request engine that manages sending web requests
    /// </summary>
    public class HttpRequestService : IHttpRequestService
    {
        /// <summary>
        /// The <see cref="System.Net.Http.HttpClient"/> responsible for sending requests. The default timeout is 120 seconds
        /// </summary>
        /// <remarks>
        /// One instance of HttpClient per application (<see href="https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/#problem-description"/>)
        /// </remarks>
        protected static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(120)
        };

        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <returns>A new fluent HTTP request</returns>
        public FluentRequest NewRequest()
        {
            return new FluentRequest(this);
        }

        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <param name="url">The url to point the request to</param>
        /// <param name="urlSegments">Any sub-url path segments. These are escaped and then joined onto the end of the url, with a "/" between each one</param>
        /// <returns>A fluent request builder</returns>
        public FluentRequest NewRequest(string url, params string[] urlSegments)
        {
            return NewRequest()
                .UseUrl(url, urlSegments);
        }

        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <param name="url">The url to point the request to</param>
        /// <param name="urlSegments">Any sub-url path segments. These are escaped and then joined onto the end of the url, with a "/" between each one</param>
        /// <returns>A fluent request builder</returns>
        public FluentRequest NewRequest(string url, IEnumerable<string> urlSegments)
        {
            return NewRequest()
                .UseUrl(url, urlSegments);
        }

        /// <summary>
        /// Send the <see cref="ApiRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        public Task<HttpResponseMessage> SendAsync(ApiRequestMessage request)
        {
            return HttpClient.SendAsync(request.ToRequestMessage());
        }
        /// <summary>
        /// Send the <see cref="ApiRequestMessage"/> over HTTP, with a non-default <see cref="HttpClient"/>
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="httpClient">The HTTP Client to use for the request</param>
        /// <returns>The response from the foreign URL</returns>
        public Task<HttpResponseMessage> SendAsync(ApiRequestMessage request, HttpClient httpClient)
        {
            return httpClient.SendAsync(request.ToRequestMessage());
        }
    }
}
