using System.Text;
using MarkSFrancis.AspNet.Core.Extensions;
using MarkSFrancis.AspNet.Core.Modals.Interfaces;
using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Modals
{
    public class DefaultModalRenderer : IModalRenderer<ModalModel>
    {
        public virtual HtmlString RenderHtml(ModalModel messageToRender)
        {
            if (messageToRender == null)
            {
                return "".ToHtmlString();
            }

            StringBuilder html = new StringBuilder();

            var modalStart = @"
    <div id=""" + messageToRender.Id.HtmlSanitise() + @""" class=""modal fade"" role=""dialog"">
        <div class=""modal-dialog"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">";

            html.Append(modalStart);

            html.Append(messageToRender.Heading);

            var headerHtml = @"</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    " + messageToRender.IconHtml + @"
                </button>";

            html.Append(headerHtml);

            string modalBodyStart = @"</div>
                <div class=""modal-body"">
                    <p>";

            html.Append(modalBodyStart);

            html.Append(messageToRender.Body);

            string modalEnd = @"</p>
                </div>
                <div class=""modal-footer"">
                    <button type = ""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                </div>
            </div>
        </div>
    </div>
";

            html.Append(modalEnd);

            return html.ToHtmlString();
        }

        public virtual HtmlString RenderJs(ModalModel messageViewModal)
        {
            var html = @"$('#" + messageViewModal.Id.HtmlSanitise() + @"').modal('show');";
            return html.ToHtmlString();
        }
    }
}
