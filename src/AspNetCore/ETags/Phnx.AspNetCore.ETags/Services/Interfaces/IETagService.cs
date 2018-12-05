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
        /// <param name="dataModel">The database model to compare the request's ETag to</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException">Request ETag is set, and <paramref name="dataModel"/> is <see langword="null"/></exception>
        ETagMatchResult CheckETagsForModel(string requestETag, object dataModel);

        /// <summary>
        /// Get the strong ETag for <paramref name="data"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="data">The data to load the strong ETag for</param>
        /// <param name="eTag"><see langword="null"/> if a strong ETag could not be loaded, otherwise, the strong ETag that represents <paramref name="data"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        bool TryGetStrongETagForModel(object data, out string eTag);

        /// <summary>
        /// Generates a weak ETag for <paramref name="data"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="data">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="data"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        string GetWeakETagForModel(object data);
    }
}
