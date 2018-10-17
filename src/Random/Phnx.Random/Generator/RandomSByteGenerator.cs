namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="sbyte"/>
    /// </summary>
    public static class RandomSByteGenerator
    {
        /// <summary>
        /// Get a new random instance of <see cref="sbyte"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="sbyte"/> with the specified range</returns>
        public static sbyte Get(sbyte inclusiveMinValue = sbyte.MinValue, sbyte inclusiveMaxValue = sbyte.MaxValue)
        {
            byte[] buffer = new byte[1];
            GetRandom.StaticRandom.NextBytes(buffer);
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
