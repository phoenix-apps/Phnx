using Phnx.Web.Services.Interfaces;
using System.Net.Http;

namespace Phnx.Web.Services
{
    /// <summary>
    /// Options for the <see cref="HttpRequestService"/>, used for dependancy injection
    /// </summary>
    public class HttpRequestServiceOptions : IHttpRequestServiceOptions
    {
        /// <summary>
        /// Set the default <see cref="System.Net.Http.HttpClient"/>
        /// </summary>
        /// <param name="client">The client to use</param>
        public HttpRequestServiceOptions SetDefaultHttpClient(HttpClient client)
        {
            HttpClient = client;

            return this;
        }

        /// <summary>
        /// The default <see cref="System.Net.Http.HttpClient"/> to use when formulating requests
        /// </summary>
        public HttpClient HttpClient { get; set; }
    }
}
