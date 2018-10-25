using Phnx.Collections;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Phnx.Security
{
    /// <summary>
    /// An AES symmetric encryption algorithm. Suitable for password protecting documents, but not suited for storing passwords or sending data to 3rd parties. Consider using <see cref="Pbkdf2Hash"/> for storing passwords, or <see cref="RsaEncryption"/> for sending data to 3rd parties. If you're using AES with user entered passwords, you'll need to hash the passwords to a valid key length (see <see cref="KeyBits"/>). Consider using an algorithm <see cref="Sha256Hash"/> for getting the password to a key of a valid size
    /// </summary>
    public class AesEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// The default initialization vector size in bits
        /// </summary>
        public const int InitialisationVectorBits = 128;

        /// <summary>
        /// The default key size in bits to encrypt and decrypt the data, used by <see cref="CreateRandomKey()"/>
        /// </summary>
        public const int KeyBits = 256;

        /// <summary>
        /// Generate a random secure key of length <see cref="KeyBits"/> / 8
        /// </summary>
        /// <returns>A random secure key of length <see cref="KeyBits"/> / 8</returns>
        public byte[] CreateRandomKey()
        {
            return CreateRandomKey(KeyBits / 8);
        }

        /// <summary>
        /// Generate a random secure key of length <paramref name="keySizeBytes"/> in bytes
        /// </summary>
        /// <param name="keySizeBytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="keySizeBytes"/> is less than zero</exception>
        public byte[] CreateRandomKey(int keySizeBytes)
        {
            if (keySizeBytes < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(keySizeBytes));
            }

            return SecureRandomBytes.Generate(keySizeBytes);
        }

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> is not equal in size to <see cref="KeyBits"/> / 8</exception>
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            byte[] iv = SecureRandomBytes.Generate(InitialisationVectorBits / 8);

            var encrypted = Encrypt(data, key, iv);

            byte[] result = iv.Join(encrypted);
            return result;
        }

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="data"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> is not equal in size to <see cref="KeyBits"/> / 8</exception>
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            return Encrypt(data, 0, data.Length, key, iv);
        }

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="dataBuffer">The data buffer which contains the data to encrypt</param>
        /// <param name="dataStartIndex">The index in <paramref name="dataBuffer"/> at which the data to encrypt begins</param>
        /// <param name="dataLength">The length of the data in <paramref name="dataBuffer"/> to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <param name="iv">The initialisation vector to use when encrypting the data</param>
        /// <returns><paramref name="dataBuffer"/> encrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dataBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="dataStartIndex"/> or <paramref name="dataLength"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="dataLength"/> and <paramref name="dataStartIndex"/> is greater than the length of <paramref name="dataBuffer"/></exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> is not equal in size to <see cref="KeyBits"/> / 8</exception>
        public byte[] Encrypt(byte[] dataBuffer, int dataStartIndex, int dataLength, byte[] key, byte[] iv)
        {
            if (dataBuffer is null)
            {
                throw new ArgumentNullException(nameof(dataBuffer));
            }
            if (dataStartIndex < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(dataStartIndex));
            }
            if (dataLength < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(dataLength));
            }
            if (dataStartIndex + dataLength > dataBuffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dataLength), $"The sum of {dataLength} and {dataStartIndex} cannot be greater than {nameof(dataBuffer)}.{nameof(dataBuffer.Length)}");
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using (var aes = CreateAes())
            {
                using (var encrypter = aes.CreateEncryptor(key, iv))
                {
                    return DumpCryptoStream(encrypter, dataBuffer, dataStartIndex, dataLength);
                }
            }
        }

        /// <summary>
        /// Decrypt data using a key. The randomly generated IV must be stored at the beginning of the data, which is done automatically for you by <see cref="Encrypt(byte[], byte[])"/>
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
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Extract IV
            var iv = new byte[InitialisationVectorBits / 8];
            Array.Copy(encryptedData, 0, iv, 0, iv.Length);

            return Decrypt(encryptedData, iv.Length, encryptedData.Length - iv.Length, key, iv);
        }

        /// <summary>
        /// Decrypt using a specified IV
        /// </summary>
        /// <param name="encryptedData">The encrypted data to decrypt</param>
        /// <param name="key">The key to decrypt with</param>
        /// <param name="iv">The initialisation vector to decrypt with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        public byte[] Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
        {
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Decrypt(encryptedData, 0, encryptedData.Length, key, iv);
        }

        /// <summary>
        /// Decrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="encryptedDataBuffer">The data buffer which contains the data to decrypt</param>
        /// <param name="dataStartIndex">The index in <paramref name="encryptedDataBuffer"/> at which the data to decrypt begins</param>
        /// <param name="dataLength">The length of the data in <paramref name="encryptedDataBuffer"/> to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <returns><paramref name="encryptedDataBuffer"/> decrypted</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encryptedDataBuffer"/> or <paramref name="key"/> or <paramref name="iv"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="dataStartIndex"/> or <paramref name="dataLength"/> is less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The sum of <paramref name="dataLength"/> and <paramref name="dataStartIndex"/> is greater than the length of <paramref name="encryptedDataBuffer"/></exception>
        public byte[] Decrypt(byte[] encryptedDataBuffer, int dataStartIndex, int dataLength, byte[] key, byte[] iv)
        {
            if (encryptedDataBuffer is null)
            {
                throw new ArgumentNullException(nameof(encryptedDataBuffer));
            }
            if (dataStartIndex < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(dataStartIndex));
            }
            if (dataLength < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(dataLength));
            }
            if (dataStartIndex + dataLength > encryptedDataBuffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dataLength), $"The sum of {dataLength} and {dataStartIndex} cannot be greater than {nameof(encryptedDataBuffer)}.{nameof(encryptedDataBuffer.Length)}");
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using (var aes = CreateAes())
            {
                using (var decrypter = aes.CreateDecryptor(key, iv))
                {
                    return DumpCryptoStream(decrypter, encryptedDataBuffer, dataStartIndex, dataLength);
                }
            }
        }

        private AesManaged CreateAes()
        {
            return new AesManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
        }

        private byte[] DumpCryptoStream(ICryptoTransform crypto, byte[] destination, int destinationStartIndex, int bytesToCopy)
        {
            var output = new MemoryStream();
            using (var cryptoStream = new CryptoStream(output, crypto, CryptoStreamMode.Write))
            {
                cryptoStream.Write(
                    destination,
                    destinationStartIndex,
                    bytesToCopy
                );
            }

            return output.ToArray();
        }
    }
}
