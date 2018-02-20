using System.Web;

namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public abstract class BaseContextService
    {
        protected HttpRequestBase Request { get; }
        public HttpResponseBase Response { get; }

        protected BaseContextService(HttpRequestBase request, HttpResponseBase response)
        {
            Request = request;
            Response = response;
        }
    }
}
