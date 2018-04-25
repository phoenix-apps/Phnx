using MarkSFrancis.Web.Models.Request;
using MarkSFrancis.Web.Services.Interfaces;
using System;
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
        /// Send the <see cref="ApiRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        public Task<HttpResponseMessage> SendAsync(ApiRequestMessage request)
        {
            return HttpClient.SendAsync(request.ToRequestMessage());
        }
    }
}
