using System;

namespace MarkSFrancis.Data
{
    public static class GetRandom
    {
        static readonly char[] AlphaNumericChars =
        {
            '!', '\"', '£', '$', '%', '^', '&', '*', '(', ')', '\'', '-', '_', '=',
            '`', '¬', '|', '\\', ',', '<', '.', '>', '/', '?', ';', ':', '@', '~', '#', ']', '[', '}', '{', 'A', 'B',
            'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
            'Z', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
            'x', 'y', 'z'
        };
        private static Random Random => RandomHelper.Random;
        public static string String(int length)
        {
            string ReturnValue = "";
            for (int i = 0; i < length; i++)
            {
                ReturnValue += AlphaNumericChars[Random.Next(0, AlphaNumericChars.Length)];
            }
            return ReturnValue;
        }
        /// <summary>
        /// Generates a random string with a length between 1 and 19 characters
        /// </summary>
        /// <returns></returns>
        public static string String()
        {
            return String(Random.Next(1, 20));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static ulong Ulong(ulong minimum = ulong.MinValue, ulong maximum = ulong.MaxValue)
        {
            byte[] buffer = new byte[8];
            Random.NextBytes(buffer);
            return (ulong)(BitConverter.ToInt64(buffer, 0)) % (maximum - minimum) + minimum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static long Long(long minimum = long.MinValue, long maximum = long.MaxValue)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return Math.Abs(longRand % (maximum - minimum)) + minimum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static int Int(int minimum = int.MinValue, int maximum = int.MaxValue)
        {
            return Random.Next(minimum, maximum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static uint UInt(uint minimum = uint.MinValue, long maximum = uint.MaxValue)
        {
            var min = BitConverter.ToInt32(BitConverter.GetBytes(minimum), 0); BitConverter.ToInt32(BitConverter.GetBytes(minimum), 0);
            var max = BitConverter.ToInt32(BitConverter.GetBytes(maximum), 0);
            return BitConverter.ToUInt32(BitConverter.GetBytes(Random.Next(
                Math.Min(min, max),
                Math.Max(min, max))), 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static short Short(short minimum = short.MinValue, short maximum = short.MaxValue)
        {
            return (short)Random.Next(minimum, maximum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static ushort UShort(ushort minimum = ushort.MinValue, ushort maximum = ushort.MaxValue)
        {
            return (ushort)Random.Next(minimum, maximum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static sbyte SByte(sbyte minimum = System.SByte.MinValue, sbyte maximum = sbyte.MaxValue)
        {
            return (sbyte)Random.Next(minimum, maximum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum">Inclusive minimum</param>
        /// <param name="maximum">Exclusive maximum</param>
        /// <returns></returns>
        public static byte Byte(byte minimum = byte.MinValue, byte maximum = byte.MaxValue)
        {
            return (byte)Random.Next(minimum, maximum);
        }
    }
}