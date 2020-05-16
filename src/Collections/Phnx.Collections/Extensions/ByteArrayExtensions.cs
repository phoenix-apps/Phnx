using System;
using System.IO;

namespace Phnx.Collections
{
    /// <summary>
    /// Extensions for <see cref="T:byte[]"/>
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Create a new memory stream from an array of bytes
        /// </summary>
        /// <param name="source">The bytes to fill the <see cref="MemoryStream"/> with</param>
        /// <returns>A <see cref="MemoryStream"/> filled with specified bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        public static MemoryStream ToMemoryStream(this byte[] source)
        {
            return new MemoryStream(source);
        }

        /// <summary>
        /// Convert eight bytes at a specified position in a byte array to a 64-bit signed integer
        /// </summary>
        /// <param name="source">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> is less than 8 in length</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than or equal to the length of <paramref name="source"/> minus 7</exception>
        public static long ToLong(this byte[] source, int startIndex = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (source.Length < 8)
            {
                throw new ArgumentException("Array is too short to convert to int64", nameof(source));
            }

            if (startIndex > source.Length - 8)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            return BitConverter.ToInt64(source, startIndex);
        }

        /// <summary>
        /// Convert four bytes at a specified position in a byte array to a 32-bit signed integer
        /// </summary>
        /// <param name="source">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 32-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> is less than 4 in length</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than or equal to the length of <paramref name="source"/> minus 3</exception>
        public static int ToInt(this byte[] source, int startIndex = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (source.Length < 4)
            {
                throw new ArgumentException($"{nameof(source)} is too short to convert to an integer. 4 bytes are required, but {nameof(source)} is only {source.Length} long", nameof(source));
            }
            if (startIndex >= source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (startIndex > source.Length - 4)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"{nameof(startIndex)} is too close to the end of {nameof(source)} to convert to an integer. 4 bytes are required to convert to an integer, but there's only {source.Length - startIndex} available");
            }

            return BitConverter.ToInt32(source, startIndex);
        }

        /// <summary>
        /// Convert two bytes at a specified position in a byte array to a 32-bit signed integer
        /// </summary>
        /// <param name="source">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/> is greater than or equal to the length of <paramref name="source"/> minus 1</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="source"/> minus 1</exception>
        public static short ToShort(this byte[] source, int startIndex = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (source.Length < 2)
            {
                throw new ArgumentException("Array is too short to convert to int16", nameof(source));
            }

            if (startIndex > source.Length - 2)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            return BitConverter.ToInt16(source, startIndex);
        }
    }
}
