using Microsoft.AspNetCore.Mvc;
using System;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Formulates various e-tag related responses
    /// </summary>
    public interface IETagResponseService
    {
        /// <summary>
        /// Add a strong e-tag if <paramref name="savedData"/> supports it, or a weak tag if it does not
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        void AddBestETagForModelToResponse(object savedData);

        /// <summary>
        /// Add a weak e-tag for <paramref name="savedData"/>
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        void AddWeakETagForModelToResponse(object savedData);

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
        /// Add a strong e-tag if <paramref name="savedData"/> supports it
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        bool TryAddStrongETagForModelToResponse(object savedData);

        /// <summary>
        /// Append the e-tag to the response. Weak e-tags should be formatted as W/"etag", and strong e-tags should be formatted as "etag"
        /// </summary>
        /// <param name="etag">The e-tag to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="etag"/> is <see langword="null"/></exception>
        void AddETagToResponse(string etag);
    }
}