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
    }
}