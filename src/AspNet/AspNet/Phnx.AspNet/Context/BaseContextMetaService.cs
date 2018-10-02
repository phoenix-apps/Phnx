using MarkSFrancis;
using System.Web;

namespace Phnx.AspNet.Context
{
    /// <summary>
    /// A context meta service with helpers for access the current <see cref="HttpContext"/>, response and request
    /// </summary>
    public abstract class BaseContextMetaService
    {
        /// <summary>
        /// Get the current <see cref="HttpContext"/>
        /// </summary>
        protected HttpContext Context => HttpContext.Current;

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
    }
}
