using System.Collections.Generic;
using MarkSFrancis.Random.Generator;

namespace MarkSFrancis.Random
{
    /// <summary>
    /// Contains helpers for generating random things (such as data)
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// The static <see cref="System.Random"/> object. As this is created when the app opens, <see cref="Random"/> is better to use than recreating a new <see cref="System.Random"/> object each time you generate a value
        /// </summary>
        public static System.Random Random { get; } = new System.Random();

        #region Generator Helpers

        public static sbyte GetSByte(sbyte inclusiveMinValue = sbyte.MinValue, sbyte inclusiveMaxValue = sbyte.MaxValue)
        {
            return new RandomSByteGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static byte GetByte(byte inclusiveMinValue = byte.MinValue, byte inclusiveMaxValue = byte.MaxValue)
        {
            return new RandomByteGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static short GetShort(short inclusiveMinValue = short.MinValue, short inclusiveMaxValue = short.MaxValue)
        {
            return new RandomShortGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static ushort GetUShort(ushort inclusiveMinValue = ushort.MinValue, ushort inclusiveMaxValue = ushort.MaxValue)
        {
            return new RandomUShortGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static int GetInt(int inclusiveMinValue = int.MinValue, int inclusiveMaxValue = int.MaxValue)
        {
            return new RandomIntGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static uint GetUInt(uint inclusiveMinValue = uint.MinValue, uint inclusiveMaxValue = uint.MaxValue)
        {
            return new RandomUIntGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static long GetLong(long inclusiveMinValue = long.MinValue, long inclusiveMaxValue = long.MaxValue)
        {
            return new RandomLongGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static ulong GetULong(ulong inclusiveMinValue = ulong.MinValue, ulong inclusiveMaxValue = ulong.MaxValue)
        {
            return new RandomULongGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        public static bool GetBool()
        {
            return new RandomBoolGenerator().Get();
        }

        public static T OneOf<T>(params T[] values)
        {
            return values[Random.Next(values.Length)];
        }

        public static T OneOf<T>(IList<T> values)
        {
            return values[Random.Next(values.Count)];
        }

        #endregion
    }
}