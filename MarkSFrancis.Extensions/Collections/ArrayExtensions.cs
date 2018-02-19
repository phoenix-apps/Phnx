using System;
using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Extensions.Collections
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Checks if the items are IList. If so - uses IList.Count. If not, falls back to IEnumerable count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static int BetterCount<T>(this IEnumerable<T> items)
        {
            var list = items as IList;
            return list?.Count ?? System.Linq.Enumerable.Count(items);
        }

        public static Array Clear(this Array arr)
        {
            Array.Clear(arr, 0, arr.Length);

            return arr;
        }

        public static int IndexOf<T>(this T[] arr, T value)
        {
            return Array.IndexOf(arr, value);
        }

        public static int LastIndexOf<T>(this T[] arr, T value)
        {
            return Array.LastIndexOf(arr, value);
        }

        public static Array Reverse(this Array arr)
        {
            Array.Reverse(arr);

            return arr;
        }

        /// <summary>
        /// Reverses the sequence of the elements in a range of elements in the one-dimensional .
        /// </summary>
        /// <param name="arr">The one-dimensional  to reverse.</param>
        /// <param name="startIndex">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        public static Array Reverse(this Array arr, int startIndex, int length)
        {
            Array.Reverse(arr, startIndex, length);

            return arr;
        }
        
        public static Array Sort(this Array arr)
        {
            Array.Sort(arr);

            return arr;
        }

        public static int ByteLength(this Array arr)
        {
            return Buffer.ByteLength(arr);
        }

        public static T[] Resize<T>(this T[] arr, int newSize)
        {
            Array.Resize(ref arr, newSize);

            return arr;
        }

        public static Type[] GetTypeArray(this object[] arr)
        {
            Type[] types = new Type[arr.Length];

            for (var index = 0; index < arr.Length; index++)
            {
                types[index] = arr[index].GetType();
            }

            return types;
        }

        public static Array Fill(this Array arr, object fillWith)
        {
            for (int index = 0; index < arr.Length; index++)
            {
                arr.SetValue(fillWith, index);
            }

            return arr;
        }

        public static T[] Fill<T>(this T[] arr, T fillWith)
        {
            for (long index = 0; index < arr.Length; index++)
            {
                arr[index] = fillWith;
            }

            return arr;
        }
    }
}