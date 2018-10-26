using System;
using System.Text;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// Extension for <see cref="IHashWithSalt"/> and <see cref="IHashWithoutSalt"/>
    /// </summary>
    public static class IHashExtensions
    {
        /// <summary>
        /// Hash text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The hashing algorithm to use</param>
        /// <param name="data">The data to hash</param>
        /// <param name="salt">The salt to use. This must have the same length as <see cref="IHashWithSalt.SaltBytesLength"/></param>
        /// <param name="encoding">The text encoding to use</param>
        /// <param name="numberOfIterations">The number of times the algorithm is ran on <paramref name="data"/></param>
        /// <returns><paramref name="data"/> hashed</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="salt"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="numberOfIterations"/> is less than zero</exception>
        public static byte[] Hash(this IHashWithSalt encryption, string data, byte[] salt, Encoding encoding, int numberOfIterations)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes, salt, numberOfIterations);
        }

        /// <summary>
        /// Hash text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The hashing algorithm to use</param>
        /// <param name="data">The data to hash</param>
        /// <param name="encoding">The encoding to convert the text to bytes with</param>
        /// <returns><paramref name="data"/> hashed</returns>
        public static byte[] Hash(this IHashWithoutSalt encryption, string data, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes);
        }
    }
}
