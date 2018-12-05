using System;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Interprets the ETags in a request to check before performing various data operations
    /// </summary>
    public interface IETagRequestService
    {
        /// <summary>
        /// Get whether a data model should be deleted
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be deleted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        bool ShouldDelete(object data);

        /// <summary>
        /// Get whether a saved data model should be returned, or if it should simply give the user 
        /// </summary>
        /// <param name="savedData">The data model to check</param>
        /// <returns>Whether the data model should be loaded</returns>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        bool ShouldGetSingle(object savedData);

        /// <summary>
        /// Get whether a data model should be updated
        /// </summary>
        /// <param name="savedData">The data model to check</param>
        /// <returns>Whether the data model should be updated</returns>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        bool ShouldUpdate(object savedData);
    }
}