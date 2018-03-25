using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    public abstract class BaseContextMetaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected HttpContext Context => _httpContextAccessor.HttpContext;

        protected HttpRequest Request
        {
            get
            {
                if (Context == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                return Context.Request;
            }
        }

        protected HttpResponse Response
        {
            get
            {
                if (Context == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                return Context.Response;
            }
        }

        protected BaseContextMetaService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
