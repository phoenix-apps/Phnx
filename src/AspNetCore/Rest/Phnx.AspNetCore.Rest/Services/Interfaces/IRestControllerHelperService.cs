using Microsoft.AspNetCore.Mvc;
using Phnx.AspNet.Core.Rest.Models;
using Phnx.AspNetCore.Rest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Provides an interface for helping create a rest controller, centralising all the pieces for REST
    /// </summary>
    /// <typeparam name="TDataModel">The type of data model</typeparam>
    /// <typeparam name="TDtoModel">The type of data transfer object</typeparam>
    /// <typeparam name="TPatchDtoModel">The type of data transfer object used when patching</typeparam>
    public interface IRestControllerHelperService<TDataModel, TDtoModel, in TPatchDtoModel>
    {
        /// <summary>
        /// The mapper used to map between data models and data transfer objects
        /// </summary>
        IResourceMap<TDataModel, TDtoModel, TPatchDtoModel> Mapper { get; }

        /// <summary>
        /// Create a REST NotFound response
        /// </summary>
        /// <param name="resourceTypeFriendlyName">The friendly name for the resource that could not be found</param>
        /// <param name="resourceIdentifier">The unique identifier for the resource that could not be found</param>
        /// <returns></returns>
        NotFoundObjectResult NotFound(string resourceTypeFriendlyName, string resourceIdentifier);

        /// <summary>
        /// Create a new database entry from a data transfer object
        /// </summary>
        /// <param name="dto">The data transfer object representing the data to create</param>
        /// <param name="createDbEntry">The function that inserts the data to the database. This should return the URL for accessing the created resource</param>
        /// <returns>A REST compliant status code for a successful object creation</returns>
        Task<ObjectResult> CreateData(TDtoModel dto, Func<TDataModel, Task<ResourceCreatedResult>> createDbEntry);

        /// <summary>
        /// Delete a collection of data
        /// </summary>
        /// <param name="dataCollection">The collection of data to delete</param>
        /// <param name="deleteDbEntries">The method to delete the values from the database</param>
        /// <returns>A 204 (No Content) status code, showing that the objects were successfully deleted</returns>
        Task<NoContentResult> DeleteData(IEnumerable<TDataModel> dataCollection, Func<IEnumerable<TDataModel>, Task> deleteDbEntries);

        /// <summary>
        /// Delete a single data entry if the E-Tags are valid
        /// </summary>
        /// <param name="data">The data to delete</param>
        /// <param name="deleteDbEntry">The method to delete the value from the database</param>
        /// <returns>
        /// A 204 (No Content) result if the delete is successful,
        /// or a 412 (Precondition Failed) if the E-Tag is not valid
        /// </returns>
        Task<IActionResult> DeleteData(TDataModel data, Func<TDataModel, Task> deleteDbEntry);

        /// <summary>
        /// Create a REST compliant response from a collection of data, mapping them to data transfer objects
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <returns>A REST compliant response containing a collection of data transfer objects</returns>
        OkObjectResult RetrievedData(IEnumerable<TDataModel> data);

        /// <summary>
        /// Create a REST compliant response from a single data entry, mapping it to a data transfer object
        /// </summary>
        /// <param name="data">The data to place in the response. If this is null, a 404 (Not Found) is returned</param>
        /// <param name="resourceTypeFriendlyName">The resource type's friendly name, shown if the resource was not found</param>
        /// <param name="resourceId">The resource type's unique ID, shown if the resource was not found</param>
        /// <returns>
        /// A REST compliant response containing either
        /// a 304 (Not Modified) if the E-Tag is valid,
        /// a 404 (Not Found) if <paramref name="data"/> is <see langword="null"/>,
        /// or a 200 (OK) with the data in the response as a data transfer object
        /// </returns>
        IActionResult RetrievedData(TDataModel data, string resourceTypeFriendlyName, string resourceId);

        /// <summary>
        /// Updates an entry in the database if a request is valid, and returns the relevant REST response
        /// </summary>
        /// <param name="patch">The model representing the data to patch onto <paramref name="data"/></param>
        /// <param name="data">The data model to be patched. If this is <see langword="null"/>, a 404 (Not Found) is returned</param>
        /// <param name="resourceTypeFriendlyName">The resource type's friendly name, shown if the resource was not found</param>
        /// <param name="resourceId">The resource type's unique ID, shown if the resource was not found</param>
        /// <param name="updateDbEntry">The method to call to update the database</param>
        /// <returns>
        /// A REST compliant response containing either
        /// a 412 (Precondition Failed) if the E-Tag is not valid,
        /// a 404 (Not Found) if <paramref name="data"/> is <see langword="null"/>,
        /// or a 200 (OK) with the patched data in the response as a data transfer object
        /// </returns>
        Task<IActionResult> UpdateData(TPatchDtoModel patch, TDataModel data, string resourceTypeFriendlyName, string resourceId, Func<TDataModel, Task> updateDbEntry);
    }
}
