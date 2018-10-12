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
        /// Gets the last value from an <see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of values in <paramref name="source"/></typeparam>
        /// <param name="source">The values to get the last from</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is an empty collection</exception>
        public static T Last<T>(this IList<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Count == 0)
            {
                throw new InvalidOperationException(nameof(source));
            }

            return source[source.Count - 1];
        }

        /// <summary>
        /// Converts this <see cref="IList{T}"/> to a <see cref="List{T}"/>, setting the default capacity to the size of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of items in the collection</typeparam>
        /// <param name="source">The values to convert to a <see cref="List{T}"/></param>
        /// <returns>A new <see cref="List{T}"/> which contains all the items that were in <paramref name="source"/>, with a capacity of the total number of values in <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        public static List<T> ToList<T>(this IList<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var newList = new List<T>(source.Count);
            newList.AddRange(source);

            return newList;
        }

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

        /// <summary>
        /// Gets whether two collections contain the same data using <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        /// <typeparam name="T">The type of values to compare</typeparam>
        /// <param name="source">The first collection to compare</param>
        /// <param name="rangeToCompare">The collection to compare with</param>
        /// <returns>Whether the two collections contain the same data. Returns <see langword="false"/> if either collection is <see langword="null"/></returns>
        public static bool EqualsRange<T>(this IList<T> source, IList<T> rangeToCompare)
        {
            if (source == null || rangeToCompare == null)
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;

            if (source.Count != rangeToCompare.Count)
            {
                return false;
            }

            for (int index = 0; index < source.Count; index++)
            {
                if (!comparer.Equals(source[index], rangeToCompare[index]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Searches a range of elements in a sorted <see cref="IList{T}"/> for a value
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The values to search</param>
        /// <param name="value">The value to search for</param>
        /// <returns>The index of the specified <paramref name="value"/> in the specified <paramref name="source"/>, if <paramref name="value"/> is found; otherwise, a negative number. If <paramref name="value"/> is not found and <paramref name="value"/> is less than one or more elements in <paramref name="source"/>, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value"/>. If <paramref name="value"/> is not found and <paramref name="value"/> is greater than all elements in <paramref name="source"/>, the negative number returned is the bitwise complement of the index of the last element plus 1. If this method is called with a non-sorted <paramref name="source"/>, the return value can be incorrect and a negative number could be returned, even if <paramref name="value"/> is present in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The default comparer for <typeparamref name="T"/> returned an invalid result. The comparer's results can only be -1, 0 or 1</exception>
        public static int BinarySearch<T>(this IList<T> source, T value)
        {
            return BinarySearch(source, value, null);
        }

        /// <summary>
        /// Searches a range of elements in a sorted <see cref="IList{T}"/> for a value
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The values to search</param>
        /// <param name="value">The value to search for</param>
        /// <param name="comparer">The comparer to use when searching. If this is <see langword="null"/>, the default <see cref="IComparer{T}"/> is used</param>
        /// <returns>The index of the specified <paramref name="value"/> in the specified <paramref name="source"/>, if <paramref name="value"/> is found; otherwise, a negative number. If <paramref name="value"/> is not found and <paramref name="value"/> is less than one or more elements in <paramref name="source"/>, the negative number returned is the bitwise complement of the index of the first element that is larger than <paramref name="value"/>. If <paramref name="value"/> is not found and <paramref name="value"/> is greater than all elements in <paramref name="source"/>, the negative number returned is the bitwise complement of the index of the last element plus 1. If this method is called with a non-sorted <paramref name="source"/>, the return value can be incorrect and a negative number could be returned, even if <paramref name="value"/> is present in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="comparer"/> returned an invalid result. <paramref name="comparer"/> results can only be -1, 0 or 1</exception>
        public static int BinarySearch<T>(this IList<T> source, T value, IComparer<T> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            bool comparerIsValidated = comparer != null;
            if (!comparerIsValidated)
            {
                comparer = ComparerHelpers.DefaultComparer<T>();

                if (comparer == null)
                {
                    throw new InvalidOperationException(typeof(T).FullName + " does not have a default comparer");
                }
            }

            int left = 0, right = source.Count - 1;

            BinarySearchRangeStart:

            if (left > right)
            {
                // Not found
                return ~Math.Max(left, right);
            }

            int mid = (left + right) / 2;

            int orderResult;

            if (comparerIsValidated)
            {
                orderResult = comparer.Compare(value, source[mid]);
            }
            else
            {
                try
                {
                    orderResult = comparer.Compare(value, source[mid]);
                    comparerIsValidated = true;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(typeof(T).FullName + " does not have a default comparer");
                }
            }

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
                    throw new ArgumentOutOfRangeException(nameof(comparer),
                        "Comparer result was invalid. Value of result was " + orderResult);
            }
        }
    }
}