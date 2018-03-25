using System.Collections.Generic;
using System.Text;
using System.Web;
using MarkSFrancis.AspNet.Windows.Context.Interfaces;
using MarkSFrancis.AspNet.Windows.Modals.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Modals
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

        public IHtmlString RenderHtml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var modal in Modals)
            {
                sb.Append(_renderer.RenderHtml(modal));
            }

            return new HtmlString(sb.ToString());
        }

        public IHtmlString RenderJs()
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