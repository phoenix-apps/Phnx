using System;
using System.Security.Cryptography;

namespace Phnx.Security
{
    /// <summary>
    /// Provides a way to generate secure random bytes
    /// </summary>
    public static class SecureRandomBytes
    {
        private static readonly Lazy<RandomNumberGenerator> _rng = new Lazy<RandomNumberGenerator>(() => RandomNumberGenerator.Create());

        /// <summary>
        /// Generate a number of secure random bytes
        /// </summary>
        /// <param name="numberOfBytesToGenerate">The number of bytes to generate</param>
        /// <returns></returns>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="numberOfBytesToGenerate"/> is less than zero</exception>
        public static byte[] Generate(int numberOfBytesToGenerate)
        {
            if (numberOfBytesToGenerate < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(numberOfBytesToGenerate));
            }

            byte[] rndBytes = new byte[numberOfBytesToGenerate];
            _rng.Value.GetBytes(rndBytes);

            return rndBytes;
        }
    }
}
