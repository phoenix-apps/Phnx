using MarkSFrancis.AspNet.Core.Rest.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarkSFrancis.AspNet.Core.Rest.Services.Interfaces
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
        void AddETagToResponse(IResourceDataModel resource);

        /// <summary>
        /// Check whether a sent E-Tag matches a given data model using the If-Match header
        /// </summary>
        /// <param name="resource">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is a match</returns>
        bool CheckIfMatch(IResourceDataModel resource);

        /// <summary>
        /// Check whether a sent E-Tag does not match a given data model using the If-None-Match header
        /// </summary>
        /// <param name="resource">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        bool CheckIfNoneMatch(IResourceDataModel resource);

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
    }
}
