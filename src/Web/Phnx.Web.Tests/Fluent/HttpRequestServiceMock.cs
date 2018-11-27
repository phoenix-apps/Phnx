using Phnx.Web.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Tests.Fluent
{
    public class HttpRequestServiceMock : HttpRequestService
    {
        public HttpRequestServiceMock() : this(null)
        {
        }

        public HttpRequestServiceMock(HttpResponseMessage response)
        {
            Response = response;
        }

        public HttpRequestMessage Request { get; private set; }

        public HttpResponseMessage Response { get; }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            Request = request;

            return Task.FromResult(Response);
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpClient httpClient)
        {
            return SendAsync(request);
        }
    }
}
