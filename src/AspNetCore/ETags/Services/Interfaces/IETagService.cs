using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Provides an interface for managing ETags and caching
    /// </summary>
    public interface IETagService
    {
        /// <summary>
        /// Check whether an ETag matches a given data model
        /// </summary>
        /// <param name="requestETag">The ETags from the request</param>
        /// <param name="model">The database model to compare the request's ETag to</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException">Request ETag is set, and <paramref name="model"/> is <see langword="null"/></exception>
        ETagMatchResult CheckETags(string requestETag, object model);

        /// <summary>
        /// Get the strong ETag for <paramref name="model"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="model">The data to load the strong ETag for</param>
        /// <param name="eTag"><see langword="null"/> if a strong ETag could not be loaded, otherwise, the strong ETag that represents <paramref name="model"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        bool TryGetStrongETag(object model, out string eTag);

        /// <summary>
        /// Generates a weak ETag for <paramref name="model"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="model">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="model"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        string GetWeakETag(object model);
    }
}
