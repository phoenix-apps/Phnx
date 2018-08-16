using MarkSFrancis.AspNetCore.Rest.Models;
using MarkSFrancis.AspNetCore.Rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkSFrancis.AspNetCore.Rest.Services
{
    /// <summary>
    /// Helps to create a rest controller, centralising all the pieces for REST
    /// </summary>
    /// <typeparam name="TDataModel">The type of data model</typeparam>
    /// <typeparam name="TDtoModel">The type of data transfer object</typeparam>
    /// <typeparam name="TDtoLinksModel">The type of links contained within the data transfer object</typeparam>
    /// <typeparam name="TPatchDtoModel">The type of data transfer object used when patching</typeparam>
    public class RestControllerHelperService<TDataModel, TDtoModel, TDtoLinksModel, TPatchDtoModel> : IRestControllerHelperService<TDataModel, TDtoModel, TDtoLinksModel, TPatchDtoModel>
           where TDataModel : IResourceDataModel
           where TDtoModel : IHateoasDtoModel<TDtoLinksModel>
           where TDtoLinksModel : ILinksDtoModel
    {
        /// <summary>
        /// The helper for REST Requests
        /// </summary>
        protected IRestRequestService<TDataModel> RestRequestService { get; }

        /// <summary>
        /// The helper for REST Responses
        /// </summary>
        protected IRestResponseService<TDataModel, TDtoModel, TDtoLinksModel> RestResponseService { get; }

        /// <summary>
        /// Create a new 
        /// <see cref="RestControllerHelperService{TDataModel,TDtoModel,TDtoLinksModel,TPatchDtoModel}"/>
        /// </summary>
        /// <param name="mapper">The service to map between the data models and its various data transfer objects</param>
        /// <param name="restRequestService">The REST request service to handle incoming requests</param>
        /// <param name="restResponseService">The REST response service to handle outgoing responses</param>
        public RestControllerHelperService(
            IResourceMapService<TDataModel, TDtoModel, TDtoLinksModel, TPatchDtoModel> mapper,
            IRestRequestService<TDataModel> restRequestService,
            IRestResponseService<TDataModel, TDtoModel, TDtoLinksModel> restResponseService)
        {
            Mapper = mapper;
            RestRequestService = restRequestService;
            RestResponseService = restResponseService;
        }

        /// <summary>
        /// The service to map between the data models and its various data transfer objects
        /// </summary>
        public IResourceMapService<TDataModel, TDtoModel, TDtoLinksModel, TPatchDtoModel> Mapper { get; }

        /// <summary>
        /// Create a REST NotFound response
        /// </summary>
        /// <param name="resourceTypeFriendlyName">The friendly name for the resource that could not be found</param>
        /// <param name="resourceIdentifier">The unique identifier for the resource that could not be found</param>
        /// <returns></returns>
        public NotFoundObjectResult NotFound(string resourceTypeFriendlyName, string resourceIdentifier)
        {
            return RestResponseService.NotFound(resourceTypeFriendlyName, resourceIdentifier);
        }

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
        public IActionResult RetrievedData(TDataModel data, string resourceTypeFriendlyName, string resourceId)
        {
            if (data == null)
            {
                return RestResponseService.NotFound(resourceTypeFriendlyName, resourceId);
            }

            if (!RestRequestService.ShouldGetSingle(data))
            {
                return RestResponseService.DataHasNotChanged();
            }

            return RestResponseService.RetrievedData(data);
        }

        /// <summary>
        /// Create a REST compliant response from a collection of data, mapping them to data transfer objects
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <returns>A REST compliant response containing a collection of data transfer objects</returns>
        public OkObjectResult RetrievedData(IEnumerable<TDataModel> data)
        {
            return RestResponseService.RetrievedData(data);
        }

        /// <summary>
        /// Create a new database entry from a data transfer object
        /// </summary>
        /// <param name="dto">The data transfer object representing the data to create</param>
        /// <param name="createDbEntry">The function that inserts the data to the database</param>
        /// <returns>A REST compliant status code for a successful object creation</returns>
        public async Task<CreatedResult> CreateData(TDtoModel dto, Func<TDataModel, Task> createDbEntry)
        {
            var data = Mapper.MapToData(dto);

            await createDbEntry(data);

            return RestResponseService.CreatedData(data);
        }

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
        public async Task<IActionResult> UpdateData(TPatchDtoModel patch, TDataModel data, string resourceTypeFriendlyName, string resourceId, Func<TDataModel, Task> updateDbEntry)
        {
            if (data == null)
            {
                return RestResponseService.NotFound(resourceTypeFriendlyName, resourceId);
            }

            if (!RestRequestService.ShouldUpdate(data))
            {
                return RestResponseService.DataHasChanged();
            }

            Mapper.PatchToData(patch, data);

            await updateDbEntry(data);

            return RestResponseService.UpdatedData(data);
        }

        /// <summary>
        /// Delete a collection of data
        /// </summary>
        /// <param name="dataCollection">The collection of data to delete</param>
        /// <param name="deleteDbEntries">The method to delete the values from the database</param>
        /// <returns>A 204 (No Content) status code, showing that the objects were successfully deleted</returns>
        public async Task<NoContentResult> DeleteData(IEnumerable<TDataModel> dataCollection, Func<IEnumerable<TDataModel>, Task> deleteDbEntries)
        {
            await deleteDbEntries(dataCollection);

            return RestResponseService.DeletedData();
        }

        /// <summary>
        /// Delete a single data entry if the E-Tags are valid
        /// </summary>
        /// <param name="data">The data to delete</param>
        /// <param name="deleteDbEntry">The method to delete the value from the database</param>
        /// <returns>
        /// A 204 (No Content) result if the delete is successful, 
        /// or a 412 (Precondition Failed) if the E-Tag is not valid
        /// </returns>
        public async Task<IActionResult> DeleteData(TDataModel data, Func<TDataModel, Task> deleteDbEntry)
        {
            if (data != null)
            {
                if (!RestRequestService.ShouldDelete(data))
                {
                    return RestResponseService.DataHasChanged();
                }

                await deleteDbEntry(data);
            }

            return RestResponseService.DeletedData();
        }
    }
}
