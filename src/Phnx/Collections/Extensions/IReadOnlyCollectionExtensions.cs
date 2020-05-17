using System;
using System.Collections.Generic;

namespace Phnx.Collections
{
    /// <summary>
    /// Extensions for <see cref="IReadOnlyCollection{T}"/>
    /// </summary>
    public static class IReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Converts this <see cref="IReadOnlyCollection{T}"/> to a <see cref="List{T}"/>, setting the default capacity to the size of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of items in the collection</typeparam>
        /// <param name="source">The values to convert to a <see cref="List{T}"/></param>
        /// <returns>A new <see cref="List{T}"/> which contains all the items that were in <paramref name="source"/>, with a capacity of the total number of values in <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        public static List<T> ToList<T>(this IReadOnlyCollection<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var newList = new List<T>(source.Count);
            newList.AddRange(source);

            return newList;
        }
    }
}
