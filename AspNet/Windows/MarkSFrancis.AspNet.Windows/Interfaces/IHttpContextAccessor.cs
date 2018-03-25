using System.Web;

namespace MarkSFrancis.AspNet.Windows.Interfaces
{
    /// <summary>
    /// Provides a way to access the current <see cref="System.Web.HttpContext"/>, so that the <see cref="System.Web.HttpContext"/> can be injected as a dependancy
    /// </summary>
    public interface IHttpContextAccessor
    {
        /// <summary>
        /// Get the current <see cref="System.Web.HttpContext"/>
        /// </summary>
        HttpContext HttpContext { get; }
    }
}
