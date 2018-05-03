using MarkSFrancis.Web.Models.Request;
using MarkSFrancis.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkSFrancis.Web.Tests.Fluent
{
    public class HttpRequestServiceMock : IHttpRequestService
    {
        public ApiRequestMessage Request { get; private set; }

        public HttpRequestServiceMock()
        {
        }

        public Task<HttpResponseMessage> SendAsync(ApiRequestMessage request)
        {
            Request = request;
            return Task.FromResult<HttpResponseMessage>(null);
        }
    }
}
