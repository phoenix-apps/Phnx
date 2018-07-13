using MarkSFrancis.Data.EntityFrameworkCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EntityFrameworkCore.Repositories
{
    public class CrudRepository<TDbContext, T, TKey> : ICrudRepository<T, TKey>
        where T : class, IPrimaryKeyDataModel<TKey>, new()
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; }
        private readonly Func<TDbContext, DbSet<T>> _accessor;
        private DbSet<T> DbSet => _accessor(Context);

        public CrudRepository(TDbContext context, Func<TDbContext, DbSet<T>> accessor)
        {
            Context = context ?? throw ErrorFactory.Default.ArgumentNull(nameof(context));

            _accessor = accessor ?? throw ErrorFactory.Default.ArgumentNull(nameof(accessor));
        }

        public virtual void Create(T data)
        {
            DbSet.Add(data);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetSingle(TKey id)
        {
            return DbSet.FirstOrDefault(item => item.Id.Equals(id));
        }

        public virtual void Update(T data)
        {
            DbSet.Update(data);
        }

        public virtual void Delete(T data)
        {
            DbSet.Remove(data);
        }

        public virtual async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
