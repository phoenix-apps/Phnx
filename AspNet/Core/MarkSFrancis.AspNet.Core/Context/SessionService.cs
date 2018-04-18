using MarkSFrancis.AspNet.Core.Context.Interfaces;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Serialization.Extensions;
using Microsoft.AspNetCore.Http;

namespace MarkSFrancis.AspNet.Core.Context
{
    /// <summary>
    /// A service for managing the current <see cref="HttpContext"/> session
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The current <see cref="HttpContext"/>
        /// </summary>
        protected HttpContext Context => _httpContextAccessor.HttpContext;

        /// <summary>
        /// The current request's <see cref="ISession"/>
        /// </summary>
        protected ISession Session => Context.Session;

        /// <summary>
        /// Create a new <see cref="SessionService"/> using a <see cref="IHttpContextAccessor"/> to access the current session
        /// </summary>
        /// <param name="httpContextAccessor">The accessor for the current <see cref="Context"/></param>
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get a session value by key from the current <see cref="HttpContext"/>
        /// </summary>
        /// <typeparam name="T">The type of the data stored in this session slot</typeparam>
        /// <param name="key">The key to the session data to get</param>
        /// <returns>The value of the specified data</returns>
        public T Get<T>(string key)
        {
            if (Context == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            if (!Session.TryGetValue(key, out byte[] sessionBytes))
            {
                throw ErrorFactory.Default.KeyNotFound(key);
            }

            return sessionBytes.Deserialize<T>();
        }

        /// <summary>
        /// Set a session value by key to the current <see cref="HttpContext"/>
        /// </summary>
        /// <typeparam name="T">The type of the data to store in the session</typeparam>
        /// <param name="key">The key to the session data to set</param>
        /// <param name="value">The value to set</param>
        public void Set<T>(string key, T value)
        {
            if (Context == null)
            {
                throw ErrorFactory.Default.HttpContextRequired();
            }

            var valueBytes = value.Serialize();
            Session.Set(key, valueBytes);
        }
    }
}
