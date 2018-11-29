using Microsoft.AspNetCore.Mvc;
using Phnx.AspNetCore.Rest.Models;
using System;
using System.Collections.Generic;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Formulates various REST compliant responses
    /// </summary>
    /// <typeparam name="TDataModel">The data model type</typeparam>
    /// <typeparam name="TDtoModel">The data transfer object type</typeparam>
    public class RestResponseFactory<TDataModel, TDtoModel> : IRestResponseService<TDataModel, TDtoModel>
        where TDataModel : IResourceDataModel
    {
        /// <summary>
        /// The configured response mapper
        /// </summary>
        public IReadonlyResourceMapService<TDataModel, TDtoModel> Mapper { get; }

        /// <summary>
        /// The ETag reader service
        /// </summary>
        public IETagService ETagService { get; }

        /// <summary>
        /// Create a new REST response factory
        /// </summary>
        /// <param name="mapper">The response mapper to map from the <typeparamref name="TDataModel"/> to <typeparamref name="TDtoModel"/></param>
        /// <param name="eTagService">The E-Tag writer</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> or <paramref name="eTagService"/> is <see langword="null"/></exception>
        public RestResponseFactory(IReadonlyResourceMapService<TDataModel, TDtoModel> mapper, IETagService eTagService)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ETagService = eTagService ?? throw new ArgumentNullException(nameof(eTagService));
        }

        /// <summary>
        /// Create a response describing that the data could not be found
        /// </summary>
        /// <param name="resourceTypeFriendlyName">The resource type's friendly name</param>
        /// <param name="resourceIdentifier">The unique identifier for the resource</param>
        /// <returns>A response describing that the data could not be found</returns>
        public NotFoundObjectResult NotFound(string resourceTypeFriendlyName, string resourceIdentifier)
        {
            if (string.IsNullOrWhiteSpace(resourceTypeFriendlyName))
            {
                resourceTypeFriendlyName = "resource";
            }

            string message;
            if (string.IsNullOrWhiteSpace(resourceIdentifier))
            {
                message = $"The requested {resourceTypeFriendlyName} was not found";
            }
            else
            {
                message = $"The requested {resourceTypeFriendlyName} \"{resourceIdentifier}\" was not found";
            }

            return new NotFoundObjectResult(message);
        }

        /// <summary>
        /// Create a response describing that the data has not been changed (ETag)
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        public StatusCodeResult DataHasNotChanged()
        {
            return ETagService.CreateMatchResponse();
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
            ETagService.AddETagToResponse(data);

            var model = Mapper.MapToDto(data);

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
            var model = Mapper.MapToDto(data);

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Create a response describing that the data was successfully created
        /// </summary>
        /// <param name="data">The data that was created</param>
        /// <param name="actionName">The name of the action for accessing the resource</param>
        /// <param name="controllerName">The name of the controller for accessing the resource</param>
        /// <param name="routeValues">The route values needed to access the resource</param>
        /// <returns>A response describing that the data was successfully created</returns>
        public CreatedAtActionResult CreatedDataAtAction(TDataModel data, string controllerName, string actionName, object routeValues)
        {
            ETagService.AddETagToResponse(data);

            var createdModel = Mapper.MapToDto(data);

            return new CreatedAtActionResult(actionName, controllerName, routeValues, createdModel);
        }

        /// <summary>
        /// Create a response describing that the data was successfully created
        /// </summary>
        /// <param name="data">The data that was created</param>
        /// <param name="url">The URL to the data that was created</param>
        /// <returns>A response describing that the data was successfully created</returns>
        public CreatedResult CreatedData(TDataModel data, string url)
        {
            ETagService.AddETagToResponse(data);

            var createdModel = Mapper.MapToDto(data);

            return new CreatedResult(url, createdModel);
        }

        /// <summary>
        /// Create a response describing that the data has been updated (ETag)
        /// </summary>
        /// <returns>A response describing that the data has been updated</returns>
        public StatusCodeResult DataHasChanged()
        {
            return ETagService.CreateDoNotMatchResponse();
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
            ETagService.AddETagToResponse(data);

            var editedModel = Mapper.MapToDto(data);

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
