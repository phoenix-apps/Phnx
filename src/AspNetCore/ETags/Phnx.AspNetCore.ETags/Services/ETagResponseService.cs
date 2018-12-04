using Microsoft.AspNetCore.Mvc;
using System;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Formulates various REST compliant responses
    /// </summary>
    public class ETagResponseService : IETagResponseService
    {
        /// <summary>
        /// The e-tag service
        /// </summary>
        public IETagService ETagService { get; }

        /// <summary>
        /// Create a new E-Tag response factory
        /// </summary>
        /// <param name="eTagService">The e-tag service</param>
        /// <exception cref="ArgumentNullException"><paramref name="eTagService"/> is <see langword="null"/></exception>
        public ETagResponseService(IETagService eTagService)
        {
            ETagService = eTagService ?? throw new ArgumentNullException(nameof(eTagService));
        }

        /// <summary>
        /// Create a response describing that the data has not been changed
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        public StatusCodeResult CreateDataHasNotChangedResponse()
        {
            return ETagService.CreateMatchResponse();
        }

        /// <summary>
        /// Create a response describing that the data has been changed
        /// </summary>
        /// <returns>A response describing that the data has been changed</returns>
        public StatusCodeResult CreateDataHasChangedResponse()
        {
            return ETagService.CreateDoNotMatchResponse();
        }

        /// <summary>
        /// Add a weak e-tag for <paramref name="data"/>
        /// </summary>
        /// <param name="data">The data to add the e-tag for</param>
        public void AddWeakETagToResponse(object data)
        {
            ETagService.AddETagForModelToResponse(data);
        }

        /// <summary>
        /// Add a strong e-tag if <paramref name="data"/> supports it
        /// </summary>
        /// <param name="data">The data to add the e-tag for</param>
        public bool TryAddStrongETagToResponse(object data)
        {
            if (!ETagService.TryGetStrongETag(data, out var etag))
            {
                return false;
            }

            ETagService.AddETagForModelToResponse(etag);
            return true;
        }

        /// <summary>
        /// Add a strong e-tag if <paramref name="data"/> supports it, or a weak tag if it does not
        /// </summary>
        /// <param name="data">The data to add the e-tag for</param>
        public void AddBestETagToResponse(object data)
        {
            ETagService.AddETagForModelToResponse(data);
        }
    }
}
