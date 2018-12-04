using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Net;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Formulates various e-tag related responses
    /// </summary>
    public class ETagResponseService : IETagResponseService
    {
        /// <summary>
        /// The ETag header's key
        /// </summary>
        public const string ETagHeaderKey = "ETag";

        /// <summary>
        /// The e-tag service
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
        /// Create a new E-Tag response factory
        /// </summary>
        /// <param name="actionContext">The action context accessor for writing e-tag headers</param>
        /// <param name="eTagService">The e-tag service</param>
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
        /// Add a weak e-tag for <paramref name="savedData"/>
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public void AddWeakETagForModelToResponse(object savedData)
        {
            var etag = ETagService.GetWeakETagForModel(savedData);

            AddETagToResponse(etag);
        }

        /// <summary>
        /// Add a strong e-tag if <paramref name="savedData"/> supports it
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public bool TryAddStrongETagForModelToResponse(object savedData)
        {
            if (!ETagService.TryGetStrongETagForModel(savedData, out var etag))
            {
                return false;
            }

            AddETagToResponse(etag);
            return true;
        }

        /// <summary>
        /// Add a strong e-tag if <paramref name="savedData"/> supports it, or a weak tag if it does not
        /// </summary>
        /// <param name="savedData">The data to add the e-tag for</param>
        /// <exception cref="ArgumentNullException"><paramref name="savedData"/> is <see langword="null"/></exception>
        public void AddBestETagForModelToResponse(object savedData)
        {
            if (!TryAddStrongETagForModelToResponse(savedData))
            {
                AddWeakETagForModelToResponse(savedData);
            }
        }

        /// <summary>
        /// Append the e-tag to the response. Weak e-tags should be formatted as W/"etag", and strong e-tags should be formatted as "etag"
        /// </summary>
        /// <param name="etag">The e-tag to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="etag"/> is <see langword="null"/></exception>
        public void AddETagToResponse(string etag)
        {
            if (etag is null)
            {
                throw new ArgumentNullException(nameof(etag));
            }

            ResponseHeaders.Add(ETagHeaderKey, etag);
        }
    }
}