using Microsoft.AspNetCore.Mvc;
using System;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Formulates various ETag related responses
    /// </summary>
    public interface IETagResponseService
    {
        /// <summary>
        /// Add a strong ETag if <paramref name="savedData"/> supports it, or a weak tag if it does not
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        void AddStrongestETagForModel(object savedData);

        /// <summary>
        /// Add a weak ETag for <paramref name="savedData"/>
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        void AddWeakETagForModel(object savedData);

        /// <summary>
        /// Create a response describing that the data has been changed
        /// </summary>
        /// <returns>A response describing that the data has been changed</returns>
        StatusCodeResult CreateDataHasChangedResponse();

        /// <summary>
        /// Create a response describing that the data has not been changed
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        StatusCodeResult CreateDataHasNotChangedResponse();

        /// <summary>
        /// Add a strong ETag if <paramref name="savedData"/> supports it
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        bool TryAddStrongETagForModel(object savedData);

        /// <summary>
        /// Append the ETag to the response. Weak ETags should be formatted as W/"eTag", and strong ETags should be formatted as "eTag"
        /// </summary>
        /// <param name="eTag">The ETag to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="eTag"/> is <see langword="null"/></exception>
        void AddETag(string eTag);
    }
}