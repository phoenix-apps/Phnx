using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomByteGenerator : IRandomNumberGenerator<byte>
    {
        byte IRandomGenerator<byte>.Get()
        {
            return Get();
        }

        public byte Get(byte inclusiveMinValue = byte.MinValue, byte inclusiveMaxValue = byte.MaxValue)
        {
            byte[] buffer = new byte[1];
            RandomHelper.Random.NextBytes(buffer);
            byte randomGenerated = buffer[0];
            
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a int
                return randomGenerated;
            }

            return (byte)((randomGenerated % range) + inclusiveMinValue);
        }
    }
}
