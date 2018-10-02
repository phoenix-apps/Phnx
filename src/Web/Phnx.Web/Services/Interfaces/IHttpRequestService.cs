using Phnx.Web.Fluent;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Services.Interfaces
{
    /// <summary>
    /// The core API request engine responsible for sending web requests
    /// </summary>
    public interface IHttpRequestService
    {
        /// <summary>
        /// The <see cref="System.Net.Http.HttpClient"/> responsible for sending requests
        /// </summary>
        /// <remarks>
        /// One instance of HttpClient per application (<see href="https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/#problem-description"/>)
        /// </remarks>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <returns>A new fluent HTTP request</returns>
        FluentRequest CreateRequest();

        /// <summary>
        /// Send the <see cref="HttpRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// Send the <see cref="HttpRequestMessage"/> over HTTP, with a non-default <see cref="HttpClient"/>
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="httpClient">The HTTP Client to use for the request</param>
        /// <returns>The response from the foreign URL</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpClient httpClient);
    }
}
