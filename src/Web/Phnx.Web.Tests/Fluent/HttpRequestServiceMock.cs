using Phnx.Web.Fluent;
using Phnx.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Tests.Fluent
{
    public class HttpRequestServiceMock : IHttpRequestService
    {
        public HttpRequestMessage Request { get; private set; }

        public HttpClient HttpClient => null;

        public FluentRequest CreateRequest()
        {
            return new FluentRequest(this);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            Request = request;
            return Task.FromResult<HttpResponseMessage>(null);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpClient httpClient)
        {
            Request = request;
            return Task.FromResult<HttpResponseMessage>(null);
        }
    }
}
