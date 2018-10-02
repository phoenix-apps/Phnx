using Phnx.AspNetCore.Rest.Models;
using Phnx.AspNetCore.Rest.Services.Interfaces;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Interprets a REST request
    /// </summary>
    /// <typeparam name="TDataModel">The type of data subject to change</typeparam>
    public class RestRequestService<TDataModel> : IRestRequestService<TDataModel>
        where TDataModel : IResourceDataModel
    {
        /// <summary>
        /// The service for reading the E-Tags in the headers of the request
        /// </summary>
        private readonly IETagService eTagService;

        /// <summary>
        /// Create a new <see cref="RestRequestService{TDataModel}"/>
        /// </summary>
        /// <param name="eTagService">The E-Tag reader</param>
        public RestRequestService(IETagService eTagService)
        {
            this.eTagService = eTagService;
        }

        /// <summary>
        /// Get whether a data model should be loaded
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be loaded</returns>
        public bool ShouldGetSingle(TDataModel data)
        {
            return eTagService.CheckIfNoneMatch(data);
        }

        /// <summary>
        /// Get whether a data model should be updated
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be updated</returns>
        public bool ShouldUpdate(TDataModel data)
        {
            return eTagService.CheckIfMatch(data);
        }

        /// <summary>
        /// Get whether a data model should be deleted
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be deleted</returns>
        public bool ShouldDelete(TDataModel data)
        {
            return eTagService.CheckIfMatch(data);
        }
    }
}
