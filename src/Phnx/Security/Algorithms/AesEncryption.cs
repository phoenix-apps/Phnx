using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// An AES symmetric encryption algorithm. Suitable for 2-way encryption, such as protecting documents, but it is not suited for storing passwords or sending data to 3rd parties. Consider using <see cref="Pbkdf2Hash"/> for storing passwords, or <see cref="RsaEncryption"/> for sending data to 3rd parties
    /// </summary>
    /// <remarks>
    /// If you're using AES with user entered passwords, you'll need to hash the passwords to a valid key length (see <see cref="KeyBits"/>). Use a password hashing library, such as aspnetcore's PasswordHasher https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.passwordhasher-1 </remarks>
    public class AesEncryption : SymmetricEncryption
    {
        /// <summary>
        /// The default initialization vector size in bits
        /// </summary>
        public const int IvBits = 128;

        /// <summary>
        /// The default key size in bits to encrypt and decrypt the data, used by <see cref="CreateRandomKey()"/>
        /// </summary>
        public const int KeyBits = 256;

        /// <summary>
        /// Generate a random secure key of length <see cref="KeyBits"/> / 8
        /// </summary>
        /// <returns>A random secure key of length <see cref="KeyBits"/> / 8</returns>
        public override byte[] CreateRandomKey()
        {
            return SecureRandomBytes.Generate(KeyBits / 8);
        }

        /// <summary>
        /// Generate a random secury IV of length <see cref="IvBits"/> / 8
        /// </summary>
        /// <returns>A random secury IV of length <see cref="IvBits"/> / 8</returns>
        public override byte[] CreateRandomIv()
        {
            return SecureRandomBytes.Generate(IvBits / 8);
        }

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
        public override void Encrypt(Stream input, byte[] key, byte[] iv, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (!input.CanRead)
            {
                throw new ArgumentException($"{nameof(input)} stream does not support read", nameof(input));
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (!output.CanWrite)
            {
                throw new ArgumentException($"{nameof(output)} stream does not support write", nameof(output));
            }

            using var aes = CreateAes();
            using var encrypter = aes.CreateEncryptor(key, iv);

            DumpCryptoStream(encrypter, input, output);
        }

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="input">The stream which contains the data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <param name="iv">The initialisation vector to use when decrypting the data</param>
        /// <param name="output">Where to output the decrypted bytes</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="key"/> or <paramref name="iv"/> or <paramref name="output"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> does not support read or <paramref name="output"/> does not support write</exception>
        public override void Decrypt(Stream input, byte[] key, byte[] iv, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (!input.CanRead)
            {
                throw new ArgumentException($"{nameof(input)} stream does not support read", nameof(input));
            }
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv is null)
            {
                throw new ArgumentNullException(nameof(iv));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (!output.CanWrite)
            {
                throw new ArgumentException($"{nameof(output)} stream does not support write", nameof(output));
            }

            using var aes = CreateAes();
            using var decrypter = aes.CreateDecryptor(key, iv);

            DumpCryptoStream(decrypter, input, output);
        }

        private AesManaged CreateAes()
        {
            return new AesManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };
        }

        private void DumpCryptoStream(ICryptoTransform crypto, Stream source, Stream output)
        {
            Debug.Assert(crypto != null, $"{nameof(crypto)} cannot be null");
            Debug.Assert(source != null, $"{nameof(source)} cannot be null");
            Debug.Assert(output != null, $"{nameof(output)} cannot be null");

            byte[] buffer = new byte[Math.Min(1024, source.Length + 1)];
            int bytesRead = buffer.Length;

            var cryptoStream = new CryptoStream(output, crypto, CryptoStreamMode.Write);

            // Not at end of stream yet
            while (bytesRead == buffer.Length)
            {
                bytesRead = source.Read(buffer, 0, buffer.Length);

                cryptoStream.Write(
                    buffer,
                    0,
                    bytesRead
                );
            }

            cryptoStream.FlushFinalBlock();
        }
    }
}
