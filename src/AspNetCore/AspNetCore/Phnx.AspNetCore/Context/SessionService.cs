using Microsoft.AspNetCore.Http;
using Phnx.AspNetCore.Context.Interfaces;
using Phnx.Serialization.Extensions;
using System;
using System.Collections.Generic;

namespace Phnx.AspNetCore.Context
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
                string msg = ErrorMessage.Factory.HttpContextRequired();
                throw new NullReferenceException(msg);
            }

            if (!Session.TryGetValue(key, out byte[] sessionBytes))
            {
                throw new KeyNotFoundException($"The key \"{key}\" was not found in the session");
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
                string msg = ErrorMessage.Factory.HttpContextRequired();

                throw new NullReferenceException(msg);
            }

            var valueBytes = value.Serialize();
            Session.Set(key, valueBytes);
        }
    }
}
