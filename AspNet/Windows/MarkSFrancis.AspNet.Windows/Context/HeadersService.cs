using MarkSFrancis.AspNet.Windows.Context.Interfaces;
using System.Collections.Specialized;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public class HeadersService : BaseContextMetaService, IHeadersService
    {
        protected NameValueCollection RequestHeaders => Request.Headers;
        protected NameValueCollection ResponseHeaders => Request.Headers;

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
