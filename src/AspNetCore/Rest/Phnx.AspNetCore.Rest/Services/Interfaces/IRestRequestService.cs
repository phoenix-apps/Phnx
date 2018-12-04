namespace Phnx.AspNetCore.Rest.Services
{
    /// <summary>
    /// Provides an interface for interpreting a REST request and taking according action
    /// </summary>
    /// <typeparam name="TDataModel">The type of data subject to change</typeparam>
    public interface IRestRequestService<in TDataModel>
    {
        /// <summary>
        /// Get whether a data model should be deleted
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be deleted</returns>
        bool ShouldDelete(TDataModel data);

        /// <summary>
        /// Get whether a data model should be loaded
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be loaded</returns>
        bool ShouldGetSingle(TDataModel data);

        /// <summary>
        /// Get whether a data model should be updated
        /// </summary>
        /// <param name="data">The data model to check</param>
        /// <returns>Whether the data model should be updated</returns>
        bool ShouldUpdate(TDataModel data);
    }
}
