using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkSFrancis.Extensions.Collections
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> items, int startIndex, int count)
        {
            if (startIndex < 0)
            {
                throw new IndexOutOfRangeException(nameof(startIndex) + " cannot be less than 0");
            }

            IEnumerator<T> enumerator = items.GetEnumerator();

            for (int index = 0; index < startIndex; index++)
            {
                // Skip item
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
            }

            for (int index = 0; index < count; index++)
            {
                yield return enumerator.Current;

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
            }

            enumerator.Dispose();
        }

        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> items, int startIndex)
        {
            if (startIndex < 0)
            {
                throw new IndexOutOfRangeException(nameof(startIndex) + " cannot be less than 0");
            }

            IEnumerator<T> enumerator = items.GetEnumerator();

            for (int index = 0; index < startIndex; index++)
            {
                // Skip item
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
            }

            yield return enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, bool indexesAlreadySorted, params int[] indexesToIgnore)
        {
            if (!indexesAlreadySorted)
            {
                Array.Sort(indexesToIgnore);
            }

            return Except(enumerable, indexesToIgnore);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, List<int> indexesToIgnore, bool indexesAlreadySorted = false)
        {
            if (!indexesAlreadySorted)
            {
                indexesToIgnore.Sort();
            }

            return Except(enumerable, (IList<int>)indexesToIgnore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="indexesToIgnore">Must be in order asc</param>
        /// <returns></returns>
        private static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, IList<int> indexesToIgnore)
        {
            int ignoreIndex = 0;

            // Get first in ignore index where index is > 0
            while (ignoreIndex < indexesToIgnore.Count && indexesToIgnore[ignoreIndex] < 0)
            {
                ignoreIndex++;
            }

            long index = 0;
            foreach (var val in enumerable)
            {
                if (ignoreIndex >= indexesToIgnore.Count || indexesToIgnore[ignoreIndex] != index)
                {
                    yield return val;
                }
                else
                {
                    ignoreIndex++;
                }
                index++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="indexesToIgnore">Must be in order asc</param>
        /// <returns></returns>
        private static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, IList<long> indexesToIgnore)
        {
            int ignoreIndex = 0;

            // Get first in ignore index where index is > 0
            while (ignoreIndex < indexesToIgnore.Count && indexesToIgnore[ignoreIndex] < 0)
            {
                ignoreIndex++;
            }

            long index = 0;
            foreach (var val in enumerable)
            {
                if (ignoreIndex >= indexesToIgnore.Count || indexesToIgnore[ignoreIndex] != index)
                {
                    yield return val;
                }
                else
                {
                    ignoreIndex++;
                }
                index++;
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, bool indexesAlreadySorted, params long[] indexesToIgnore)
        {
            if (!indexesAlreadySorted)
            {
                Array.Sort(indexesToIgnore);
            }

            return Except(enumerable, indexesToIgnore);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, List<long> indexesToIgnore, bool indexesAlreadySorted = false)
        {
            if (!indexesAlreadySorted)
            {
                indexesToIgnore.Sort();
            }

            return Except(enumerable, (IList<long>)indexesToIgnore);
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> distinction)
        {
            return enumerable.GroupBy(distinction).Select(x => x.First());
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            T curMax = default(T);
            TKey curMaxValue = default(TKey);
            bool firstRun = true;

            foreach (var item in enumerable)
            {
                if (firstRun)
                {
                    curMax = item;
                    curMaxValue = selector(item);
                    firstRun = false;
                    continue;
                }

                TKey itemValue = selector(item);
                if (itemValue.CompareTo(curMaxValue) == 1)
                {
                    // New max
                    curMax = item;
                    curMaxValue = itemValue;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException("The given sequence is empty");
            }

            return curMax;
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            T curMin = default(T);
            TKey curMinValue = default(TKey);
            bool firstRun = true;

            foreach (var item in enumerable)
            {
                if (firstRun)
                {
                    curMin = item;
                    curMinValue = selector(item);
                    firstRun = false;
                    continue;
                }

                TKey itemValue = selector(item);
                if (itemValue.CompareTo(curMinValue) == -1)
                {
                    // New max
                    curMin = item;
                    curMinValue = itemValue;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException("The given sequence is empty");
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