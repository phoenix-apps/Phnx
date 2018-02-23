using System;
using System.IO;

namespace MarkSFrancis.Collections.Extensions
{
    public static class ByteArrayExtensions
    {
        public static MemoryStream ToMemoryStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static long ToLong(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt64(bytes, startIndex);
        }

        public static int ToInt(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt32(bytes, startIndex);
        }

        public static short ToShort(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt16(bytes, startIndex);
        }
    }
}