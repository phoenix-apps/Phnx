using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return values.GroupBy(keySelector).Select(x => x.First());
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            T curMax = default(T);
            TKey curMaxValue = default(TKey);
            bool firstRun = true;

            foreach (var value in enumerable)
            {
                if (firstRun)
                {
                    curMax = value;
                    curMaxValue = selector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMaxBy = selector(value);
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

        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            T curMin = default(T);
            TKey curMinValue = default(TKey);
            bool firstRun = true;

            foreach (var value in enumerable)
            {
                if (firstRun)
                {
                    curMin = value;
                    curMinValue = selector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMinBy = selector(value);
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

        public static IEnumerable<T> Fill<T>(this IEnumerable<T> enumerable, T fillWith)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return fillWith;
                }
            }
        }

        public static bool IsEqualToRange<T>(this IEnumerable<T> enumerable, IEnumerable<T> rangeToCompare)
        {
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

        public static List<T> ToList<T>(this IEnumerable<T> enumerable, int capacity)
        {
            var newList = new List<T>(capacity);
            newList.AddRange(enumerable);

            return newList;
        }

        public static string ToString<T>(this IEnumerable<T> enumerable, string seperator, int startIndex = 0, Func<T, string> toString = null)
        {
            if (toString == null)
            {
                toString = new Func<T, string>(t => t.ToString());
            }

            enumerable = enumerable.Skip(startIndex);

            StringBuilder str = new StringBuilder();

            foreach (var obj in enumerable)
            {
                str.Append(toString(obj) + seperator);
            }

            if (str.Length > 0)
            {
                // Remove trailing seperator
                str.Remove(str.Length - seperator.Length, seperator.Length);
            }

            return str.ToString();
        }

        public static string ToString<T>(this IEnumerable<T> enumerable, string seperator, int startIndex, int count, Func<T, string> toString = null)
        {
            if (toString == null)
            {
                toString = t => t.ToString();
            }

            enumerable = enumerable.Skip(startIndex).Take(count);
            StringBuilder str = new StringBuilder();

            foreach (var obj in enumerable)
            {
                str.Append(toString(obj) + seperator);
            }

            if (str.Length > 0)
            {
                // Remove trailing seperator
                str.Remove(str.Length - seperator.Length, seperator.Length);
            }

            return str.ToString();
        }
    }
}