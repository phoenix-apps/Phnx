using System;
using System.IO;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// A helper implementation of <see cref="ISymmetricEncryption"/> for using <see cref="T:byte[]"/> instead of <see cref="Stream"/> for output, and other overloads
    /// </summary>
    public abstract class SymmetricEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// Generate a random secure IV of a length that is best suited for this algorithm
        /// </summary>
        /// <returns>A random secure IV</returns>
        public abstract byte[] CreateRandomIv();

        /// <summary>
        /// Generate a random secure key of a length that is best suited for this algorithm
        /// </summary>
        /// <returns>A random secure key</returns>
        public abstract byte[] CreateRandomKey();

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <param name="output">Where to output the decrypted bytes</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        public abstract void Decrypt(Stream input, byte[] key, byte[] iv, Stream output);

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
        public abstract void Encrypt(Stream input, byte[] key, byte[] iv, Stream output);

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted, with the size of the IV stored in the first 4 bytes, then the IV, then the encrypted data</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> is <see langword="null"/></exception>
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            byte[] iv = CreateRandomIv();

            var output = new MemoryStream(4 + iv.Length);
            this.WriteIvHeader(iv, output);

            var input = new MemoryStream(data);
            Encrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Decrypt data using a key. The IV's size must be stored in the first 4 bytes, followed by the randomly generated IV, and then the encrypted data, which is done automatically for you by <see cref="Encrypt(byte[], byte[])"/>
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <returns><paramref name="encryptedData"/> decrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="key"/> is <see langword="null"/></exception>
        public byte[] Decrypt(byte[] encryptedData, byte[] key)
        {
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            // Extract IV
            var input = new MemoryStream(encryptedData);

            var iv = this.ReadIvHeader(input);

            var output = new MemoryStream();
            Decrypt(input, key, iv, output);

            return output.ToArray();
        }
    }
}
