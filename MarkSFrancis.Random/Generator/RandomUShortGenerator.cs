using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomUShortGenerator : IRandomNumberGenerator<ushort>
    {
        ushort IRandomGenerator<ushort>.Get()
        {
            return Get();
        }

        public ushort Get(ushort inclusiveMinValue = ushort.MinValue, ushort inclusiveMaxValue = ushort.MaxValue)
        {
            byte[] buffer = new byte[2];
            RandomHelper.Random.NextBytes(buffer);

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
