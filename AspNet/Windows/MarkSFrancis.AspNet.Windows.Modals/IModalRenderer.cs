using System.Web;

namespace MarkSFrancis.AspNet.Windows.Modals
{
    public interface IModalRenderer<TModal> where TModal : IModal
    {
        IHtmlString RenderHtml(TModal modal);

        IHtmlString RenderJs(TModal modal);
    }
}
