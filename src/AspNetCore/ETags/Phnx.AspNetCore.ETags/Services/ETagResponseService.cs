using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Formulates various ETag related responses
    /// </summary>
    public class ETagResponseService : IETagResponseService
    {
        /// <summary>
        /// The ETag header's key
        /// </summary>
        public const string ETagHeaderKey = "ETag";

        /// <summary>
        /// The ETag service
        /// </summary>
        public IETagService ETagService { get; }

        /// <summary>
        /// The action context accessor for accessing the current request and response headers
        /// </summary>
        public IActionContextAccessor ActionContext { get; }

        /// <summary>
        /// The headers in the response
        /// </summary>
        public IHeaderDictionary ResponseHeaders => ActionContext.ActionContext.HttpContext.Response.Headers;

        /// <summary>
        /// Create a new ETag response factory
        /// </summary>
        /// <param name="actionContext">The action context accessor for writing ETag headers</param>
        /// <param name="eTagService">The ETag service</param>
        /// <exception cref="ArgumentNullException"><paramref name="actionContext"/> or <paramref name="eTagService"/> is <see langword="null"/></exception>
        public ETagResponseService(IActionContextAccessor actionContext, IETagService eTagService)
        {
            ActionContext = actionContext ?? throw new ArgumentNullException(nameof(actionContext));

            ETagService = eTagService ?? throw new ArgumentNullException(nameof(eTagService));
        }

        /// <summary>
        /// Create a response describing that the data has not been changed
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        public StatusCodeResult CreateDataHasNotChangedResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Create a response describing that the data has been changed
        /// </summary>
        /// <returns>A response describing that the data has been changed</returns>
        public StatusCodeResult CreateDataHasChangedResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.PreconditionFailed);
        }

        /// <summary>
        /// Add a weak ETag for <paramref name="savedData"/>
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public void AddWeakETagForModel(object savedData)
        {
            if (savedData is null)
            {
                throw new ArgumentNullException(nameof(savedData));
            }

            var eTag = ETagService.GetWeakETagForModel(savedData);

            AddETag(eTag);
        }

        /// <summary>
        /// Add a strong ETag if <paramref name="savedData"/> supports it
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public bool TryAddStrongETagForModel(object savedData)
        {
            if (!ETagService.TryGetStrongETagForModel(savedData, out var eTag))
            {
                return false;
            }

            AddETag(eTag);
            return true;
        }

        /// <summary>
        /// Add a strong ETag if <paramref name="savedData"/> supports it, or a weak tag if it does not
        /// </summary>
        /// <param name="savedData">The data to add the ETag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public void AddStrongestETagForModel(object savedData)
        {
            if (savedData is null)
            {
                throw new ArgumentNullException(nameof(savedData));
            }

            if (!TryAddStrongETagForModel(savedData))
            {
                AddWeakETagForModel(savedData);
            }
        }

        /// <summary>
        /// Append the ETag to the response. Weak ETags should be formatted as W/"eTag", and strong ETags should be formatted as "eTag"
        /// </summary>
        /// <remarks>For more information on ETag formatting, see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/ETag"/></remarks>
        /// <param name="eTag">The ETag to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="eTag"/> is <see langword="null"/></exception>
        public void AddETag(string eTag)
        {
            if (eTag is null)
            {
                throw new ArgumentNullException(nameof(eTag));
            }

            ResponseHeaders.Add(ETagHeaderKey, eTag);
        }
    }
}