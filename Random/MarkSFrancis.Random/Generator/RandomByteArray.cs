using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="T:byte[]"/>
    /// </summary>
    public class RandomByteArray : IRandomGenerator<byte[]>
    {
        private const int _defaultLength = 4;

        /// <summary>
        /// Get a random <see cref="T:byte[]"/> 4 bytes long
        /// </summary>
        /// <returns>A random <see cref="T:byte[]"/></returns>
        byte[] IRandomGenerator<byte[]>.Get()
        {
            return Get();
        }
        
        /// <summary>
        /// Get a random <see cref="T:byte[]"/> with a specified length
        /// </summary>
        /// <param name="length">The size of the array of <see cref="T:byte"/> to generate</param>
        /// <returns>A random <see cref="T:byte[]"/></returns>
        public byte[] Get(int length = _defaultLength)
        {
            byte[] randomBytes = new byte[length];

            RandomHelper.Random.NextBytes(randomBytes);

            return randomBytes;
        }
    }
}
