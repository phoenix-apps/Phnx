using System.Web.Mvc;

namespace MarkSFrancis.AspNet.Windows.Extensions
{
    /// <summary>
    /// Extensions for <see cref="TagBuilder"/>
    /// </summary>
    public static class TagBuilderExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tag, TagRenderMode renderMode)
        {
            return MvcHtmlString.Create(tag.ToString(renderMode));
        }
    }
}