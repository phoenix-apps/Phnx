using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IList{T}"/>
    /// </summary>
    public static class IListExtensions
    {
        public static T Last<T>(this IList<T> values)
        {
            if (values.Count == 0)
            {
                throw new IndexOutOfRangeException(nameof(values) + " is an empty " + nameof(IList<T>));
            }

            return values[values.Count - 1];
        }

        public static IList<T> Fill<T>(this IList<T> values, T fillWith)
        {
            for (int index = 0; index < values.Count; index++)
            {
                values[index] = fillWith;
            }

            return values;
        }

        public static bool IsEqualToRange<T>(this IList<T> list, IList<T> rangeToCompare)
        {
            var comparer = EqualityComparer<T>.Default;

            if (list == null || rangeToCompare == null)
            {
                return false;
            }

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