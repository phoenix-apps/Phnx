using System;
using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerator{T}"/>
    /// </summary>
    public static class IEnumeratorExtensions
    {
        /// <summary>
        /// Convert this <see cref="IEnumerator{T}"/> to an <see cref="EnumerableEnumerator{T}"/>, ready to use in a <see langword="foreach"/>, linq expression, etc
        /// </summary>
        /// <param name="enumerator">The enumerator to enumerate over</param>
        /// <exception cref="ArgumentNullException"><paramref name="enumerator"/> is <see langword="null"/></exception>
        public static EnumerableEnumerator<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            return new EnumerableEnumerator<T>(enumerator);
        }

        /// <summary>
        /// Convert this <see cref="IEnumerator"/> to an <see cref="EnumerableEnumerator"/>, ready to use in a <see langword="foreach"/>, linq expression, etc
        /// </summary>
        /// <param name="enumerator">The enumerator to enumerate over</param>
        /// <exception cref="ArgumentNullException"><paramref name="enumerator"/> is <see langword="null"/></exception>
        public static EnumerableEnumerator ToEnumerable(this IEnumerator enumerator)
        {
            return new EnumerableEnumerator(enumerator);
        }
    }
}
