using Microsoft.AspNetCore.Http;
using System;

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
                    string msg = ErrorMessage.Factory.HttpContextRequired();
                    throw new NullReferenceException(msg);
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
                    string err = ErrorMessage.Factory.HttpContextRequired();
                    throw new NullReferenceException(err);
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
