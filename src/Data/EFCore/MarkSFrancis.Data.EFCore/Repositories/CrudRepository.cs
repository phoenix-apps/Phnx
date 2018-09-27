using MarkSFrancis.Data.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides CRUD operations on data with an ID property for the primary key, via <see cref="Microsoft.EntityFrameworkCore"/>
    /// </summary>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/> that <typeparamref name="TEntity"/> is stored in</typeparam>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CrudRepository<TDbContext, TEntity, TKey> : ICrudRepository<TEntity, TKey>
        where TEntity : class, IIdDataModel<TKey>
        where TDbContext : DbContext
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        protected TDbContext Context { get; }

        /// <summary>
        /// The DbSet for the entities wrapped by this repository
        /// </summary>
        protected DbSet<TEntity> Table { get; }

        /// <summary>
        /// Create a new <see cref="CrudRepository{TDbContext, T, TKey}"/>
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to use</param>
        /// <param name="accessor">The <see cref="DbSet{T}"/> to use</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> or <paramref name="accessor"/> was <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="accessor"/> returns <see langword="null"/></exception>
        public CrudRepository(TDbContext context, Func<TDbContext, DbSet<TEntity>> accessor)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            if (accessor is null)
            {
                throw new ArgumentNullException(nameof(accessor));
            }

            Table = accessor(Context) ??
                throw new ArgumentException($"{nameof(accessor)} gave a null table", nameof(accessor));
        }

        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save
        /// </summary>
        /// <param name="data">The data to commit</param>
        public virtual void Create(TEntity data)
        {
            Table.Add(data);
        }

        /// <summary>
        /// Get all records of <typeparamref name="TEntity"/> from the database
        /// </summary>
        /// <returns>All records of <typeparamref name="TEntity"/> in the database</returns>
        public virtual IQueryable<TEntity> Get()
        {
            return Table;
        }

        /// <summary>
        /// Get a single item from the database with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        public virtual TEntity GetSingle(TKey id)
        {
            return Table.FirstOrDefault(item => item.Id.Equals(id));
        }

        /// <summary>
        /// Commit an update a single item in the database
        /// </summary>
        /// <param name="data">The item to update</param>
        public virtual void Update(TEntity data)
        {
            Table.Update(data);
        }

        /// <summary>
        /// Commit a delete operation for a single item in the database
        /// </summary>
        /// <param name="data">The item to delete</param>
        public virtual void Delete(TEntity data)
        {
            Table.Remove(data);
        }

        /// <summary>
        /// Save all changes that have been committed
        /// </summary>
        /// <returns>An asyncronous task representing the save operation</returns>
        /// <exception cref="DbUpdateException">An error is encountered while saving to the database</exception>
        /// <exception cref="DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database. A concurrency violation occurs when an unexpected number of rows are affected during save. This is usually because the data in the database has been modified since it was loaded into memory</exception>
        /// <remarks>This method will automatically call <see cref="ChangeTracker.DetectChanges"/> to discover any changes to entity instances before saving to the underlying database. This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled"/>. Multiple active operations on the same context instance are not supported. Use <see langword="await"/> to ensure that any asynchronous operations have completed before calling another method on this context.</remarks>
        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
