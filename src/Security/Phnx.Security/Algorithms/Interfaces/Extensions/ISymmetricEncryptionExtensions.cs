using System;
using System.IO;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// Extensions for <see cref="ISymmetricEncryption"/>
    /// </summary>
    public static class ISymmetricEncryptionExtensions
    {
        /// <summary>
        /// Saves the IV's length and IV
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="iv">The IV to save</param>
        /// <param name="output">The stream to save to</param>
        public static void WriteIvHeader(this ISymmetricEncryption encryption, byte[] iv, Stream output)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var ivLength = BitConverter.GetBytes(iv.Length);
            output.Write(ivLength, 0, ivLength.Length);
            output.Write(iv, 0, iv.Length);
        }

        /// <summary>
        /// Reads the IV's length and IV
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="input">The stream to read from</param>
        /// <returns>The IV read from <paramref name="input"/></returns>
        public static byte[] ReadIvHeader(this ISymmetricEncryption encryption, Stream input)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var ivLengthBytes = new byte[4];
            input.Read(ivLengthBytes, 0, ivLengthBytes.Length);

            var ivLength = BitConverter.ToInt32(ivLengthBytes, 0);
            var iv = new byte[ivLength];

            input.Read(iv, 0, ivLength);

            return iv;
        }

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        public static byte[] Encrypt(this ISymmetricEncryption encryption, byte[] data, byte[] key, byte[] iv)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var input = new MemoryStream(data);
            var output = new MemoryStream();
            encryption.Encrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="sourceBuffer">The data buffer which contains the data to encrypt</param>
        /// <param name="index">The index in <paramref name="sourceBuffer"/> at which the data to encrypt begins</param>
        /// <param name="count">The length of the data in <paramref name="sourceBuffer"/> to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="sourceBuffer"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryption"/> or <paramref name="sourceBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="index"/> or <paramref name="count"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="count"/> and <paramref name="index"/> is greater than the length of <paramref name="sourceBuffer"/></exception>
        public static byte[] Encrypt(this ISymmetricEncryption encryption, byte[] sourceBuffer, int index, int count, byte[] key, byte[] iv)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (sourceBuffer is null)
            {
                throw new ArgumentNullException(nameof(sourceBuffer));
            }
            if (index < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(index));
            }
            if (count < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(count));
            }

            var output = new MemoryStream();
            var input = new MemoryStream(sourceBuffer, index, count);

            encryption.Encrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Decrypt using a specified IV
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="encryptedData">The encrypted data to decrypt</param>
        /// <param name="key">The key to decrypt with</param>
        /// <param name="iv">The initialisation vector to decrypt with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        public static byte[] Decrypt(this ISymmetricEncryption encryption, byte[] encryptedData, byte[] key, byte[] iv)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            var input = new MemoryStream(encryptedData);
            var output = new MemoryStream();

            encryption.Decrypt(input, key, iv, output);

            return output.ToArray();
        }

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="encryption">The encryption to extend</param>
        /// <param name="encryptedDataBuffer">The data buffer which contains the data to decrypt</param>
        /// <param name="index">The index in <paramref name="encryptedDataBuffer"/> at which the data to decrypt begins</param>
        /// <param name="count">The length of the data in <paramref name="encryptedDataBuffer"/> to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedDataBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="index"/> or <paramref name="count"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="count"/> and <paramref name="index"/> is greater than the length of <paramref name="encryptedDataBuffer"/></exception>
        public static byte[] Decrypt(this ISymmetricEncryption encryption, byte[] encryptedDataBuffer, int index, int count, byte[] key, byte[] iv)
        {
            if (encryption is null)
            {
                throw new ArgumentNullException(nameof(encryption));
            }
            if (encryptedDataBuffer is null)
            {
                throw new ArgumentNullException(nameof(encryptedDataBuffer));
            }
            if (index < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(index));
            }
            if (count < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(count));
            }

            var output = new MemoryStream();
            var input = new MemoryStream(encryptedDataBuffer, index, count);

            encryption.Decrypt(input, key, iv, output);

            return output.ToArray();
        }
    }
}
