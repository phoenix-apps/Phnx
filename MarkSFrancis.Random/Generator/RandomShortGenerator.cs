using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomShortGenerator : IRandomNumberGenerator<short>
    {
        short IRandomGenerator<short>.Get()
        {
            return Get();
        }

        public short Get(short inclusiveMinValue = short.MinValue, short inclusiveMaxValue = short.MaxValue)
        {
            byte[] buffer = new byte[2];
            RandomHelper.Random.NextBytes(buffer);

            var randomGenerated = BitConverter.ToInt16(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a short
                return randomGenerated;
            }

            return (short)((randomGenerated % range) + inclusiveMinValue);
        }
    }
}
