using System.Linq;

namespace MarkSFrancis.Data.EFCore.Repositories.Interfaces
{
    /// <summary>
    /// Provides an interface for CRUD operations on data
    /// </summary>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public interface ICrudRepository<TEntity, in TKey>
    {
        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save
        /// </summary>
        /// <param name="data">The data to commit</param>
        void Create(TEntity data);

        /// <summary>
        /// Get all records of <typeparamref name="TEntity"/> from the database
        /// </summary>
        /// <returns>All records of <typeparamref name="TEntity"/> in the database</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Get a single item from the database with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        TEntity GetSingle(TKey id);

        /// <summary>
        /// Commit an update a single item in the database
        /// </summary>
        /// <param name="data">The item to update</param>
        void Update(TEntity data);

        /// <summary>
        /// Commit a delete operation for a single item in the database
        /// </summary>
        /// <param name="data">The item to delete</param>
        void Delete(TEntity data);
    }
}
