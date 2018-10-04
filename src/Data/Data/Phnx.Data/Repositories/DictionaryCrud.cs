using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Data.Repositories
{
    /// <summary>
    /// Provides CRUD operations for a <see cref="Dictionary{TKey, TValue}"/> that contains <see cref="IIdDataModel{TKey}"/>
    /// </summary>
    /// <typeparam name="TKey">The primary key for this model</typeparam>
    /// <typeparam name="TEntity">The model this CRUD repository controls</typeparam>
    public class DictionaryCrud<TKey, TEntity> : ICrud<TKey, TEntity>, IEnumerable<TEntity> where TEntity : IIdDataModel<TKey>
    {
        /// <summary>
        /// The data source wrapped by this
        /// </summary>
        public Dictionary<TKey, TEntity> Source { get; }

        /// <summary>
        /// Create a new <see cref="DictionaryCrud{TKey, TValue}"/> with an empty source
        /// </summary>
        public DictionaryCrud()
        {
            Source = new Dictionary<TKey, TEntity>();
        }

        /// <summary>
        /// Create a new <see cref="DictionaryCrud{TKey, TValue}"/> with a given source
        /// </summary>
        /// <param name="source">The data source to use</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> was <see langword="null"/></exception>
        public DictionaryCrud(Dictionary<TKey, TEntity> source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Add a new entry to <see cref="Source"/>
        /// </summary>
        /// <param name="data">The data to add</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> was <see langword="null"/></exception>
        public void Create(TEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Source.Add(data.Id, data);
        }

        /// <summary>
        /// Delete an entry from <see cref="Source"/>
        /// </summary>
        /// <param name="data">The data to remove</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> was <see langword="null"/></exception>
        public void Delete(TEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Delete(data.Id);
        }

        /// <summary>
        /// Delete an entry from <see cref="Source"/>
        /// </summary>
        /// <param name="key">The key to the data to delete</param>
        public void Delete(TKey key)
        {
            Source.Remove(key);
        }

        /// <summary>
        /// Get all items in the <see cref="Source"/> as an <see cref="IQueryable{T}"/>
        /// </summary>
        /// <returns>All items in the <see cref="Source"/> as an <see cref="IQueryable{T}"/></returns>
        IQueryable<TEntity> ICrud<TKey, TEntity>.Get()
        {
            return this.AsQueryable();
        }

        /// <summary>
        /// Get all records of <typeparamref name="TEntity"/> from the database. This has no cache support
        /// </summary>
        /// <returns>All records of <typeparamref name="TEntity"/> in the database</returns>
        public IEnumerable<TEntity> Get()
        {
            return this;
        }

        /// <summary>
        /// Get a single entry from <see cref="Source"/>
        /// </summary>
        /// <param name="id">The key to the entry to get</param>
        /// <returns>A single entry from <see cref="Source"/> which is associated with <paramref name="id"/></returns>
        public TEntity GetSingle(TKey id)
        {
            return Source[id];
        }

        /// <summary>
        /// Update an entry in <see cref="Source"/>
        /// </summary>
        /// <param name="data">The new data to save</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> was <see langword="null"/></exception>
        public void Update(TEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Source[data.Id] = data;
        }

        /// <summary>
        /// Clears all entries in <see cref="Source"/>
        /// </summary>
        public void Clear()
        {
            Source.Clear();
        }

        /// <summary>
        /// Enumerate through all entries in <see cref="Source"/>
        /// </summary>
        /// <returns>An enumerator for all entries in <see cref="Source"/></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Source.Select(entry => entry.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
