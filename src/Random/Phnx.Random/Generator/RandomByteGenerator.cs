namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="byte"/>
    /// </summary>
    public class RandomByteGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="byte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="byte"/> with the specified range</returns>
        public static byte Get(byte inclusiveMinValue = byte.MinValue, byte inclusiveMaxValue = byte.MaxValue)
        {
            byte[] buffer = new byte[1];
            GetRandom.StaticRandom.NextBytes(buffer);
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
