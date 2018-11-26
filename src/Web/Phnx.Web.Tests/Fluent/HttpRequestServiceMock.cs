using Phnx.Web.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Tests.Fluent
{
    public class HttpRequestServiceMock : HttpRequestService
    {
        public HttpRequestMessage Request { get; private set; }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            Request = request;
            return Task.FromResult<HttpResponseMessage>(null);
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpClient httpClient)
        {
            Request = request;
            return Task.FromResult<HttpResponseMessage>(null);
        }
    }
}
