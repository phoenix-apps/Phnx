using System;
using System.IO;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="T:byte[]"/>
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Create a new memory stream from an array of bytes
        /// </summary>
        /// <param name="bytes">The bytes to fill the <see cref="MemoryStream"/> with</param>
        /// <returns>A <see cref="MemoryStream"/> filled with specified bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is <see langword="null"/></exception>
        public static MemoryStream ToMemoryStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Convert eight bytes at a specified position in a byte array to a 64-bit signed integer
        /// </summary>
        /// <param name="bytes">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/> is greater than or equal to the length of <paramref name="bytes"/> minus 7, and is less than or equal to the length of <paramref name="bytes"/> minus 1</exception>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="bytes"/> minus 1</exception>
        public static long ToLong(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt64(bytes, startIndex);
        }

        /// <summary>
        /// Convert four bytes at a specified position in a byte array to a 32-bit signed integer
        /// </summary>
        /// <param name="bytes">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/> is greater than or equal to the length of <paramref name="bytes"/> minus 3, and is less than or equal to the length of <paramref name="bytes"/> minus 1</exception>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="bytes"/> minus 1</exception>
        public static int ToInt(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt32(bytes, startIndex);
        }

        /// <summary>
        /// Convert two bytes at a specified position in a byte array to a 32-bit signed integer
        /// </summary>
        /// <param name="bytes">An array of bytes</param>
        /// <param name="startIndex">The starting position within value</param>
        /// <returns>A 64-bit signed integer formed by eight bytes beginning at <paramref name="startIndex"/></returns>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/> is greater than or equal to the length of <paramref name="bytes"/> minus 1</exception>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="bytes"/> minus 1</exception>
        public static short ToShort(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt16(bytes, startIndex);
        }
    }
}