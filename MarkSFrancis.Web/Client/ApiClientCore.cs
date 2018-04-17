using MarkSFrancis.Web.Models.Request;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Client
{
    /// <summary>
    /// The internal request engine that manages sending web requests
    /// </summary>
    internal class ApiClientCore
    {
        private readonly HttpClient _httpClient;

        public ApiClientCore()
        {
            // One instance of HttpClient per application (https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/#problem-description)

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(120)
            };
        }

        /// <summary>
        /// Send the <see cref="ApiRequestMessage"/> over HTTP to an URL
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        public Task<HttpResponseMessage> SendAsync(ApiRequestMessage request)
        {
            return _httpClient.SendAsync(request.ToRequestMessage());
        }
    }
}
