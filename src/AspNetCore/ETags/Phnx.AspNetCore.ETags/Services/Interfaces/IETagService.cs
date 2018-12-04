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
        /// Check whether an e-tag matches a given data model
        /// </summary>
        /// <param name="requestETag">The e-tags from the request</param>
        /// <param name="dataModel">The database model to compare the request's e-tag to</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException">Request e-tag is set, and <paramref name="dataModel"/> is <see langword="null"/></exception>
        ETagMatchResult CheckETagsForModel(string requestETag, object dataModel);

        /// <summary>
        /// Get the strong e-tag for <paramref name="data"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="data">The data to load the strong e-tag for</param>
        /// <param name="etag"><see langword="null"/> if a strong e-tag could not be loaded, otherwise, the strong e-tag that represents <paramref name="data"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        bool TryGetStrongETagForModel(object data, out string etag);

        /// <summary>
        /// Generates a weak ETag for <paramref name="data"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="data">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="data"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        string GetWeakETagForModel(object data);
    }
}
