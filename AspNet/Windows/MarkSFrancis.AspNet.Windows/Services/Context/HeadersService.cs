using System.Web;

namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public class HeadersService : BaseContextService
    {
        public HeadersService(HttpRequestBase request, HttpResponseBase response) : base(request, response)
        {
        }

        protected string Get(string key)
        {
            return Request.Headers[key];
        }

        protected void Set(string key, string value)
        {
            Response.Headers[key] = value;
        }
    }
}
