using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    public static class IListExtensions
    {
        public static T Last<T>(this IList<T> items)
        {
            if (items.Count == 0)
            {
                throw new IndexOutOfRangeException(nameof(items) + " is an empty " + nameof(IList<T>));
            }

            return items[items.Count - 1];
        }

        public static IList<T> Fill<T>(this IList<T> items, T fillWith)
        {
            for (int index = 0; index < items.Count; index++)
            {
                items[index] = fillWith;
            }

            return items;
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