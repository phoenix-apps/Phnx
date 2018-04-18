using MarkSFrancis.AspNet.Windows.Context.Interfaces;
using System.Web;
using System.Web.SessionState;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public class SessionService : ISessionService
    {
        protected HttpContext HttpContext => HttpContext.Current;

        protected HttpSessionState Session => HttpContext.Session;

        public T Get<T>(string key)
        {
            if (HttpContext == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            return (T)Session[key];
        }

        public void Set<T>(string key, T value)
        {
            if (HttpContext == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            Session[key] = value;
        }
    }
}
