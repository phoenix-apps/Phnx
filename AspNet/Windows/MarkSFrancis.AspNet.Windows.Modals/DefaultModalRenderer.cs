using System.Text;
using System.Web;
using MarkSFrancis.AspNet.Windows.Extensions;

namespace MarkSFrancis.AspNet.Windows.Modals
{
    public class DefaultModalRenderer : IModalRenderer<ModalViewModal>
    {
        public IHtmlString RenderHtml(ModalViewModal messageToRender)
        {
            if (messageToRender == null)
            {
                return new HtmlString("");
            }

            StringBuilder html = new StringBuilder();

            var modalStart = @"
    <div id=""" + messageToRender.Guid.HtmlSanitise() + @""" class=""modal fade"" role=""dialog"">
        <div class=""modal-dialog"">
            <!-- Modal content-->
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">";

            html.Append(modalStart);

            html.Append(messageToRender.Heading);

            var headerHtml = @"</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    " + messageToRender.FontAwesomeIconHtml + @"
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

            return new HtmlString(html.ToString());
        }

        public IHtmlString RenderJs(ModalViewModal messageViewModal)
        {
            var html = @"$('#" + messageViewModal.Guid.HtmlSanitise() + @"').modal('show');";
            return new HtmlString(html);
        }
    }
}
