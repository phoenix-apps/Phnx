using System;
using System.Collections.Generic;

namespace Phnx.Collections
{
    /// <summary>
    /// Extensions for <see cref="IList{T}"/>
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Fill this list with a value
        /// </summary>
        /// <param name="source">The list to fill</param>
        /// <param name="fillWith">The value to fill the list with</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        public static IList<T> Fill<T>(this IList<T> source, T fillWith)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            for (int index = 0; index < source.Count; index++)
            {
                source[index] = fillWith;
            }

            return source;
        }
    }
}