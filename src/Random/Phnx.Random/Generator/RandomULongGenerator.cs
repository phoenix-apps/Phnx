using System;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="ulong"/>
    /// </summary>
    public static class RandomULongGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="ulong"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ulong"/> with the specified range</returns>
        public static ulong Get(ulong inclusiveMinValue = ulong.MinValue, ulong inclusiveMaxValue = ulong.MaxValue)
        {
            byte[] buffer = new byte[8];
            GetRandom.StaticRandom.NextBytes(buffer);

            var randomGenerated = BitConverter.ToUInt64(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a ulong
                return randomGenerated;
            }

            return (randomGenerated % range) + inclusiveMinValue;
        }
    }
}
