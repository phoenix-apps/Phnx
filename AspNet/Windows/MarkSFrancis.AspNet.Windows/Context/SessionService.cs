using System.Web;
using System.Web.SessionState;
using MarkSFrancis.AspNet.Windows.Context.Interfaces;

namespace MarkSFrancis.AspNet.Windows.Context
{
    public class SessionService : ISessionService
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        protected HttpContext HttpContext => _httpContextAccessor.HttpContext;

        protected HttpSessionState Session => HttpContext.Session;

        public SessionService(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
