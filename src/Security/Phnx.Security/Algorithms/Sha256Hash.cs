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
        /// The number of times the algorithm is ran on data when using <see cref="Hash(byte[])"/>
        /// </summary>
        public int IterationCount { get; }

        /// <summary>
        /// Create a new <see cref="Sha256Hash"/>
        /// </summary>
        /// <param name="iterationCount">The number of times to run the algorithm when hashing data</param>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="iterationCount"/> is less than zero</exception>
        public Sha256Hash(int iterationCount = 1)
        {
            if (iterationCount < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(iterationCount));
            }

            IterationCount = iterationCount;
        }

        /// <summary>
        /// Hash data
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public byte[] Hash(byte[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (IterationCount == 0)
            {
                return data;
            }

            SHA256Managed sha = new SHA256Managed();

            var hashedData = sha.ComputeHash(data);

            for (int iterationsSoFar = 1; iterationsSoFar < IterationCount; ++iterationsSoFar)
            {
                hashedData = sha.ComputeHash(hashedData);
            }

            return hashedData;
        }
    }
}
