using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomULongGenerator : IRandomNumberGenerator<ulong>
    {
        ulong IRandomGenerator<ulong>.Get()
        {
            return Get();
        }

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
