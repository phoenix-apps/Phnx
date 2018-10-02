using MarkSFrancis;
using MarkSFrancis.Collections.Extensions;
using Phnx.Random.Generator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Random
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

        /// <summary>
        /// Get a new random instance of <see cref="sbyte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="sbyte"/> with the specified range</returns>
        public static sbyte GetSByte(sbyte inclusiveMinValue = sbyte.MinValue, sbyte inclusiveMaxValue = sbyte.MaxValue)
        {
            return new RandomSByteGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="byte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="byte"/> with the specified range</returns>
        public static byte GetByte(byte inclusiveMinValue = byte.MinValue, byte inclusiveMaxValue = byte.MaxValue)
        {
            return new RandomByteGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="short"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="short"/> with the specified range</returns>
        public static short GetShort(short inclusiveMinValue = short.MinValue, short inclusiveMaxValue = short.MaxValue)
        {
            return new RandomShortGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="ushort"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ushort"/> with the specified range</returns>
        public static ushort GetUShort(ushort inclusiveMinValue = ushort.MinValue, ushort inclusiveMaxValue = ushort.MaxValue)
        {
            return new RandomUShortGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="int"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="int"/> with the specified range</returns>
        public static int GetInt(int inclusiveMinValue = int.MinValue, int inclusiveMaxValue = int.MaxValue)
        {
            return new RandomIntGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="uint"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="uint"/> with the specified range</returns>
        public static uint GetUInt(uint inclusiveMinValue = uint.MinValue, uint inclusiveMaxValue = uint.MaxValue)
        {
            return new RandomUIntGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="long"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="long"/> with the specified range</returns>
        public static long GetLong(long inclusiveMinValue = long.MinValue, long inclusiveMaxValue = long.MaxValue)
        {
            return new RandomLongGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="ulong"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ulong"/> with the specified range</returns>
        public static ulong GetULong(ulong inclusiveMinValue = ulong.MinValue, ulong inclusiveMaxValue = ulong.MaxValue)
        {
            return new RandomULongGenerator().Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="bool"/>
        /// </summary>
        /// <returns>A random instance of <see cref="bool"/></returns>
        public static bool GetBool()
        {
            return new RandomBoolGenerator().Get();
        }

        /// <summary>
        /// Get a new random instance of <see cref="DateTime"/> within a limited range
        /// </summary>
        /// <param name="from">The inclusive minimum value to generate</param>
        /// <param name="to">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="DateTime"/> with the specified range</returns>
        public static DateTime GetDateTime(DateTime from, DateTime to)
        {
            return new RandomDateTimeGenerator().Get(from, to);
        }

        #endregion

        /// <summary>
        /// Get a random instance of <typeparamref name="T"/> from a collection
        /// </summary>
        /// <typeparam name="T">The type of item to get a random instance of</typeparam>
        /// <param name="value">The first value in the collection of values to get an instance of</param>
        /// <param name="value2">The second value in the collection of values to get an instance of</param>
        /// <param name="additionalValues">All other values in the collection of values to get an instance of</param>
        /// <returns>A random instance of <typeparamref name="T"/> from a collection</returns>
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

        /// <summary>
        /// Get a random instance of <typeparamref name="T"/> from a collection
        /// </summary>
        /// <typeparam name="T">The type of item to get a random instance of</typeparam>
        /// <param name="values">The collection of values to get random instance from</param>
        /// <returns>A random instance of <typeparamref name="T"/> from a collection</returns>
        public static T OneOf<T>(IList<T> values)
        {
            return values[Random.Next(values.Count)];
        }

        /// <summary>
        /// Get a random instance of <typeparamref name="T"/> from a collection
        /// </summary>
        /// <typeparam name="T">The type of item to get a random instance of</typeparam>
        /// <param name="values">The collection of values to get random instance from</param>
        /// <param name="totalValuesCount">A count of the total number of values in the collection</param>
        /// <returns>A random instance of <typeparamref name="T"/> from a collection</returns>
        public static T OneOf<T>(IEnumerable<T> values, int totalValuesCount)
        {
            var index = Random.Next(totalValuesCount);
            return values.Skip(index).First();
        }

        /// <summary>
        /// Get a random instance of <typeparamref name="T"/> from a two dimensional collection
        /// </summary>
        /// <typeparam name="T">The type of item to get a random instance of</typeparam>
        /// <param name="values">The two dimensional collection of values to get random instance from</param>
        /// <returns>A random instance of <typeparamref name="T"/> from a two dimensional collection</returns>
        public static T OneOf2D<T>(IList<IList<T>> values)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }

            var totalValues = values.Sum(v => v.Count);

            if (totalValues == 0)
            {
                throw ErrorFactory.Default.CollectionEmpty(nameof(values));
            }

            var indexToGet = Random.Next(0, totalValues);

            foreach (var value in values)
            {
                if (indexToGet < value.Count)
                {
                    return value[indexToGet];
                }

                indexToGet -= value.Count;
            }

            throw new Exception("Program should have never reached this point. Method name thrown in: " + nameof(OneOf2D));
        }
    }
}
