using System;
using System.Collections.Generic;

namespace Phnx.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> related to querying collections
    /// </summary>
    public static class IEnumerableLinqExtensions
    {
        /// <summary>Gets unique elements from a collection according to the key selector</summary>
        /// <param name="source">The collection to get distinct elements from</param>
        /// <param name="keySelector">A function to extract the key for each value</param>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/></typeparam>
        /// <returns>Unique elements by the <paramref name="keySelector"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            HashSet<TKey> knownDistincts = new HashSet<TKey>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                if (knownDistincts.Contains(key))
                {
                    continue;
                }

                yield return item;
                knownDistincts.Add(key);
            }
        }

        /// <summary>
        /// Get the value from a collection of values with the maximum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="source">The collection of values to get the maximum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the maximum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> was empty</exception>
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            T curMax = default(T);
            TKey curMaxValue = default(TKey);
            bool firstRun = true;

            foreach (var value in source)
            {
                if (firstRun)
                {
                    curMax = value;
                    curMaxValue = keySelector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMaxBy = keySelector(value);
                if (valueMaxBy.CompareTo(curMaxValue) == 1)
                {
                    // New max
                    curMax = value;
                    curMaxValue = valueMaxBy;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.SequenceEmpty(nameof(source)));
            }

            return curMax;
        }

        /// <summary>
        /// Get the value from a collection of values with the minimum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="source">The collection of values to get the minimum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the minimum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> was empty</exception>
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            T curMin = default(T);
            TKey curMinValue = default(TKey);
            bool firstRun = true;

            foreach (var value in source)
            {
                if (firstRun)
                {
                    curMin = value;
                    curMinValue = keySelector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMinBy = keySelector(value);
                if (valueMinBy.CompareTo(curMinValue) == -1)
                {
                    // New max
                    curMin = value;
                    curMinValue = valueMinBy;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException(nameof(source));
            }

            return curMin;
        }

        /// <summary>
        /// Gets whether two collections contain the same data using <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        /// <typeparam name="T">The type of values to compare</typeparam>
        /// <param name="source">The first collection to compare</param>
        /// <param name="rangeToCompare">The collection to compare with</param>
        /// <returns>Whether the two collections contain the same data. Returns <see langword="false"/> if either collection is <see langword="null"/></returns>
        public static bool EqualsRange<T>(this IEnumerable<T> source, IEnumerable<T> rangeToCompare)
        {
            if (source == null || rangeToCompare == null)
            {
                return false;
            }

            var comparer = ComparerHelpers.DefaultEqualityComparer<T>();

            using (var sourceEnumerator = source.GetEnumerator())
            {
                using (var rangeToCompareEnumerator = rangeToCompare.GetEnumerator())
                {
                    do
                    {
                        if (!sourceEnumerator.MoveNext())
                        {
                            // Reached the end of collection 1
                            // If we're not at the end of collection 2, they don't match as collection 2 must be longer
                            // If we are, they match
                            return !rangeToCompareEnumerator.MoveNext();
                        }

                        if (!rangeToCompareEnumerator.MoveNext())
                        {
                            return false;
                        }
                    } while (comparer.Equals(sourceEnumerator.Current, rangeToCompareEnumerator.Current));
                }
            }

            return false;
        }

        /// <summary>
        /// Converts this collection to a <see cref="List{T}"/> with a given default capacity. Useful if you know how many values will be in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection of values</param>
        /// <param name="capacity">The capacity to assign to the new <see cref="List{T}"/></param>
        /// <returns>A list of values as a copy of an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0</exception>
        public static List<T> ToList<T>(this IEnumerable<T> source, int capacity)
        {
            var newList = new List<T>(capacity);
            newList.AddRange(source);

            return newList;
        }

        /// <summary>
        /// Append multiple collections of <see cref="IEnumerable{T}"/> onto a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="source">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="source"/>. If any of these collections are <see langword="null"/>, they are treated as empty, and skipped</param>
        /// <returns>A collection containing all given collection's values</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params IEnumerable<T>[] collections)
        {
            return source.Append((IEnumerable<IEnumerable<T>>)collections);
        }

        /// <summary>
        /// Append multiple collections of <see cref="IEnumerable{T}"/> onto a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="source">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="source"/>. If any of these collections are <see langword="null"/>, they are treated as empty, and skipped</param>
        /// <returns>A collection containing all given collection's values</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<IEnumerable<T>> collections)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (collections == null)
            {
                throw new ArgumentNullException(nameof(collections));
            }

            foreach (var value in source)
            {
                yield return value;
            }

            foreach (var value in collections.Flatten())
            {
                yield return value;
            }
        }

        /// <summary>
        /// Flatten a two dimensional collection into a single one dimensional collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection to flatten. If any collections within <paramref name="source"/> are <see langword="null"/>, they are skipped</param>
        /// <returns>A one dimensional collection containing all the two dimensional collection's values</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var subCollection in source)
            {
                if (subCollection == null)
                {
                    // Skip
                    continue;
                }

                foreach (var value in subCollection)
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Get the index of an item that matches a criteria
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection to search</param>
        /// <param name="isMatch">The criteria that the value must pass in order to be a match</param>
        /// <returns>The index of the first occurance of an item within <paramref name="source"/> that passes <paramref name="isMatch"/>. If the item is not found, this returns -1</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="isMatch"/> is <see langword="null"/></exception>
        /// <exception cref="OverflowException"><paramref name="source"/> is longer than <see cref="int.MaxValue"/>, which meant the index overflowed</exception>
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> isMatch)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (isMatch == null)
            {
                throw new ArgumentNullException(nameof(isMatch));
            }

            int index = 0;
            foreach (var item in source)
            {
                if (isMatch(item))
                {
                    return index;
                }

                index = checked(index + 1);
            }

            // Not found
            return -1;
        }

        /// <summary>
        /// Get the index of an item that matches a criteria
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection to search</param>
        /// <param name="isMatch">The criteria that the value must pass in order to be a match</param>
        /// <returns>The index of the first occurance of an item within <paramref name="source"/> that passes <paramref name="isMatch"/>. If the item is not found, this returns -1</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="isMatch"/> is <see langword="null"/></exception>
        /// <exception cref="OverflowException"><paramref name="source"/> is longer than <see cref="long.MaxValue"/>, which meant the index overflowed</exception>
        public static long IndexOfLong<T>(this IEnumerable<T> source, Func<T, bool> isMatch)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (isMatch == null)
            {
                throw new ArgumentNullException(nameof(isMatch));
            }

            long index = 0;
            foreach (var item in source)
            {
                if (isMatch(item))
                {
                    return index;
                }

                index = checked(index + 1);
            }

            // Not found
            return -1L;
        }
    }
}
