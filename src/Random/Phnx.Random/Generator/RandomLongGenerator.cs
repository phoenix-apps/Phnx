using System;
using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="long"/>
    /// </summary>
    public class RandomLongGenerator : IRandomNumberGenerator<long>
    {
        /// <summary>
        /// Get a random <see cref="long"/>
        /// </summary>
        /// <returns>A random <see cref="long"/></returns>
        long IRandomGenerator<long>.Get()
        {
            return Get();
        }

        /// <summary>
        /// Get a new random instance of <see cref="long"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="long"/> with the specified range</returns>
        public long Get(long inclusiveMinValue = long.MinValue, long inclusiveMaxValue = long.MaxValue)
        {
            byte[] buffer = new byte[8];
            RandomHelper.Random.NextBytes(buffer);

            var randomGenerated = BitConverter.ToInt64(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a long
                return randomGenerated;
            }

            return (randomGenerated % range) + inclusiveMinValue;
        }
    }
}
