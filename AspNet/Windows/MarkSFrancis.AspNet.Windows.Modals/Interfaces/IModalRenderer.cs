using System.Web;

namespace MarkSFrancis.AspNet.Windows.Modals.Interfaces
{
    public interface IModalRenderer<in TModal> where TModal : IModalModel
    {
        IHtmlString RenderHtml(TModal modal);

        IHtmlString RenderJs(TModal modal);
    }
}
