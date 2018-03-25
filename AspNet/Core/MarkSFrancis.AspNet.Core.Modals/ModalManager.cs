using System.Collections.Generic;
using System.Text;
using MarkSFrancis.AspNet.Core.Context.Interfaces;
using MarkSFrancis.AspNet.Core.Modals.Interfaces;
using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Modals
{
    public class ModalManager<TModal> : IModalManager<TModal> where TModal : IModalModel
    {
        private readonly ISessionService _session;
        public const string SessionMessageKey = "ModalMessage";
        
        protected IModalRenderer<TModal> _renderer { get; }

        public ModalManager(ISessionService session, IModalRenderer<TModal> renderer)
        {
            _session = session;
            _renderer = renderer;
        }

        public List<TModal> Modals
        {
            get => _session.Get<List<TModal>>(SessionMessageKey);
            private set => _session.Set(SessionMessageKey, value);
        }

        public void Clear()
        {
            Modals = null;
        }

        public HtmlString RenderHtml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var modal in Modals)
            {
                sb.Append(_renderer.RenderHtml(modal));
            }

            return new HtmlString(sb.ToString());
        }

        public HtmlString RenderJs()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var modal in Modals)
            {
                sb.Append(_renderer.RenderJs(modal));
            }

            return new HtmlString(sb.ToString());
        }
    }
}