namespace Phnx.AspNetCore.Rest.Models
{
    /// <summary>
    /// A data model that can be used with e-tags
    /// </summary>
    public interface IResourceDataModel
    {
        /// <summary>
        /// The version number of this data model.
        /// This should be incremented each time the data is edited
        /// </summary>
        byte[] RowVersion { get; }
    }
}
