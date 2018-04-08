using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MarkSFrancis.AspNet.Windows.Extensions;
using MarkSFrancis.AspNet.Windows.Modals.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Modals.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderModals<TModal>(this HtmlHelper helper, IModalManager<TModal> modalManager, string partialViewName) where TModal : IModalViewModel
        {
            if (string.IsNullOrWhiteSpace(partialViewName))
            {
                ErrorFactory.Default.ArgumentNull(nameof(partialViewName));
            }

            StringBuilder content = new StringBuilder();

            foreach (var modal in modalManager.Modals)
            {
                content.Append(helper.Partial(partialViewName, modal));
            }

            return content.ToHtmlString();
        }
    }
}
