namespace Phnx.AspNetCore.Rest.Models
{
    /// <summary>
    /// A data model that can be used with e-tags
    /// </summary>
    public interface IResourceDataModel
    {
        /// <summary>
        /// A random value that must change whenever this model is persisted to the store
        /// </summary>
        /// <remarks>Entity Framework Core can handle concurrency tokens for you. For more information, see EF Core's documentation on <a href="https://docs.microsoft.com/en-us/ef/core/modeling/concurrency">Concurrency Tokens</a></remarks>
        string ConcurrencyStamp { get; }
    }
}
