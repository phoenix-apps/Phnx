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
        private const int _nonceBytes = 12;
        private const int _tagBytes = 16;
        private const int _chunkSize = 1024 * 1024;

        /// <summary>
        /// The default initialization vector size in bits
        /// </summary>
        public const int IvBits = _nonceBytes * 8;

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
            return SecureRandomBytes.Generate(_nonceBytes);
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

            using var aes = CreateAes(key);

            bool reachedEnd;
            do
            {
                byte[] plaintext;
                (reachedEnd, plaintext) = ReadChunk(input);

                if (plaintext.Length == 0)
                {
                    break;
                }

                byte[] ciphertext = new byte[plaintext.Length];
                byte[] tag = new byte[_tagBytes];

                aes.Encrypt(iv, plaintext, ciphertext, tag);

                WriteEncyptedChunk(tag, ciphertext, output);
            } while (!reachedEnd);
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

            using var aes = CreateAes(key);

            bool reachedEnd;
            do
            {
                byte[] tag, ciphertext;
                (reachedEnd, tag, ciphertext) = ReadEncryptedChunk(input);

                if (ciphertext.Length == 0)
                {
                    break;
                }

                var plaintext = new byte[ciphertext.Length];
                aes.Decrypt(iv, ciphertext, tag, plaintext);

                output.Write(plaintext);
            } while (!reachedEnd);
        }

        private AesGcm CreateAes(byte[] key)
        {
            return new AesGcm(key);
        }

        private (bool reachedEnd, byte[] chunk) ReadChunk(Stream input)
        {
            var buffer = new byte[_chunkSize - _tagBytes];
            int totalRead = input.Read(buffer);

            var chunk = buffer.AsSpan(0, totalRead).ToArray();

            return (totalRead != _chunkSize - _tagBytes, chunk);
        }

        private (bool reachedEnd, byte[] tag, byte[] ciphertext) ReadEncryptedChunk(Stream input)
        {
            byte[] tag = new byte[_tagBytes];

            var tagSize = input.Read(tag);
            if (tagSize == 0)
            {
                return (true, Array.Empty<byte>(), Array.Empty<byte>());
            }
            if (tagSize != tag.Length)
            {
                throw new CryptographicException("Data has an invalid cryptographic tag. Please ensure that the data was encrypted by this AES implementation, and not a different one");
            }

            Debug.Assert(_chunkSize > _tagBytes);
            byte[] ciphertext = new byte[_chunkSize - _tagBytes];

            int ciphertextSize = input.Read(ciphertext);
            bool atEnd = ciphertextSize != ciphertext.Length;
            ciphertext = ciphertext.AsSpan(0, ciphertextSize).ToArray();

            return (atEnd, tag, ciphertext);
        }

        private void WriteEncyptedChunk(byte[] tag, byte[] ciphertext, Stream output)
        {
            output.Write(tag);
            output.Write(ciphertext);
        }
    }
}
