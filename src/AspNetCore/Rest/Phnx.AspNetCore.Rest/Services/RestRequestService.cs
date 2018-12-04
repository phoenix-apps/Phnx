using Phnx.AspNetCore.Rest.Models;
using System;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Interprets a REST request
    /// </summary>
    /// <typeparam name="TDataModel">The type of data subject to change</typeparam>
    public class RestRequestService<TDataModel> : IRestRequestService<TDataModel>
    {
        /// <summary>
        /// The service for reading the E-Tags in the headers of the request
        /// </summary>
        public IETagService ETagService { get; }

        /// <summary>
        /// Create a new <see cref="RestRequestService{TDataModel}"/>
        /// </summary>
        /// <param name="eTagService">The E-Tag reader</param>
        /// <exception cref="ArgumentNullException"><paramref name="eTagService"/> is <see langword="null"/></exception>
        public RestRequestService(IETagService eTagService)
        {
            ETagService = eTagService ?? throw new ArgumentNullException(nameof(eTagService));
        }

        /// <summary>
        /// Get whether a data model should be loaded
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be loaded</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public bool ShouldGetSingle(TDataModel data)
        {
            var result = ETagService.CheckIfNoneMatch(data);

            return
                result == ETagMatchResult.ETagNotInRequest ||
                (result & ETagMatchResult.DoNotMatch) != 0;
        }

        /// <summary>
        /// Get whether a data model should be updated
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be updated</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public bool ShouldUpdate(TDataModel data)
        {
            var result = ETagService.CheckIfMatch(data);

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
        public bool ShouldDelete(TDataModel data)
        {
            var result = ETagService.CheckIfMatch(data);

            return
                result == ETagMatchResult.ETagNotInRequest ||
                (result & ETagMatchResult.Match) != 0;
        }
    }
}
