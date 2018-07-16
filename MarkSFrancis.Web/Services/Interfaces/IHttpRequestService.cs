using MarkSFrancis.Web.Fluent;
using MarkSFrancis.Web.Models.Request;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Services.Interfaces
{
    /// <summary>
    /// The core API request engine responsible for sending web requests
    /// </summary>
    public interface IHttpRequestService
    {
        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <returns>A new fluent HTTP request</returns>
        FluentRequest NewRequest();

        /// <summary>
        /// Send the <see cref="ApiRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        Task<HttpResponseMessage> SendAsync(ApiRequestMessage request);
    }
}
