using System.Web.Mvc;

namespace Phnx.AspNet.Extensions
{
    /// <summary>
    /// Extensions for <see cref="TagBuilder"/>
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Render the current <see cref="TagBuilder"/> to a <see cref="MvcHtmlString"/>
        /// </summary>
        /// <param name="tag">The <see cref="TagBuilder"/> to render</param>
        /// <param name="renderMode">The <see cref="TagRenderMode"/> to use when rendering the current <see cref="TagBuilder"/></param>
        /// <returns><paramref name="tag"/> rendered to a <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tag, TagRenderMode renderMode)
        {
            return MvcHtmlString.Create(tag.ToString(renderMode));
        }
    }
}