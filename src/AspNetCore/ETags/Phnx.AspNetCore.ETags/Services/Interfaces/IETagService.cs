using Microsoft.AspNetCore.Mvc;
using Phnx.AspNetCore.ETags.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Provides an interface for managing E-Tags and caching
    /// </summary>
    public interface IETagService
    {
        /// <summary>
        /// Append the relevant E-Tag for the data model to the response
        /// </summary>
        /// <param name="resource">The data model for which to generate the E-Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="resource"/> is <see langword="null"/></exception>
        void AddETagForModelToResponse(object resource);

        /// <summary>
        /// Append the e-tag to the response
        /// </summary>
        /// <param name="etag">The e-tag to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="etag"/> is <see langword="null"/></exception>
        void AddETagToResponse(string etag);

        /// <summary>
        /// Check whether a sent E-Tag matches a given data model using the If-Match header
        /// </summary>
        /// <param name="resource">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="resource"/> is <see langword="null"/></exception>
        ETagMatchResult CheckIfMatch(object resource);

        /// <summary>
        /// Check whether a sent E-Tag does not match a given data model using the If-None-Match header
        /// </summary>
        /// <param name="resource">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="resource"/> is <see langword="null"/></exception>
        ETagMatchResult CheckIfNoneMatch(object resource);

        /// <summary>
        /// Create the E-Tag response for a match
        /// </summary>
        /// <returns>The E-Tag response for a match</returns>
        StatusCodeResult CreateMatchResponse();

        /// <summary>
        /// Create the E-Tag response for a do not match
        /// </summary>
        /// <returns>The E-Tag response for a do not match</returns>
        StatusCodeResult CreateDoNotMatchResponse();

        /// <summary>
        /// Get the strong e-tag for <paramref name="data"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="data">The data to load the strong e-tag for</param>
        /// <param name="etag"><see langword="null"/> if a strong e-tag could not be loaded, otherwise, the strong e-tag that represents <paramref name="data"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        bool TryGetStrongETag(object data, out string etag);

        /// <summary>
        /// Generates a weak ETag for <paramref name="o"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="o">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="o"/></returns>
        string GetWeakETag(object o);
    }
}
