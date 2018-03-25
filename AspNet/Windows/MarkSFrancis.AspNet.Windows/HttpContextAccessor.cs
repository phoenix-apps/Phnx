using System.Web;
using MarkSFrancis.AspNet.Windows.Interfaces;

namespace MarkSFrancis.AspNet.Windows
{
    /// <summary>
    /// Provides a way to access the current <see cref="System.Web.HttpContext"/>, so that the <see cref="System.Web.HttpContext"/> can be injected as a dependancy
    /// </summary>
    public class HttpContextAccessor : IHttpContextAccessor
    {
        /// <summary>
        /// Get the current <see cref="System.Web.HttpContext"/>
        /// </summary>
        public HttpContext HttpContext => HttpContext.Current;
    }
}
