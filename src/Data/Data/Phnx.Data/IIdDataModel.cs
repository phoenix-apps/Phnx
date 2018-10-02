namespace Phnx.Data
{
    /// <summary>
    /// A data model with a primary key made from a single column
    /// </summary>
    /// <typeparam name="TKey">The type of key</typeparam>
    public interface IIdDataModel<out TKey>
    {
        /// <summary>
        /// The key for this data model
        /// </summary>
        TKey Id { get; }
    }
}
