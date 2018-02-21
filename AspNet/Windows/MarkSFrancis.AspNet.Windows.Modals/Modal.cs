using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MarkSFrancis.AspNet.Windows.Modals
{
    public class ModalManager<TModal> where TModal : IModal
    {
        public const string SessionMessageKey = "ModalMessage";
        
        public IModalRenderer<TModal> Renderer { get; }

        public ModalManager(IModalRenderer<TModal> renderer)
        {
            Renderer = renderer;
        }

        public List<TModal> Modals
        {
            get
            {
                var curContext = HttpContext.Current;
                if (curContext == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                return (List<TModal>)curContext.Session[SessionMessageKey];
            }
            private set
            {
                var curContext = HttpContext.Current;
                if (curContext == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                HttpContext.Current.Session[SessionMessageKey] = value;
            }
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
                sb.Append(Renderer.RenderHtml(modal));
            }

            return new HtmlString(sb.ToString());
        }

        public IHtmlString RenderJs()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var modal in Modals)
            {
                sb.Append(Renderer.RenderJs(modal));
            }

            return new HtmlString(sb.ToString());
        }
    }
}