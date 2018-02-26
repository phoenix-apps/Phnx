using System;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extension for <see cref="Array"/>
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Clear the array
        /// </summary>
        /// <param name="array">The array to clear</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        public static void Clear(this Array array)
        {
            Array.Clear(array, 0, array.Length);
        }

        /// <summary>
        /// Gets the index of the first occurance of a value within an array. Returns -1 if the value is not in the array
        /// </summary>
        /// <typeparam name="T">The type of array and value to get</typeparam>
        /// <param name="array">The array to search</param>
        /// <param name="value">The value to search for</param>
        /// <returns>Returns a 0 based index of the first occurance of the value in the array. If it is not found, returns -1</returns>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary>
        /// Gets the index of the last occurance of a value within an array. Returns -1 if the value is not in the array
        /// </summary>
        /// <typeparam name="T">The type of array and value to get</typeparam>
        /// <param name="array">The array to search</param>
        /// <param name="value">The value to search for</param>
        /// <returns>Returns a 0 based index of the first occurance of the value in the array. If it is not found, returns -1</returns>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        public static int LastIndexOf<T>(this T[] array, T value)
        {
            return Array.LastIndexOf(array, value);
        }

        /// <summary>
        /// Reverse the sequence of the elements within the array
        /// </summary>
        /// <param name="array">The array to reverse</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        public static void Reverse<T>(this T[] array)
        {
            Array.Reverse(array);
        }

        /// <summary>
        /// Reverse the sequence of a range of elements within the array
        /// </summary>
        /// <param name="array">The array to reverse</param>
        /// <param name="startIndex">The starting index of the section to reverse</param>
        /// <param name="length">The number of elements in the section to reverse</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than the lower bound of <paramref name="array"/> or <paramref name="length"/> is less than zero</exception>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/> and <paramref name="length"/> do not specify a valid range in <paramref name="array"/></exception>
        public static Array Reverse<T>(this T[] array, int startIndex, int length)
        {
            Array.Reverse(array, startIndex, length);

            return array;
        }

        /// <summary>
        /// Sorts the elements in an <see cref="Array"/> using the <see cref="IComparable"/> implementation of each element of the <see cref="Array"/>
        /// </summary>
        /// <param name="array">The <see cref="Array"/> to sort</param>
        /// <exception cref="ArgumentNullException"><paramref name="array">array</paramref> is null</exception>
        /// <exception cref="InvalidOperationException">One or more elements in <paramref name="array">array</paramref> do not implement the <see cref="IComparable"/> interface</exception>
        public static Array Sort<T>(this T[] array)
        {
            Array.Sort(array);

            return array;
        }

        /// <summary>
        /// Returns the number of bytes in the array
        /// </summary>
        /// <param name="array">The array to get the length of</param>
        /// <returns>The number of bytes in the array</returns>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="array"/> is not a primitive.</exception>
        /// <exception cref="OverflowException"><paramref name="array"/> is larger than 2 gigabytes (GB).</exception>
        public static int ByteLength(this Array array)
        {
            return Buffer.ByteLength(array);
        }

        /// <summary>
        /// Changes the number of elements in the array to the specified new size
        /// </summary>
        /// <param name="array">The array to resize, or null to create a new array with the specified size</param>
        /// <param name="newSize">The size of the new array</param>
        /// <typeparam name="T">The type of the elements in the array</typeparam>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="newSize"/> is less than zero</exception>
        public static T[] Resize<T>(this T[] array, int newSize)
        {
            Array.Resize(ref array, newSize);

            return array;
        }

        /// <summary>
        /// Gets the runtime types for each value in the array
        /// </summary>
        /// <param name="array">The array to get the types of</param>
        /// <returns>An array of types, each representing a value in the array</returns>
        public static Type[] GetTypeArray(this object[] array)
        {
            Type[] types = new Type[array.Length];

            for (var index = 0; index < array.Length; index++)
            {
                types[index] = array[index].GetType();
            }

            return types;
        }

        /// <summary>
        /// Fill this array with a value
        /// </summary>
        /// <param name="array">The array to fill</param>
        /// <param name="fillWith">The value to fill the array with</param>
        public static void Fill(this Array array, object fillWith)
        {
            for (int index = 0; index < array.Length; index++)
            {
                array.SetValue(fillWith, index);
            }
        }
        
        /// <summary>
        /// Fill this array with a value
        /// </summary>
        /// <param name="array">The array to fill</param>
        /// <param name="fillWith">The value to fill the array with</param>
        public static void Fill<T>(this T[] array, T fillWith)
        {
            for (long index = 0; index < array.Length; index++)
            {
                array[index] = fillWith;
            }
        }
    }
}