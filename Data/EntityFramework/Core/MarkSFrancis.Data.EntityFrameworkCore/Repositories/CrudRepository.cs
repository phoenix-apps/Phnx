using MarkSFrancis.Data.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides CRUD operations on data with a single primary key, via <see cref="Microsoft.EntityFrameworkCore"/>
    /// </summary>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/> that <typeparamref name="T"/> is stored in</typeparam>
    /// <typeparam name="T">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CrudRepository<TDbContext, T, TKey> : ICrudRepository<T, TKey>
        where T : class, IPrimaryKeyDataModel<TKey>, new()
        where TDbContext : DbContext
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        protected TDbContext Context { get; }

        /// <summary>
        /// The DbSet for the entities wrapped by this repository
        /// </summary>
        protected DbSet<T> DbSet { get; }

        /// <summary>
        /// Create a new <see cref="CrudRepository{TDbContext, T, TKey}"/>
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to use</param>
        /// <param name="accessor">The <see cref="DbSet"/> to use</param>
        public CrudRepository(TDbContext context, Func<TDbContext, DbSet<T>> accessor)
        {
            Context = context ?? throw ErrorFactory.Default.ArgumentNull(nameof(context));

            DbSet = accessor?.Invoke(Context) ?? throw ErrorFactory.Default.ArgumentNull(nameof(accessor));
        }

        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save
        /// </summary>
        /// <param name="data">The data to commit</param>
        public virtual void Create(T data)
        {
            DbSet.Add(data);
        }

        /// <summary>
        /// Get all records of <typeparamref name="T"/> from the database
        /// </summary>
        /// <returns>All records of <typeparamref name="T"/> in the database</returns>
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Get a single item from the database with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        public virtual T GetSingle(TKey id)
        {
            return DbSet.FirstOrDefault(item => item.Id.Equals(id));
        }

        /// <summary>
        /// Commit an update a single item in the database
        /// </summary>
        /// <param name="data">The item to update</param>
        public virtual void Update(T data)
        {
            DbSet.Update(data);
        }

        /// <summary>
        /// Commit a delete operation for a single item in the database
        /// </summary>
        /// <param name="data">The item to delete</param>
        public virtual void Delete(T data)
        {
            DbSet.Remove(data);
        }

        /// <summary>
        /// Save all changes that have been committed
        /// </summary>
        /// <returns>An asyncronous task representing the save operation</returns>
        public virtual async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
