using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    class RandomSByteGenerator : IRandomNumberGenerator<sbyte>
    {
        sbyte IRandomGenerator<sbyte>.Get()
        {
            return Get();
        }

        public sbyte Get(sbyte inclusiveMinValue = sbyte.MinValue, sbyte inclusiveMaxValue = sbyte.MaxValue)
        {
            byte[] buffer = new byte[1];
            RandomHelper.Random.NextBytes(buffer);
            sbyte randomGenerated = unchecked((sbyte)buffer[0]);

            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a int
                return randomGenerated;
            }

            return (sbyte)((randomGenerated % range) + inclusiveMinValue);
        }
    }
}
