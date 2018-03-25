using MarkSFrancis.AspNet.Core.Context.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    public class HeadersService : BaseContextMetaService, IHeadersService
    {
        protected IHeaderDictionary RequestHeaders => Request.Headers;
        protected IHeaderDictionary ResponseHeaders => Request.Headers;

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
