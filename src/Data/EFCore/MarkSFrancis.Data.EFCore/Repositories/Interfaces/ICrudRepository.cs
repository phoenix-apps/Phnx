using System.Linq;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EFCore.Repositories.Interfaces
{
    /// <summary>
    /// Provides an interface for CRUD operations on data
    /// </summary>
    /// <typeparam name="T">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public interface ICrudRepository<T, in TKey>
    {
        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save
        /// </summary>
        /// <param name="data">The data to commit</param>
        void Create(T data);

        /// <summary>
        /// Get all records of <typeparamref name="T"/> from the database
        /// </summary>
        /// <returns>All records of <typeparamref name="T"/> in the database</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get a single item from the database with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        T GetSingle(TKey id);

        /// <summary>
        /// Commit an update a single item in the database
        /// </summary>
        /// <param name="data">The item to update</param>
        void Update(T data);

        /// <summary>
        /// Commit a delete operation for a single item in the database
        /// </summary>
        /// <param name="data">The item to delete</param>
        void Delete(T data);

        /// <summary>
        /// Save all changes that have been committed
        /// </summary>
        /// <returns>An asyncronous task representing the save operation</returns>
        Task SaveChanges();
    }
}
