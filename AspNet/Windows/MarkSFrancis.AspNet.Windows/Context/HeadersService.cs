using System.Collections.Specialized;
using MarkSFrancis.AspNet.Windows.Context.Interfaces;
using MarkSFrancis.AspNet.Windows.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public class HeadersService : BaseContextMetaService, IHeadersService
    {
        protected NameValueCollection RequestHeaders => Request.Headers;
        protected NameValueCollection ResponseHeaders => Request.Headers;

        public HeadersService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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
