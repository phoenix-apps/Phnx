using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomIntGenerator : IRandomNumberGenerator<int>
    {
        int IRandomGenerator<int>.Get()
        {
            return Get();
        }

        public int Get(int inclusiveMinValue = int.MinValue, int inclusiveMaxValue = int.MaxValue)
        {
            byte[] buffer = new byte[4];
            RandomHelper.Random.NextBytes(buffer);

            var randomGenerated = BitConverter.ToInt32(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a int
                return randomGenerated;
            }

            return (randomGenerated % range) + inclusiveMinValue;
        }
    }
}
