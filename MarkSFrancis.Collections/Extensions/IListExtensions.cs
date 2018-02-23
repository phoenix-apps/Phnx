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
    }
}