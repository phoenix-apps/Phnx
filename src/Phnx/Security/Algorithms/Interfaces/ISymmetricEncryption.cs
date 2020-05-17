using System;
using System.IO;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// A symmetric encryption algorithm. Suitable for password protecting documents, but not suited for storing passwords or sending data to 3rd parties. Consider using <see cref="IHashWithSalt"/> for storing passwords, or <see cref="IAsymmetricEncryption"/> for sending data to 3rd parties. If you're using user created password keys instead of random keys, you'll need to hash the passwords to the algorithm's required key length first. Consider using <see cref="IHashWithoutSalt"/> for this
    /// </summary>
    public interface ISymmetricEncryption : IEncryption
    {
        /// <summary>
        /// Create a random secure initialisation vector to use for encryption
        /// </summary>
        /// 
        byte[] CreateRandomIv();

        /// <summary>
        /// Create a random secure key that can be used for this algorithm
        /// </summary>
        /// 
        byte[] CreateRandomKey();

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <param name="output">Where to output the encrypted bytes</param>
        /// <returns><paramref name="input"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        void Encrypt(Stream input, byte[] key, byte[] iv, Stream output);

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <param name="output">Where to output the decrypted bytes</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        void Decrypt(Stream input, byte[] key, byte[] iv, Stream output);
    }
}
