using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IList{T}"/>
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Gets the last value from an <see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of values in <paramref name="values"/></typeparam>
        /// <param name="values">The values to get the last from</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/></exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="values"/> is an empty collection</exception>
        public static T Last<T>(this IList<T> values)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            if (values.Count == 0)
            {
                throw ErrorFactory.Default.CollectionEmpty(nameof(values));
            }

            return values[values.Count - 1];
        }
        
        /// <summary>
        /// Fill this list with a value
        /// </summary>
        /// <param name="values">The list to fill</param>
        /// <param name="fillWith">The value to fill the list with</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/></exception>
        public static IList<T> Fill<T>(this IList<T> values, T fillWith)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            for (int index = 0; index < values.Count; index++)
            {
                values[index] = fillWith;
            }

            return values;
        }

        /// <summary>
        /// Gets whether two collections contain the same data using <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        /// <typeparam name="T">The type of values to compare</typeparam>
        /// <param name="list">The first collection to compare</param>
        /// <param name="rangeToCompare">The collection to compare with</param>
        /// <returns>Whether the two collections contain the same data. Returns <see langword="false"/> if either collection is <see langword="null"/></returns>
        public static bool EqualsRange<T>(this IList<T> list, IList<T> rangeToCompare)
        {
            if (list == null || rangeToCompare == null)
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;

            if (list.Count != rangeToCompare.Count)
            {
                return false;
            }

            for (int index = 0; index < list.Count; index++)
            {
                if (!comparer.Equals(list[index], rangeToCompare[index]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}