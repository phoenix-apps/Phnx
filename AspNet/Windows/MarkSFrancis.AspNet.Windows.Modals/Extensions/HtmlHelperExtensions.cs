using MarkSFrancis.AspNet.Windows.Extensions;
using MarkSFrancis.AspNet.Windows.Modals.Interfaces;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MarkSFrancis.AspNet.Windows.Modals.Extensions
{
    /// <summary>
    /// Extensions for <see cref="HtmlHelper"/> related to modals
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Render all session modals using a partial view
        /// </summary>
        /// <typeparam name="TModal">The type of modal to render</typeparam>
        /// <param name="helper">The <see cref="HtmlHelper"/> to extend rendering</param>
        /// <param name="modalManager">The modal manager which contains all the modals to render</param>
        /// <param name="partialViewName">The name of the partial view to use when rendering</param>
        /// <param name="clearModalsAfterRendering">Whether to clear the modals from the session after they have all been rendered</param>
        /// <returns>All the modals rendered as HTML</returns>
        public static IHtmlString RenderModals<TModal>(this HtmlHelper helper, IModalManager<TModal> modalManager, string partialViewName, bool clearModalsAfterRendering = true) where TModal : IModalViewModel
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

            if (clearModalsAfterRendering)
            {
                modalManager.Clear();
            }

            return content.ToHtmlString();
        }
    }
}
