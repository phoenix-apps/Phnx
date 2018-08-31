using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerator{T}"/>
    /// </summary>
    public static class IEnumeratorExtensions
    {
        /// <summary>
        /// Convert this <see cref="IEnumerator{T}"/> to an <see cref="EnumerableEnumerator{T}"/>, ready to use in a <see langword="foreach"/>
        /// </summary>
        /// <param name="sourceEnumerator">The enumerator to enumerate over</param>
        public static EnumerableEnumerator<T> ToEnumerable<T>(this IEnumerator<T> sourceEnumerator)
        {
            return new EnumerableEnumerator<T>(sourceEnumerator);
        }
    }
}