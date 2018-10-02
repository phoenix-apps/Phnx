using Phnx.Data.EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides CRUD operations on data with an ID property for the primary key, via <see cref="Microsoft.EntityFrameworkCore"/>
    /// </summary>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/> that <typeparamref name="TEntity"/> is stored in</typeparam>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CrudDataRepository<TDbContext, TEntity, TKey> : ICrudRepository<TEntity, TKey>
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
        /// Create a new <see cref="CrudDataRepository{TDbContext, TEntity, TKey}"/>
        /// </summary>
        /// <param name="context">The <typeparamref name="TDbContext"/> to use</param>
        /// <param name="tableAccessor">The <see cref="DbSet{TEntity}"/> to use</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> or <paramref name="tableAccessor"/> was <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="tableAccessor"/> returns <see langword="null"/></exception>
        public CrudDataRepository(TDbContext context, Func<TDbContext, DbSet<TEntity>> tableAccessor)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            if (tableAccessor is null)
            {
                throw new ArgumentNullException(nameof(tableAccessor));
            }

            Table = tableAccessor(Context) ??
                throw new ArgumentException($"{nameof(tableAccessor)} gave a null table", nameof(tableAccessor));
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
    }
}
