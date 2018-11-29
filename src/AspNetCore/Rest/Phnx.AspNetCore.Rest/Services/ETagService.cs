using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using Phnx.AspNetCore.Rest.Models;
using System;
using System.Net;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Manages E-Tags and caching
    /// </summary>
    public class ETagService : IETagService
    {
        /// <summary>
        /// The ETag header's key
        /// </summary>
        public const string ETagHeaderKey = "ETag";

        /// <summary>
        /// The IfNoneMatch header's key
        /// </summary>
        public const string IfNoneMatchKey = "If-None-Match";

        /// <summary>
        /// The IfMatch header's key
        /// </summary>
        public const string IfMatchKey = "If-Match";

        /// <summary>
        /// The action context accessor for accessing the current request and response headers
        /// </summary>
        public IActionContextAccessor ActionContext { get; }

        /// <summary>
        /// The headers in the request
        /// </summary>
        public IHeaderDictionary RequestHeaders => ActionContext.ActionContext.HttpContext.Request.Headers;

        /// <summary>
        /// The headers in the response
        /// </summary>
        public IHeaderDictionary ResponseHeaders => ActionContext.ActionContext.HttpContext.Response.Headers;

        /// <summary>
        /// Create a new <see cref="ETagService"/> using a given <see cref="IActionContextAccessor"/>
        /// </summary>
        /// <param name="actionContext">The action context accessor for reading and writing E-Tag headers</param>
        /// <exception cref="ArgumentNullException"><paramref name="actionContext"/> is <see langword="null"/></exception>
        public ETagService(IActionContextAccessor actionContext)
        {
            ActionContext = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
        }

        /// <summary>
        /// Check whether a sent E-Tag does not match a given data model using the If-None-Match header.
        /// Defaults to <see langword="true"/> if the header is not present
        /// </summary>
        /// <param name="data">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public bool CheckIfNoneMatch(IResourceDataModel data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!RequestHeaders.TryGetValue(IfNoneMatchKey, out StringValues eTag) || eTag.Count == 0)
            {
                return true;
            }

            return eTag[0] != data.ConcurrencyStamp;
        }

        /// <summary>
        /// Check whether a sent E-Tag matches a given data model using the If-Match header.
        /// Defaults to <see langword="true"/> if the header is not present
        /// </summary>
        /// <param name="data">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public bool CheckIfMatch(IResourceDataModel data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!RequestHeaders.TryGetValue(IfMatchKey, out StringValues eTag) || eTag.Count == 0)
            {
                return true;
            }

            return eTag[0] == data.ConcurrencyStamp;
        }

        /// <summary>
        /// Create the E-Tag response for a match
        /// </summary>
        /// <returns>The E-Tag response for a match</returns>
        public StatusCodeResult CreateMatchResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Create the E-Tag response for a do not match
        /// </summary>
        /// <returns>The E-Tag response for a do not match</returns>
        public StatusCodeResult CreateDoNotMatchResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.PreconditionFailed);
        }

        /// <summary>
        /// Append the relevant E-Tag for the data model to the response
        /// </summary>
        /// <param name="data">The data model for which to generate the E-Tag</param>
        public void AddETagToResponse(IResourceDataModel data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataETag = data.ConcurrencyStamp;

            ResponseHeaders.Add(ETagHeaderKey, dataETag);
        }
    }
}