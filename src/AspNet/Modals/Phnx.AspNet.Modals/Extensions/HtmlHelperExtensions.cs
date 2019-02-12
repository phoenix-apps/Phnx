using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Phnx.AspNet.Modals
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
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> or <paramref name="modalManager"/> or <paramref name="partialViewName"/> is <see langword="null"/></exception>
        public static IHtmlString RenderModals<TModal>(this HtmlHelper helper, IModalManager<TModal> modalManager, string partialViewName, bool clearModalsAfterRendering = true) where TModal : IModalViewModel
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }
            if (modalManager is null)
            {
                throw new ArgumentNullException(nameof(modalManager));
            }
            if (partialViewName is null)
            {
                throw new ArgumentNullException(nameof(partialViewName));
            }

            var content = new StringBuilder();

            foreach (TModal modal in modalManager.Get())
            {
                content.Append(helper.Partial(partialViewName, modal));
            }

            if (clearModalsAfterRendering)
            {
                modalManager.Clear();
            }

            return new HtmlString(content.ToString());
        }
    }
}
