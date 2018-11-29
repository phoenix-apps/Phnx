using Microsoft.AspNetCore.Mvc;
using Phnx.AspNetCore.Rest.Models;
using System.Collections.Generic;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Provides methods for formulating various REST compliant responses
    /// </summary>
    /// <typeparam name="TDataModel">The data model type</typeparam>
    /// <typeparam name="TDtoModel">The data transfer object type</typeparam>
    public interface IRestResponseService<in TDataModel, out TDtoModel>
        where TDataModel : IResourceDataModel
    {
        /// <summary>
        /// Create a response describing that the data was successfully created
        /// </summary>
        /// <param name="data">The data that was created</param>
        /// <param name="actionName">The name of the action for accessing the resource</param>
        /// <param name="controllerName">The name of the controller for accessing the resource</param>
        /// <param name="routeValues">The route values needed to access the resource</param>
        /// <returns>A response describing that the data was successfully created</returns>
        CreatedAtActionResult CreatedDataAtAction(TDataModel data, string controllerName, string actionName, object routeValues);

        /// <summary>
        /// Create a response describing that the data was successfully created
        /// </summary>
        /// <param name="data">The data that was created</param>
        /// <param name="url">The URL to the data that was created</param>
        /// <returns>A response describing that the data was successfully created</returns>
        CreatedResult CreatedData(TDataModel data, string url);

        /// <summary>
        /// Create a response describing that the data has been updated (ETag)
        /// </summary>
        /// <returns>A response describing that the data has been updated</returns>
        StatusCodeResult DataHasChanged();

        /// <summary>
        /// Create a response describing that the data has not been changed (ETag)
        /// </summary>
        /// <returns>A response describing that the data has not been changed</returns>
        StatusCodeResult DataHasNotChanged();

        /// <summary>
        /// Create a response describing that the data has been deleted
        /// </summary>
        /// <returns>A response describing that the data has been deleted</returns>
        NoContentResult DeletedData();

        /// <summary>
        /// Create a response describing that the data could not be found
        /// </summary>
        /// <param name="resourceTypeFriendlyName">The resource type's friendly name</param>
        /// <param name="resourceIdentifier">The unique identifier for the resource</param>
        /// <returns>A response describing that the data could not be found</returns>
        NotFoundObjectResult NotFound(string resourceTypeFriendlyName, string resourceIdentifier);

        /// <summary>
        /// Create a response with a collection of retrieved data in, mapped to data transfer objects
        /// </summary>
        /// <param name="data">The data that was retrieved</param>
        /// <returns>
        /// A response with a collection of retrieved data in, mapped to data transfer objects
        /// </returns>
        OkObjectResult RetrievedData(IEnumerable<TDataModel> data);

        /// <summary>
        /// Create a response with retrieved data in, mapped to a data transfer object
        /// </summary>
        /// <param name="data">The data that was retrieved</param>
        /// <returns>
        /// A response with retrieved data in, mapped to a data transfer object
        /// </returns>
        OkObjectResult RetrievedData(TDataModel data);

        /// <summary>
        /// Create a response with updated data in, mapped to a data transfer object
        /// </summary>
        /// <param name="data">The data that was updated</param>
        /// <returns>
        /// A response with updated data in, mapped to a data transfer object
        /// </returns>
        OkObjectResult UpdatedData(TDataModel data);
    }
}
