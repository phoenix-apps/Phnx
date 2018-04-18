using System.Web;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public abstract class BaseContextMetaService
    {
        protected HttpContext Context => HttpContext.Current;

        protected HttpRequest Request
        {
            get
            {
                if (Context == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                return Context.Request;
            }
        }

        protected HttpResponse Response
        {
            get
            {
                if (Context == null)
                {
                    throw ErrorFactory.Default.HttpContextRequired();
                }

                return Context.Response;
            }
        }
    }
}
