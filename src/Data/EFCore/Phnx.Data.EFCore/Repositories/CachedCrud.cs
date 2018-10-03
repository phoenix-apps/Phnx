using Phnx.Data.LazyLoad;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides CRUD operations on data with an ID property for the primary key
    /// </summary>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CachedCrud<TEntity, TKey> : ICrud<TEntity, TKey>
        where TEntity : class, IIdDataModel<TKey>
    {
        /// <summary>
        /// The <see cref="LazyDatabase"/> used as a cache source
        /// </summary>
        public LazyDatabase Cache { get; }

        /// <summary>
        /// The underlying data source supported by this cache
        /// </summary>
        public ICrud<TEntity, TKey> DataSource { get; }

        /// <summary>
        /// Create a new <see cref="CachedCrud{TEntity, TKey}"/>. If <typeparamref name="TEntity"/> is not configured for the <paramref name="cache"/>, it automatically adds it with the <paramref name="cache"/>'s default lifespan for entities
        /// </summary>
        /// <param name="cache">The cache to use for loading and commiting changes</param>
        /// <param name="dataSource">The data to cache</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataSource"/> was <see langword="null"/></exception>
        public CachedCrud(LazyDatabase cache, ICrud<TEntity, TKey> dataSource)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));

            Cache.TryAddTable<TKey, TEntity>(DataSource.GetSingle);
        }

        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save. This does not add it to the cache, as it does not have an ID yet
        /// </summary>
        /// <param name="data">The data to commit</param>
        public void Create(TEntity data)
        {
            DataSource.Create(data);

            Cache.TryAddOrUpdate(data.Id, data);
        }

        /// <summary>
        /// Get all records of <typeparamref name="TEntity"/> from the database. This has no cache support
        /// </summary>
        /// <returns>All records of <typeparamref name="TEntity"/> in the database</returns>
        public IQueryable<TEntity> Get()
        {
            return DataSource.Get();
        }

        /// <summary>
        /// Get a single item from the cache (or database if it's not in the cache) with the key <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key of the item to get</param>
        /// <returns>The first matching item</returns>
        public TEntity GetSingle(TKey id)
        {
            Cache.TryGet(id, out TEntity value);

            return value;
        }

        /// <summary>
        /// Commit an update a single item in the database, updating the cache
        /// </summary>
        /// <param name="data">The item to update</param>
        public void Update(TEntity data)
        {
            DataSource.Update(data);

            Cache.TryAddOrUpdate(data.Id, data);
        }

        /// <summary>
        /// Commit a delete operation for a single item in the database, updating the cache
        /// </summary>
        /// <param name="data">The item to delete</param>
        public void Delete(TEntity data)
        {
            DataSource.Delete(data);

            Cache.TryRemove<TKey, TEntity>(data.Id);
        }

        /// <summary>
        /// The total number of items cached for the table represented by this <see cref="CachedCrud{TEntity, TKey}"/>
        /// </summary>
        public int CachedCount => Cache.TableItemsCachedCount<TEntity>();

        /// <summary>
        /// Clear all entries from the cache
        /// </summary>
        public void ClearCache()
        {
            Cache.TryRemoveTable<TEntity>();
            Cache.TryAddTable<TKey, TEntity>(DataSource.GetSingle);
        }
    }
}
