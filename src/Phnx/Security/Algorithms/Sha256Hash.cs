using System;
using System.Security.Cryptography;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// A 256 bit SHA2 hashing algorithm. Suitable for checksums, but generally not secure for passwords. Consider using <see cref="Pbkdf2Hash"/> if you're hashing passwords
    /// </summary>
    public class Sha256Hash : IHashWithoutSalt
    {
        /// <summary>
        /// The number of bytes produced when the data is hashed
        /// </summary>
        public const int HashBytesLength = 32;

        /// <summary>
        /// Create a new <see cref="Sha256Hash"/>
        /// </summary>
        public Sha256Hash()
        {
        }

        /// <summary>
        /// Hash data
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="iterations">The number of times to run the algorithm</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="iterations"/> is less than zero</exception>
        public byte[] Hash(byte[] data, int iterations = 1)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (iterations < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(iterations));
            }

            if (iterations == 0)
            {
                return data;
            }

            SHA256Managed sha = new SHA256Managed();

            for (int iterationsSoFar = 0; iterationsSoFar < iterations; ++iterationsSoFar)
            {
                data = sha.ComputeHash(data);
            }

            return data;
        }
    }
}
