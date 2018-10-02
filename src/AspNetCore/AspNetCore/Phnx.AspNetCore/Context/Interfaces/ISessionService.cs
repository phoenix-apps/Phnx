using Microsoft.AspNetCore.Http;

namespace Phnx.AspNetCore.Context.Interfaces
{
    /// <summary>
    /// A service for managing the current <see cref="HttpContext"/> session
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Get a session value by key from the current <see cref="HttpContext"/>
        /// </summary>
        /// <typeparam name="T">The type of the data stored in this session slot</typeparam>
        /// <param name="key">The key to the session data to get</param>
        /// <returns>The value of the specified data</returns>
        T Get<T>(string key);

        /// <summary>
        /// Set a session value by key to the current <see cref="HttpContext"/>
        /// </summary>
        /// <typeparam name="T">The type of the data to store in the session</typeparam>
        /// <param name="key">The key to the session data to set</param>
        /// <param name="value">The value to set</param>
        void Set<T>(string key, T value);
    }
}