using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Phnx.Web.Tests
{
    public class HttpMessageHandlerFake : HttpMessageHandler
    {
        public HttpRequestMessage LastMessageSent { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastMessageSent = request;
            return Task.FromResult(new HttpResponseMessage());
        }
    }
}
