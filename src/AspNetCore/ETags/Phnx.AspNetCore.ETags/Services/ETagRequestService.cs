using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Interprets the e-tags in a request to check before performing various data operations
    /// </summary>
    public class ETagRequestService : IETagRequestService
    {
        /// <summary>
        /// The IfNoneMatch header's key
        /// </summary>
        public const string IfNoneMatchKey = "If-None-Match";

        /// <summary>
        /// The IfMatch header's key
        /// </summary>
        public const string IfMatchKey = "If-Match";

        /// <summary>
        /// The service for reading the E-Tags in the headers of the request
        /// </summary>
        public IETagService ETagService { get; }

        /// <summary>
        /// The action context accessor for accessing the current request and response headers
        /// </summary>
        public IActionContextAccessor ActionContext { get; }

        /// <summary>
        /// The headers in the request
        /// </summary>
        public IHeaderDictionary RequestHeaders => ActionContext.ActionContext.HttpContext.Request.Headers;

        /// <summary>
        /// Create a new <see cref="ETagRequestService"/>
        /// </summary>
        /// <param name="eTagService">The E-Tag reader</param>
        /// <exception cref="ArgumentNullException"><paramref name="eTagService"/> is <see langword="null"/></exception>
        public ETagRequestService(IETagService eTagService)
        {
            ETagService = eTagService ?? throw new ArgumentNullException(nameof(eTagService));
        }

        /// <summary>
        /// Get whether a saved data model should be returned, or if it should simply give the user 
        /// </summary>
        /// <param name="savedData">The data model to check</param>
        /// <returns>Whether the data model should be loaded</returns>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public bool ShouldGetSingle(object savedData)
        {
            if (!RequestHeaders.TryGetValue(IfNoneMatchKey, out var etags) || etags.Count == 0)
            {
                // ETags not in use
                return true;
            }

            var result = ETagService.CheckETagsForModel(etags[0], savedData);

            return
                result == ETagMatchResult.ETagNotInRequest ||
                (result & ETagMatchResult.DoNotMatch) != 0;
        }

        /// <summary>
        /// Get whether a data model should be updated
        /// </summary>
        /// <param name="savedData">The data model to check</param>
        /// <returns>Whether the data model should be updated</returns>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public bool ShouldUpdate(object savedData)
        {
            if (!RequestHeaders.TryGetValue(IfMatchKey, out var etags) || etags.Count == 0)
            {
                // ETags not in use
                return true;
            }

            var result = ETagService.CheckETagsForModel(etags[0], savedData);

            return
                result == ETagMatchResult.ETagNotInRequest ||
                (result & ETagMatchResult.Match) != 0;
        }

        /// <summary>
        /// Get whether a data model should be deleted
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be deleted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public bool ShouldDelete(object data)
            => ShouldUpdate(data);
    }
}
