using System;
using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="ulong"/>
    /// </summary>
    public class RandomULongGenerator : IRandomNumberGenerator<ulong>
    {
        /// <summary>
        /// Get a random <see cref="ulong"/>
        /// </summary>
        /// <returns>A random <see cref="ulong"/></returns>
        ulong IRandomGenerator<ulong>.Get()
        {
            return Get();
        }

        /// <summary>
        /// Get a new random instance of <see cref="ulong"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="ulong"/> with the specified range</returns>
        public ulong Get(ulong inclusiveMinValue = ulong.MinValue, ulong inclusiveMaxValue = ulong.MaxValue)
        {
            byte[] buffer = new byte[8];
            RandomHelper.Random.NextBytes(buffer);

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
