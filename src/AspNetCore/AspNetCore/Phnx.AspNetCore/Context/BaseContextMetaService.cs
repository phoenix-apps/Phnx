using MarkSFrancis;
using Microsoft.AspNetCore.Http;

namespace Phnx.AspNetCore.Context
{
    /// <summary>
    /// A context meta service with helpers for access the current <see cref="HttpContext"/>, response and request
    /// </summary>
    public abstract class BaseContextMetaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Get the current <see cref="HttpContext"/>
        /// </summary>
        protected HttpContext Context => _httpContextAccessor.HttpContext;

        /// <summary>
        /// Get the current <see cref="HttpContext.Request"/>
        /// </summary>
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

        /// <summary>
        /// Get the current <see cref="HttpContext.Response"/>
        /// </summary>
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

        /// <summary>
        /// Create a new <see cref="BaseContextMetaService"/> using a <see cref="IHttpContextAccessor"/> to get the current <see cref="HttpContext"/>
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        protected BaseContextMetaService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
