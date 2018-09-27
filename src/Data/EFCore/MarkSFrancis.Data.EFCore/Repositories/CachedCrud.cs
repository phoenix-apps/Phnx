using MarkSFrancis.Data.EFCore.Repositories.Interfaces;
using MarkSFrancis.Data.LazyLoad;
using System;
using System.Linq;

namespace MarkSFrancis.Data.EFCore.Repositories
{
    public class CachedCrud<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : IIdDataModel<TKey>
    {
        private readonly LazyDatabase cache;
        private bool _foundLast = false;

        /// <summary>
        /// Create a new <see cref="CachedCrud{TEntity, TKey}"/>
        /// </summary>
        /// <param name="cache">The cache to use for loading and commiting changes</param>
        /// <exception cref="ArgumentNullException"><paramref name="cache"/> was <see langword="null"/></exception>
        public CachedCrud(LazyDatabase cache)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.cache.TryAddTable<TKey, TEntity>(id =>
            {
                _foundLast = false;
                return default(TEntity);
            });
        }

        public void Create(TEntity data)
        {
            cache.TryAddOrUpdate(data.Id, data);
        }

        public void Delete(TEntity data)
        {
            cache.TryRemove<TKey, TEntity>(data.Id);
        }

        public IQueryable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public TEntity GetSingle(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
