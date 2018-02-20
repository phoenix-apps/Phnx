using System.Web.Mvc;

namespace MarkSFrancis.AspNet.Windows.Extensions
{
    public static class TagBuilderExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tag, TagRenderMode renderMode)
        {
            return MvcHtmlString.Create(tag.ToString(renderMode));
        }
    }
}