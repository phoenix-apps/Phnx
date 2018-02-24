using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomUIntGenerator : IRandomNumberGenerator<uint>
    {
        uint IRandomGenerator<uint>.Get()
        {
            return Get();
        }

        public uint Get(uint inclusiveMinValue = uint.MinValue, uint inclusiveMaxValue = uint.MaxValue)
        {
            byte[] buffer = new byte[4];
            RandomHelper.Random.NextBytes(buffer);

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
