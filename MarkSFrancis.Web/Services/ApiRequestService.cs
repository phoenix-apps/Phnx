using MarkSFrancis.Web.Models.Request;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Services
{
    /// <summary>
    /// The internal request engine that manages sending web requests
    /// </summary>
    internal static class ApiRequestService
    {
        // One instance of HttpClient per application (https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/#problem-description)

        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(120)
        };

        /// <summary>
        /// Send the <see cref="ApiRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        public static Task<HttpResponseMessage> SendAsync(ApiRequestMessage request)
        {
            return HttpClient.SendAsync(request.ToRequestMessage());
        }
    }
}
