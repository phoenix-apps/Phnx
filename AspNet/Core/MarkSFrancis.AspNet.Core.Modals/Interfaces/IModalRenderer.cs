using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Modals.Interfaces
{
    public interface IModalRenderer<in TModal> where TModal : IModalModel
    {
        HtmlString RenderHtml(TModal modal);

        HtmlString RenderJs(TModal modal);
    }
}
