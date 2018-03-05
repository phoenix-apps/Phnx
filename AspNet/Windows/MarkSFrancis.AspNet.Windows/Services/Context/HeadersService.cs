using System.Collections.Specialized;
using System.Web;

namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public class HeadersService : BaseContextService, IContextMetaService<string, string>
    {
        protected NameValueCollection RequestHeaders => Request.Headers;
        protected NameValueCollection ResponseHeaders => Request.Headers;

        public HeadersService(HttpRequestBase request, HttpResponseBase response) : base(request, response)
        {
        }

        public string Get(string key)
        {
            return RequestHeaders[key];
        }

        public void Set(string key, string value)
        {
            ResponseHeaders[key] = value;
        }
    }
}
