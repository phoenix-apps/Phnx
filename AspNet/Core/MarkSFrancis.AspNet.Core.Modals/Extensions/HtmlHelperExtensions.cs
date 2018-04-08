using MarkSFrancis.AspNet.Core.Modals.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarkSFrancis.AspNet.Core.Modals.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderModals<TModal>(this IHtmlHelper helper, IModalManager<TModal> modalManager, string partialViewName) where TModal : IModalViewModel
        {
            if (string.IsNullOrWhiteSpace(partialViewName))
            {
                ErrorFactory.Default.ArgumentNull(nameof(partialViewName));
            }

            IHtmlContentBuilder content = new HtmlContentBuilder();

            foreach (var modal in modalManager.Modals)
            {
                content.AppendHtml(helper.Partial(partialViewName, modal));
            }

            return content;
        }
    }
}
