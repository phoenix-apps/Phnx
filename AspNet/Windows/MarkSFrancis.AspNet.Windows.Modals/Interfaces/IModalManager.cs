using System.Collections.Generic;
using System.Web;

namespace MarkSFrancis.AspNet.Windows.Modals.Interfaces
{
    public interface IModalManager<TModal> where TModal : IModalModel
    {
        List<TModal> Modals { get; }
        void Clear();

        IHtmlString RenderHtml();
        IHtmlString RenderJs();
    }
}