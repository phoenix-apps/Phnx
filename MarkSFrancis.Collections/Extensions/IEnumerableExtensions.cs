using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Copy a select range of values from the collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="values">The collection to copy from</param>
        /// <param name="startIndex">The index at which to start copying</param>
        /// <param name="count">The number of values to copy</param>
        /// <returns>The values from the collection within the range specified</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or <paramref name="count"/> is less than zero</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="startIndex"/> references an index greater than the number of items in the collection minus <paramref name="count"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is null</exception>
        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> values, int startIndex, int count)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            if (startIndex < 0)
            {
                throw ErrorFactory.Default.ArgumentLessThanZero(nameof(startIndex));
            }

            if (count < 0)
            {
                throw ErrorFactory.Default.ArgumentLessThanZero(nameof(count));
            }

            IEnumerator<T> enumerator = values.GetEnumerator();

            for (int index = 0; index < startIndex; index++)
            {
                // Skip value
                if (!enumerator.MoveNext())
                {
                    throw ErrorFactory.Default.IndexOutOfRange(nameof(startIndex), index);
                }
            }

            for (int index = 0; index < count; index++)
            {
                yield return enumerator.Current;

                if (!enumerator.MoveNext())
                {
                    throw ErrorFactory.Default.ArgumentOutOfRange(nameof(count));
                }
            }

            enumerator.Dispose();
        }

        /// <summary>
        /// Copy a select range of values from the collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="values">The collection to copy from</param>
        /// <param name="startIndex">The index at which to start copying</param>
        /// <returns>The values from the collection within the range specified</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="startIndex"/> references an index greater than or equal to the number of items in the collection</exception>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is null</exception>
        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> values, int startIndex)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            if (startIndex < 0)
            {
                throw ErrorFactory.Default.ArgumentLessThanZero(nameof(startIndex));
            }

            IEnumerator<T> enumerator = values.GetEnumerator();

            for (int index = 0; index < startIndex; index++)
            {
                // Skip value
                if (!enumerator.MoveNext())
                {
                    throw ErrorFactory.Default.IndexOutOfRange(nameof(startIndex), index);
                }
            }

            yield return enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }

        /// <summary>Gets unique elements from a collection according to the key selector</summary>
        /// <param name="values">The collection to get distinct elements from</param>
        /// <param name="keySelector">A function to extract the key for each value</param>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/></typeparam>
        /// <returns>Unique elements by the <paramref name="keySelector"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="keySelector"/> is null</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> values, Func<TSource, TKey> keySelector)
        {
            if (values == null)
            {
                ErrorFactory.Default.ArgumentNull(nameof(values));
            }
            else if (keySelector == null)
            {
                ErrorFactory.Default.ArgumentNull(nameof(keySelector));
            }

            return values.GroupBy(keySelector).Select(x => x.First());
        }

        /// <summary>
        /// Get the value from a collection of values with the maximum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="enumerable">The collection of values to get the maximum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the maximum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="enumerable"/> was empty</exception>
        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (enumerable == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(enumerable));
            }
            else if (keySelector == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(keySelector));
            }

            T curMax = default(T);
            TKey curMaxValue = default(TKey);
            bool firstRun = true;

            foreach (var value in enumerable)
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
                throw ErrorFactory.Default.CollectionEmpty(nameof(enumerable));
            }

            return curMax;
        }

        /// <summary>
        /// Get the value from a collection of values with the minimum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="enumerable">The collection of values to get the minimum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the minimum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="enumerable"/> was empty</exception>
        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (enumerable == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(enumerable));
            }
            else if (keySelector == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(keySelector));
            }

            T curMin = default(T);
            TKey curMinValue = default(TKey);
            bool firstRun = true;

            foreach (var value in enumerable)
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
                throw ErrorFactory.Default.CollectionEmpty(nameof(enumerable));
            }

            return curMin;
        }

        /// <summary>
        /// Fill this enumerable with a value
        /// </summary>
        /// <param name="enumerable">The enumerable to fill</param>
        /// <param name="fillWith">The value to fill the enumerable with</param>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is <see langword="null"/></exception>
        public static IEnumerable<T> Fill<T>(this IEnumerable<T> enumerable, T fillWith)
        {
            if (enumerable == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return fillWith;
                }
            }
        }

        /// <summary>
        /// Gets whether two collections contain the same data using <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        /// <typeparam name="T">The type of values to compare</typeparam>
        /// <param name="enumerable">The first collection to compare</param>
        /// <param name="rangeToCompare">The collection to compare with</param>
        /// <returns>Whether the two collections contain the same data. Returns <see langword="false"/> if either collection is <see langword="null"/></returns>
        public static bool EqualsRange<T>(this IEnumerable<T> enumerable, IEnumerable<T> rangeToCompare)
        {
            if (enumerable == null || rangeToCompare == null)
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;

            using (var enumerator1 = enumerable.GetEnumerator())
            {
                using (var enumerator2 = rangeToCompare.GetEnumerator())
                {
                    do
                    {
                        if (!comparer.Equals(enumerator1.Current, enumerator2.Current))
                        {
                            return false;
                        }

                        if (!enumerator1.MoveNext())
                        {
                            // Reached the end of collection 1
                            // If we're not at the end of collection 2, they don't match as collection 2 must be longer
                            // If we are, they match
                            return !enumerator2.MoveNext();
                        }

                        if (!enumerator2.MoveNext())
                        {
                            return false;
                        }
                    } while (true);
                }
            }
        }

        /// <summary>
        /// Converts this collection to a <see cref="List{T}"/> with a given default capacity. Useful if you know how many values will be in <paramref name="enumerable"/>
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="enumerable">The collection of values</param>
        /// <param name="capacity">The capacity to assign to the new <see cref="List{T}"/></param>
        /// <returns>A list of values as a copy of an <see cref="IEnumerable{T}"/></returns>
        public static List<T> ToList<T>(this IEnumerable<T> enumerable, int capacity)
        {
            var newList = new List<T>(capacity);
            newList.AddRange(enumerable);

            return newList;
        }

        /// <summary>
        /// Concatenates a range of the members of a constructed <see cref="IEnumerable{T}"></see> collection, using <paramref name="toString"/> to convert each member to text, and then using the specified separator between each member
        /// </summary>
        /// <param name="enumerable">A collection that contains the values to concatenate</param>
        /// <param name="separator">The string to use as a separator. The separator is included in the returned string only if values has more than one element</param>
        /// <param name="startIndex">The index at which the members to concatenate begin. If this is beyond the end of the <paramref name="enumerable"/>, <see cref="string.Empty"/> is returned</param>
        /// <param name="toString">The method to use to convert each member to a <see cref="string"/>. If this is <see langword="null"/>, <see cref="object.ToString"/> is used</param>
        /// <returns>A string that consists of the members of <paramref name="enumerable"/> delimited by the <paramref name="separator"/> string. If <paramref name="enumerable"/> has no members, or the <paramref name="startIndex"/> is beyond the end of the <paramref name="enumerable"/>, the method returns <see cref="String.Empty"></see></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero</exception>
        public static string ToString<T>(this IEnumerable<T> enumerable, string separator, int startIndex = 0, Func<T, string> toString = null)
        {
            if (toString == null)
            {
                toString = t => t.ToString();
            }

            var newEnumerable = enumerable.Skip(startIndex).Select(toString);

            return string.Join(separator, newEnumerable);
        }

        /// <summary>
        /// Concatenates a range of the members of a constructed <see cref="IEnumerable{T}"></see> collection, using <paramref name="toString"/> to convert each member to text, and then using the specified separator between each member
        /// </summary>
        /// <param name="enumerable">A collection that contains the values to concatenate</param>
        /// <param name="separator">The string to use as a separator. The separator is included in the returned string only if values has more than one element</param>
        /// <param name="startIndex">The index at which the members to concatenate begin. If this is beyond the end of the <paramref name="enumerable"/>, <see cref="string.Empty"/> is returned</param>
        /// <param name="count">The maximum number of values to concatenate. If this goes beyond the end of <paramref name="enumerable"/>, only the values up to the end will be concatenated</param>
        /// <param name="toString">The method to use to convert each member to a <see cref="string"/>. If this is <see langword="null"/>, <see cref="object.ToString"/> is used</param>
        /// <returns>A string that consists of the members of <paramref name="enumerable"/> delimited by the <paramref name="separator"/> string. If <paramref name="enumerable"/> has no members, or the <paramref name="startIndex"/> is beyond the end of the <paramref name="enumerable"/>, the method returns <see cref="String.Empty"></see></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> or <paramref name="count"/> is less than zero</exception>
        public static string ToString<T>(this IEnumerable<T> enumerable, string separator, int startIndex, int count, Func<T, string> toString = null)
        {
            if (startIndex < 0)
            {
                ErrorFactory.Default.ArgumentLessThanZero(nameof(startIndex));
            }
            if (count < 0)
            {
                ErrorFactory.Default.ArgumentLessThanZero(nameof(count));
            }

            if (toString == null)
            {
                toString = t => t.ToString();
            }

            var newEnumerable = enumerable.Skip(startIndex).Take(count).Select(toString);

            return string.Join(separator, newEnumerable);
        }

        /// <summary>
        /// Flatten multiple collections of <see cref="IEnumerable{T}"/> into a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="collection">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="collection"/></param>
        /// <returns>A collection containing all other collection's values</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, params IEnumerable<T>[] collections)
        {
            return collection.Flatten((IEnumerable<IEnumerable<T>>)collections);
        }

        /// <summary>
        /// Flatten multiple collections of <see cref="IEnumerable{T}"/> into a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="collection">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="collection"/></param>
        /// <returns>A collection containing all other collection's values</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, IEnumerable<IEnumerable<T>> collections)
        {
            foreach (var value in collection)
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
        /// <param name="collection">The collection to flatten</param>
        /// <returns>A one dimensional collection containing all the two dimensional collection's values</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            foreach (var subCollection in collection)
            {
                foreach (var value in subCollection)
                {
                    yield return value;
                }
            }
        }
    }
}