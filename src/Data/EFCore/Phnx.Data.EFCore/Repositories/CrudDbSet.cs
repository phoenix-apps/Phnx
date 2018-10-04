using Microsoft.EntityFrameworkCore;
using Phnx.Data.Repositories;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Repositories
{
    /// <summary>
    /// Provides Create, Read, Update and Delete operations on data, via <see cref="Microsoft.EntityFrameworkCore"/>. This does not save changes, so you'll need to use <see cref="DbContext.SaveChanges()"/> whenever changing data
    /// </summary>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/> that <typeparamref name="TEntity"/> is stored in</typeparam>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    public class CrudDbSet<TDbContext, TEntity, TKey> : ICrud<TEntity, TKey>
        where TEntity : class, IIdDataModel<TKey>
        where TDbContext : DbContext
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        protected TDbContext Context { get; }

        /// <summary>
        /// Whether to autosave changes
        /// </summary>
        public bool AutoSave { get; set; }

        /// <summary>
        /// The DbSet for the entities wrapped by this repository
        /// </summary>
        protected DbSet<TEntity> Table { get; }

        /// <summary>
        /// Create a new <see cref="CrudDbSet{TDbContext, TEntity, TKey}"/>
        /// </summary>
        /// <param name="context">The <typeparamref name="TDbContext"/> to use</param>
        /// <param name="tableAccessor">The <see cref="DbSet{TEntity}"/> to use</param>
        /// <param name="autoSave">Whether to automatically save any changes to the database</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> or <paramref name="tableAccessor"/> was <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="tableAccessor"/> returns <see langword="null"/></exception>
        public CrudDbSet(TDbContext context, Func<TDbContext, DbSet<TEntity>> tableAccessor, bool autoSave)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            if (tableAccessor is null)
            {
                throw new ArgumentNullException(nameof(tableAccessor));
            }

            Table = tableAccessor(Context) ??
                throw new ArgumentException($"{nameof(tableAccessor)} gave a null table", nameof(tableAccessor));

            AutoSave = autoSave;
        }

        /// <summary>
        /// Commit <paramref name="data"/> to the database, ready to save. Make sure to use <see cref="DbContext.SaveChanges()"/> to save those changes to the database
        /// </summary>
        /// <param name="data">The data to commit</param>
        public virtual void Create(TEntity data)
        {
            Table.Add(data);

            Autosave();
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
            return Table.AsNoTracking().FirstOrDefault(item => item.Id.Equals(id));
        }

        /// <summary>
        /// Commit an update of a single item, ready to save. Make sure to use <see cref="DbContext.SaveChanges()"/> to save those changes to the database
        /// </summary>
        /// <param name="data">The item to update</param>
        public virtual void Update(TEntity data)
        {
            Table.Update(data);

            Autosave();
        }

        /// <summary>
        /// Commit a delete operation for a single item, ready to save. Make sure to use <see cref="DbContext.SaveChanges()"/> to save those changes to the database
        /// </summary>
        /// <param name="data">The item to delete</param>
        public virtual void Delete(TEntity data)
        {
            Table.Remove(data);

            Autosave();
        }

        /// <summary>
        /// Saves all changes if <see cref="AutoSave"/> is <see langword="true"/>
        /// </summary>
        protected void Autosave()
        {
            if (AutoSave)
            {
                Context.SaveChanges();
            }
        }
    }
}
