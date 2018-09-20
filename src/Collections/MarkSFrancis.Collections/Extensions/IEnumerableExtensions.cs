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
        /// <param name="source">The collection to copy from</param>
        /// <param name="startIndex">The index at which to start copying</param>
        /// <param name="count">The number of values to copy</param>
        /// <returns>The values from the collection within the range specified</returns>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="startIndex"/> is less than zero or <paramref name="count"/> is less than zero</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="startIndex"/> references an index greater than the number of items in the collection minus <paramref name="count"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> source, int startIndex, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startIndex < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(startIndex));
            }

            if (count < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(count));
            }

            IEnumerator<T> enumerator = source.GetEnumerator();

            for (int index = 0; index < startIndex; ++index)
            {
                // Skip value
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                }
            }

            for (int copiedCount = 0; copiedCount < count; ++copiedCount)
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException(nameof(count));
                }

                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }

        /// <summary>
        /// Copy a select range of values from the collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection to copy from</param>
        /// <param name="startIndex">The index at which to start copying. If this is the same as the number of items in the collection, no items are copied</param>
        /// <returns>The values from the <paramref name="startIndex"/> to the end of the collection</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero</exception>
        /// <exception cref="IndexOutOfRangeException"><paramref name="startIndex"/> references an index greater than the number of items in the collection</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        public static IEnumerable<T> CopyRange<T>(this IEnumerable<T> source, int startIndex)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startIndex < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(startIndex));
            }

            IEnumerator<T> enumerator = source.GetEnumerator();

            for (int index = 0; index < startIndex; ++index)
            {
                // Skip value
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                }
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }

        /// <summary>
        /// Creates a new <see cref="IEnumerable{T}"/> of the same size of <paramref name="source"/>, filling it with <paramref name="fillWith"/>
        /// </summary>
        /// <param name="source">The enumerable to fill</param>
        /// <param name="fillWith">The value to fill the enumerable with</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        public static IEnumerable<T> Fill<T>(this IEnumerable<T> source, T fillWith)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using (var enumerator = source.GetEnumerator())
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
        /// <param name="source">A collection that contains the values to concatenate</param>
        /// <param name="separator">The string to use as a separator. The separator is included in the returned string only if values has more than one element</param>
        /// <param name="toString">The method to use to convert each member to a <see cref="string"/>. If this is <see langword="null"/>, <see cref="object.ToString"/> is used. If this returns <see langword="null"/>, <see cref="string.Empty"/> is used instead of <see langword="null"/></param>
        /// <returns>A string that consists of the members of <paramref name="source"/> delimited by the <paramref name="separator"/> string. If <paramref name="source"/> has no members, the method returns <see cref="string.Empty"></see></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        public static string ToString<T>(this IEnumerable<T> source, string separator, Func<T, string> toString = null)
        {
            if (separator == null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            if (toString == null)
            {
                return string.Join(separator, source);
            }
            else
            {
                return string.Join(separator, source.Select(toString));
            }
        }
    }
}
