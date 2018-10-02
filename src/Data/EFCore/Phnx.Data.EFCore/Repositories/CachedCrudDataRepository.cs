using Phnx.Data.LazyLoad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides CRUD operations on data with an ID property for the primary key, via <see cref="Microsoft.EntityFrameworkCore"/>, with an internal cache to reduce database calls
    /// </summary>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/> that <typeparamref name="TEntity"/> is stored in</typeparam>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CachedCrudDataRepository<TDbContext, TEntity, TKey> : CrudDataRepository<TDbContext, TEntity, TKey>
        where TEntity : class, IIdDataModel<TKey>
        where TDbContext : DbContext
    {
        private readonly LazyDatabase cache;

        /// <summary>
        /// Create a new <see cref="CachedCrudDataRepository{TDbContext, TEntity, TKey}"/>
        /// </summary>
        /// <param name="context">The <typeparamref name="TDbContext"/> to use</param>
        /// <param name="tableAccessor">The <see cref="DbSet{TEntity}"/> to use</param>
        /// <param name="cache">The cache to use for loading and commiting changes</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> or <paramref name="tableAccessor"/> was <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="tableAccessor"/> returns <see langword="null"/></exception>
        public CachedCrudDataRepository(LazyDatabase cache, TDbContext context, Func<TDbContext, DbSet<TEntity>> tableAccessor) : base(context, tableAccessor)
        {
            this.cache = cache;
            this.cache.TryAddTable<TKey, TEntity>(base.GetSingle);
        }

        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save. This does not add it to the cache, as it does not have an ID yet
        /// </summary>
        /// <param name="data">The data to commit</param>
        public override void Create(TEntity data)
        {
            base.Create(data);
        }

        /// <summary>
        /// Get all records of <typeparamref name="TEntity"/> from the database. This drops the cache support
        /// </summary>
        /// <returns>All records of <typeparamref name="TEntity"/> in the database</returns>
        public override IQueryable<TEntity> Get()
        {
            return base.Get();
        }

        /// <summary>
        /// Get a single item from the cache (or database if it's not in the cache) with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        public override TEntity GetSingle(TKey id)
        {
            cache.TryGet(id, out TEntity value);

            return value;
        }

        /// <summary>
        /// Commit an update a single item in the database, updating the cache
        /// </summary>
        /// <param name="data">The item to update</param>
        public override void Update(TEntity data)
        {
            cache.TryAddOrUpdate(data.Id, data);

            base.Update(data);
        }

        /// <summary>
        /// Commit a delete operation for a single item in the database, updating the cache
        /// </summary>
        /// <param name="data">The item to delete</param>
        public override void Delete(TEntity data)
        {
            cache.TryRemove<TKey, TEntity>(data.Id);
            base.Delete(data);
        }

        /// <summary>
        /// The total number of items cached for the table represented by this <see cref="CachedCrudDataRepository{TDbContext, TEntity, TKey}"/>
        /// </summary>
        public int CachedCount => cache.TableItemsCachedCount<TEntity>();
    }
}
