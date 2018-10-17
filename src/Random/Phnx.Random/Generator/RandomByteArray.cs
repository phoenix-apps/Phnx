namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="T:byte[]"/>
    /// </summary>
    public static class RandomByteArray
    {
        private const int _defaultLength = 4;

        /// <summary>
        /// Get a random <see cref="T:byte[]"/> with a specified length
        /// </summary>
        /// <param name="length">The size of the array of <see cref="T:byte"/> to generate</param>
        /// <returns>A random <see cref="T:byte[]"/></returns>
        public static byte[] Get(int length = _defaultLength)
        {
            byte[] randomBytes = new byte[length];

            GetRandom.StaticRandom.NextBytes(randomBytes);

            return randomBytes;
        }
    }
}
