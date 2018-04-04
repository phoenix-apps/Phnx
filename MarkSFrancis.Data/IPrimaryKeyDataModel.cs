namespace MarkSFrancis.Data
{
    /// <summary>
    /// A data model with a single primary key
    /// </summary>
    /// <typeparam name="TKey">The type of primary key</typeparam>
    public interface IPrimaryKeyDataModel<out TKey>
    {
        /// <summary>
        /// The primary key for this data model
        /// </summary>
        TKey Id { get; }
    }
}
