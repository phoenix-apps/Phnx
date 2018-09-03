using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// Provides a way to enumerator over an <see cref="IEnumerator{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of value to enumerate over</typeparam>
    public class EnumerableEnumerator<T> : IEnumerable<T>
    {
        /// <summary>
        /// Create a new <see cref="EnumerableEnumerator{T}"/> from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="enumerator">The enumerator to enumerate over</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="enumerator"/> is <see langword="null"/></exception>
        public EnumerableEnumerator(IEnumerator<T> enumerator)
        {
            Enumerator = enumerator ?? throw ErrorFactory.Default.ArgumentNull(nameof(enumerator));
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>
        /// </summary>
        public IEnumerator<T> Enumerator { get; }

        /// <summary>
        /// Gets <see cref="Enumerator"/>
        /// </summary>
        /// <returns><see cref="Enumerator"/></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerator;
        }

        /// <summary>
        /// Gets <see cref="Enumerator"/>
        /// </summary>
        /// <returns><see cref="Enumerator"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Provides a way to enumerator over an <see cref="IEnumerator"/>
    /// </summary>
    public class EnumerableEnumerator : IEnumerable
    {
        /// <summary>
        /// Create a new <see cref="EnumerableEnumerator"/> from an <see cref="IEnumerator"/>
        /// </summary>
        /// <param name="enumerator">The enumerator to enumerate over</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="enumerator"/> is <see langword="null"/></exception>
        public EnumerableEnumerator(IEnumerator enumerator)
        {
            Enumerator = enumerator ?? throw ErrorFactory.Default.ArgumentNull(nameof(enumerator));
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>
        /// </summary>
        public IEnumerator Enumerator { get; }

        /// <summary>
        /// Gets <see cref="Enumerator"/>
        /// </summary>
        /// <returns><see cref="Enumerator"/></returns>
        public IEnumerator GetEnumerator()
        {
            return Enumerator;
        }
    }
}