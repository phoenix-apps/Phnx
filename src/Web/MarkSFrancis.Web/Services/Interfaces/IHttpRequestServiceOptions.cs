using System.Net.Http;

namespace MarkSFrancis.Web.Services.Interfaces
{
    /// <summary>
    /// Options for the <see cref="IHttpRequestService"/>, used for dependancy injection
    /// </summary>
    public interface IHttpRequestServiceOptions
    {
        /// <summary>
        /// The default <see cref="System.Net.Http.HttpClient"/> to use when formulating requests
        /// </summary>
        HttpClient HttpClient { get; set; }

        /// <summary>
        /// Set the default <see cref="System.Net.Http.HttpClient"/>
        /// </summary>
        /// <param name="client">The client to use</param>
        HttpRequestServiceOptions SetDefaultHttpClient(HttpClient client);
    }
}