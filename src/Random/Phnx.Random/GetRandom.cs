using Phnx.Random.Generator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Random
{
    /// <summary>
    /// Contains helpers for generating random things (such as data)
    /// </summary>
    public static class GetRandom
    {
        /// <summary>
        /// The static <see cref="System.Random"/> object. As this is created when the app opens, <see cref="StaticRandom"/> is better to use than recreating a new <see cref="System.Random"/> object each time you generate a value
        /// </summary>
        public static System.Random StaticRandom { get; } = new System.Random();

        #region Generator Helpers

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long
        /// </summary>
        /// <returns>A random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long</returns>
        public static string AlphanumericText()
        {
            return RandomAlphaNumericTextGenerator.Get();
        }

        /// <summary>
        /// Get a random upper or lower case letter of the alphabet
        /// </summary>
        /// <returns>A random upper or lower case letter of the alphabet</returns>
        public static char Letter()
        {
            return RandomLetterGenerator.Get();
        }

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters with a fixed length
        /// </summary>
        /// <param name="length">The length of the random <see cref="string"/> to generate</param>
        /// <returns>A random <see cref="string"/> of alphanumeric characters with a fixed length</returns>
        public static string AlphanumericText(int length)
        {
            return RandomAlphaNumericTextGenerator.Get(length);
        }

        /// <summary>
        /// Get a new random instance of <see cref="sbyte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="sbyte"/> with the specified range</returns>
        public static sbyte SByte(sbyte inclusiveMinValue = sbyte.MinValue, sbyte inclusiveMaxValue = sbyte.MaxValue)
        {
            return RandomSByteGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="byte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="byte"/> with the specified range</returns>
        public static byte Byte(byte inclusiveMinValue = byte.MinValue, byte inclusiveMaxValue = byte.MaxValue)
        {
            return RandomByteGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="short"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="short"/> with the specified range</returns>
        public static short Short(short inclusiveMinValue = short.MinValue, short inclusiveMaxValue = short.MaxValue)
        {
            return RandomShortGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="ushort"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ushort"/> with the specified range</returns>
        public static ushort UShort(ushort inclusiveMinValue = ushort.MinValue, ushort inclusiveMaxValue = ushort.MaxValue)
        {
            return RandomUShortGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="int"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="int"/> with the specified range</returns>
        public static int Int(int inclusiveMinValue = int.MinValue, int inclusiveMaxValue = int.MaxValue)
        {
            return RandomIntGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="uint"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="uint"/> with the specified range</returns>
        public static uint UInt(uint inclusiveMinValue = uint.MinValue, uint inclusiveMaxValue = uint.MaxValue)
        {
            return RandomUIntGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="long"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="long"/> with the specified range</returns>
        public static long Long(long inclusiveMinValue = long.MinValue, long inclusiveMaxValue = long.MaxValue)
        {
            return RandomLongGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="ulong"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ulong"/> with the specified range</returns>
        public static ulong ULong(ulong inclusiveMinValue = ulong.MinValue, ulong inclusiveMaxValue = ulong.MaxValue)
        {
            return RandomULongGenerator.Get(inclusiveMinValue, inclusiveMaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="bool"/>
        /// </summary>
        /// <returns>A random instance of <see cref="bool"/></returns>
        public static bool Bool()
        {
            return RandomBoolGenerator.Get();
        }

        /// <summary>
        /// Get a random <see cref="System.DateTime"/>
        /// </summary>
        /// <returns>A random <see cref="System.DateTime"/></returns>
        public static DateTime DateTime()
        {
            return RandomDateTimeGenerator.Get();
        }

        /// <summary>
        /// Get a new random instance of <see cref="System.DateTime"/> within a limited range
        /// </summary>
        /// <param name="from">The inclusive minimum value to generate</param>
        /// <param name="to">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="System.DateTime"/> with the specified range</returns>
        public static DateTime DateTime(DateTime from, DateTime to)
        {
            return RandomDateTimeGenerator.Get(from, to);
        }

        /// <summary>
        /// Get a random instance of a generic type by creating an instance of itself if it's a CLR property, or a random instance of all properties if it's a complex type
        /// </summary>
        /// <typeparam name="T">The type of object to create</typeparam>
        /// <param name="shallow">Whether to create a deep instance of a random, where all complex child properties are also randomized (non-recusively), or only shallow, where complex child properties are left as their defaults</param>
        /// <returns>A random instance of <typeparamref name="T"/></returns>
        public static T Poco<T>(bool shallow)
        {
            return RandomPocoGenerator.Get<T>(shallow);
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
            var index = StaticRandom.Next(2 + additionalValues.Length);

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
            return values[StaticRandom.Next(values.Count)];
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
            var index = StaticRandom.Next(totalValuesCount);
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
                throw new ArgumentNullException(nameof(values));
            }

            var totalValues = values.Sum(v => v.Count);

            if (totalValues == 0)
            {
                string err = ErrorMessage.Factory.SequenceEmpty(nameof(values));
                throw new ArgumentException(err, nameof(values));
            }

            var indexToGet = StaticRandom.Next(0, totalValues);

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
