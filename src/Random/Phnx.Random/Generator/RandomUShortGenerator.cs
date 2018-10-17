using System;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="ushort"/>
    /// </summary>
    public class RandomUShortGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="ushort"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ushort"/> with the specified range</returns>
        public static ushort Get(ushort inclusiveMinValue = ushort.MinValue, ushort inclusiveMaxValue = ushort.MaxValue)
        {
            byte[] buffer = new byte[2];
            GetRandom.StaticRandom.NextBytes(buffer);

            var randomGenerated = BitConverter.ToUInt16(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a ushort
                return randomGenerated;
            }

            return (ushort)((randomGenerated % range) + inclusiveMinValue);
        }
    }
}
