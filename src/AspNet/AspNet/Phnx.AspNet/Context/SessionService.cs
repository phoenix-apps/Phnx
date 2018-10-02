using MarkSFrancis;
using Phnx.AspNet.Context.Interfaces;
using System.Web;
using System.Web.SessionState;

namespace Phnx.AspNet.Context
{
    /// <summary>
    /// A service for managing the current <see cref="HttpContext"/> session
    /// </summary>
    public class SessionService : ISessionService
    {
        /// <summary>
        /// The current <see cref="HttpContext"/>
        /// </summary>
        protected HttpContext Context => HttpContext.Current;

        /// <summary>
        /// The current request's session
        /// </summary>
        protected HttpSessionState Session => Context.Session;

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

            return (T)Session[key];
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

            Session[key] = value;
        }
    }
}
