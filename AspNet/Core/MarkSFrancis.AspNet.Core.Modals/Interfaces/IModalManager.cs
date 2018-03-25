using System.Collections.Generic;
using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Modals.Interfaces
{
    public interface IModalManager<TModal> where TModal : IModalModel
    {
        List<TModal> Modals { get; }
        void Clear();

        HtmlString RenderHtml();
        HtmlString RenderJs();
    }
}