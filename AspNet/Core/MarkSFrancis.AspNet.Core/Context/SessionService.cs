using MarkSFrancis.AspNet.Core.Context.Interfaces;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Serialization.Extensions;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected HttpContext HttpContext => _httpContextAccessor.HttpContext;

        protected ISession Session => HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T Get<T>(string key)
        {
            if (HttpContext == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            if (!Session.TryGetValue(key, out byte[] _sessionBytes))
            {
                throw ErrorFactory.Default.KeyNotFound(key);
            }

            return _sessionBytes.Deserialize<T>();
        }

        public void Set<T>(string key, T value)
        {
            if (HttpContext == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            var valueBytes = value.Serialize();
            Session.Set(key, valueBytes);
        }
    }
}
