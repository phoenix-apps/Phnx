using System;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="uint"/>
    /// </summary>
    public static class RandomUIntGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="uint"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="uint"/> with the specified range</returns>
        public static uint Get(uint inclusiveMinValue = uint.MinValue, uint inclusiveMaxValue = uint.MaxValue)
        {
            byte[] buffer = new byte[4];
            GetRandom.StaticRandom.NextBytes(buffer);

            var randomGenerated = BitConverter.ToUInt32(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a uint
                return randomGenerated;
            }

            return (randomGenerated % range) + inclusiveMinValue;
        }
    }
}
