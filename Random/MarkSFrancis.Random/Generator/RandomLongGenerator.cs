using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomLongGenerator : IRandomNumberGenerator<long>
    {
        long IRandomGenerator<long>.Get()
        {
            return Get();
        }

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
