using System;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="int"/>
    /// </summary>
    public static class RandomIntGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="int"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="int"/> with the specified range</returns>
        public static int Get(int inclusiveMinValue = int.MinValue, int inclusiveMaxValue = int.MaxValue)
        {
            byte[] buffer = new byte[4];
            GetRandom.StaticRandom.NextBytes(buffer);

            var randomGenerated = BitConverter.ToInt32(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a int
                return randomGenerated;
            }

            return Math.Abs(randomGenerated % range) + inclusiveMinValue;
        }
    }
}
