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
        
        /// <summary>
        /// Performs a binary search using a given <see cref="Comparer{T}"/>. If the element is not found, it returns the flipped version of the index where the value should have been (for use in an insert). If the value is not found, the result will be negative. To restore the result to the index where it should have been found, use "~" to flip the resulting integer back
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="values">The values to search</param>
        /// <param name="searchFor">The value to search for</param>
        /// <param name="comparer">The comparer to use when searching</param>
        /// <returns>The index of the found item. If the element is not found, it returns the flipped version of the index where the value should have been (for use in an insert). If the value is not found, the result will be negative. To restore the result to the index where it should have been found, use "~" to flip the resulting integer back</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="comparer"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="comparer"/> returned an invalid result. <paramref name="comparer"/> results can only be -1, 0 or 1</exception>
        public static int BinarySearchBy<T>(this IList<T> values, T searchFor, Comparer<T> comparer)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }
            if (comparer == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(comparer));
            }

            int left = 0, right = values.Count - 1;

            BinarySearchRangeStart:

            if (left > right)
            {
                // Not found
                return ~Math.Max(left, right);
            }

            int mid = (left + right) / 2;

            var orderResult = comparer.Compare(searchFor, values[mid]);

            switch (orderResult)
            {
                case 0:
                    return mid;

                case -1:
                    // Value is lower than where we're looking. Move the range lower
                    right = mid - 1;
                    goto BinarySearchRangeStart;

                case 1:
                    // Value is higher than where we're looking. Move the range higher
                    left = mid + 1;
                    goto BinarySearchRangeStart;

                default:
                    throw ErrorFactory.Default.ArgumentOutOfRange(nameof(orderResult),
                        "Comparer result was invalid. Value of result was " + orderResult);
            }
        }
    }
}