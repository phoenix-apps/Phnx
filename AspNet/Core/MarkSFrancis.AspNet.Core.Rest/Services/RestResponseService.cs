using MarkSFrancis.AspNet.Core.Rest.Models;
using MarkSFrancis.AspNet.Core.Rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MarkSFrancis.AspNet.Core.Rest.Services
{
    /// <summary>
    /// Formulates various REST compliant responses
    /// </summary>
    /// <typeparam name="TDataModel">The data model type</typeparam>
    /// <typeparam name="TDtoModel">The data transfer object type</typeparam>
    /// <typeparam name="TDtoLinksModel">The data transfer object type's HATEOAS links</typeparam>
    public class RestResponseService<TDataModel, TDtoModel, TDtoLinksModel> : IRestResponseService<TDataModel, TDtoModel, TDtoLinksModel>
        where TDataModel : IResourceDataModel
        where TDtoModel : IHateoasDtoModel<TDtoLinksModel>
        where TDtoLinksModel : ILinksDtoModel
    {
        private readonly IReadonlyResourceMapService<TDataModel, TDtoModel, TDtoLinksModel> mapper;

        private readonly IETagService eTagService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="eTagService">The E-Tag writer</param>
        public RestResponseService(IReadonlyResourceMapService<TDataModel, TDtoModel, TDtoLinksModel> mapper, IETagService eTagService)
        {
            this.mapper = mapper;

            this.eTagService = eTagService;
        }

        /// <summary>
        /// Create a response describing that the data could not be found
        /// </summary>
        /// <param name="resourceTypeFriendlyName">The resource type's friendly name</param>
        /// <param name="resourceIdentifier">The unique identifier for the resource</param>
        /// <returns>A response describing that the data could not be found</returns>
        public NotFoundObjectResult NotFound(string resourceTypeFriendlyName, string resourceIdentifier)
        {
            return new NotFoundObjectResult($"The {resourceTypeFriendlyName} \"{resourceIdentifier}\" does not exist");
        }

        /// <summary>
        /// Create a response describing that the data has not been changed (ETag)
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        public StatusCodeResult DataHasNotChanged()
        {
            return eTagService.CreateMatchResponse();
        }

        /// <summary>
        /// Create a response with retrieved data in, mapped to a data transfer object
        /// </summary>
        /// <param name="data">The data that was retrieved</param>
        /// <returns>
        /// A response with retrieved data in, mapped to a data transfer object
        /// </returns>
        public OkObjectResult RetrievedData(TDataModel data)
        {
            eTagService.AddETagToResponse(data);

            var model = mapper.MapToDto(data);

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Create a response with a collection of retrieved data in, mapped to data transfer objects
        /// </summary>
        /// <param name="data">The data that was retrieved</param>
        /// <returns>
        /// A response with a collection of retrieved data in, mapped to data transfer objects
        /// </returns>
        public OkObjectResult RetrievedData(IEnumerable<TDataModel> data)
        {
            var model = mapper.MapToDto(data);

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Create a response describing that the data was successfully created
        /// </summary>
        /// <param name="data">The data that was created</param>
        /// <returns>A response describing that the data was successfully created</returns>
        public CreatedResult CreatedData(TDataModel data)
        {
            eTagService.AddETagToResponse(data);

            var createdModel = mapper.MapToDto(data);

            return new CreatedResult(createdModel.Links.Self, createdModel);
        }

        /// <summary>
        /// Create a response describing that the data has been updated (ETag)
        /// </summary>
        /// <returns>A response describing that the data has been updated</returns>
        public StatusCodeResult DataHasChanged()
        {
            return eTagService.CreateDoNotMatchResponse();
        }

        /// <summary>
        /// Create a response with updated data in, mapped to a data transfer object
        /// </summary>
        /// <param name="data">The data that was updated</param>
        /// <returns>
        /// A response with updated data in, mapped to a data transfer object
        /// </returns>
        public OkObjectResult UpdatedData(TDataModel data)
        {
            eTagService.AddETagToResponse(data);

            var editedModel = mapper.MapToDto(data);

            return new OkObjectResult(editedModel);
        }

        /// <summary>
        /// Create a response describing that the data has been deleted
        /// </summary>
        /// <returns>A response describing that the data has been deleted</returns>
        public NoContentResult DeletedData()
        {
            return new NoContentResult();
        }
    }
}
