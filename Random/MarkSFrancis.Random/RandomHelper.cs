using System;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;
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

        public static DateTime GetDateTime(DateTime from, DateTime to)
        {
            return new RandomDateTimeGenerator().Get(from, to);
        }

        public static T OneOf<T>(T value, T value2, params T[] additionalValues)
        {
            var index = Random.Next(2 + additionalValues.Length);

            switch (index)
            {
                case 0:
                    return value;
                case 1:
                    return value2;
                default:
                    return additionalValues[index - 2];
            }
        }

        public static T OneOf<T>(IList<T> values)
        {
            return values[Random.Next(values.Count)];
        }

        public static T OneOf<T>(IEnumerable<T> values, int totalItemsCount)
        {
            var index = Random.Next(totalItemsCount);
            return values.Skip(index).First();
        }

        public static T OneOf2D<T>(IList<IList<T>> values)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            var totalItems = values.Sum(v => v.Count);

            if (totalItems == 0)
            {
                throw ErrorFactory.Default.CollectionEmpty(nameof(values));
            }

            var indexToGet = Random.Next(0, totalItems);

            foreach (var items in values)
            {
                if (indexToGet < items.Count)
                {
                    return items[indexToGet];
                }

                indexToGet -= items.Count;
            }

            throw new Exception("Program should have never reached this point");
        }

        #endregion
    }
}