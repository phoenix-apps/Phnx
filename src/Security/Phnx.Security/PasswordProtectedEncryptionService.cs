using Phnx.Security.Algorithms;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Phnx.Security
{
    /// <summary>
    /// Provides encryption helpers for working with encrypting and decrypting files, using a password hashing algorithm (hash with a salt), and a symmetric encryption algorithm
    /// </summary>
    public class PasswordProtectedEncryptionService
    {
        /// <summary>
        /// Create a new <see cref="PasswordProtectedEncryptionService"/> using defined algorithms
        /// </summary>
        /// <param name="encryptionAlgorithm">The algorithm to use when encrypting or decrypting the raw data of the file</param>
        /// <param name="passwordHash">The algorithm to use when hashing the password ready for the key</param>
        public PasswordProtectedEncryptionService(ISymmetricEncryption encryptionAlgorithm, IHashWithoutSalt passwordHash)
        {
            EncryptionAlgorithm = encryptionAlgorithm ?? throw new ArgumentNullException(nameof(encryptionAlgorithm));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

        /// <summary>
        /// The algorithm used when encrypting or decrypting the raw data of files
        /// </summary>
        public ISymmetricEncryption EncryptionAlgorithm { get; }

        /// <summary>
        /// The algorithm used when hashing passwords. This is used to ensure that the password is always the right length for <see cref="ISymmetricEncryption"/>
        /// </summary>
        public IHashWithoutSalt PasswordHash { get; }

        /// <summary>
        /// Encrypt a source stream to an output stream, including the header metadata needed to decrypt the output (if the same password is provided to decrypt)
        /// </summary>
        /// <param name="input">The plain, unencrypted data source</param>
        /// <param name="password">The password to use to encrypt the data</param>
        /// <param name="output">The stream to output all metadata and encrypted data to</param>
        public void Encrypt(Stream input, string password, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var hash = HashPassword(password);

            var iv = EncryptionAlgorithm.CreateRandomIv();
            EncryptionAlgorithm.WriteIvHeader(iv, output);

            EncryptionAlgorithm.Encrypt(input, hash, iv, output);
        }

        /// <summary>
        /// Attempts to decrypt an encrypted source using a password. If the data was successfully decrypted, this returns <see langword="true"/>, if the password is incorrect or the file is corrupt, it returns <see langword="false"/>
        /// </summary>
        /// <param name="input">The encrypted data source, which includes all header metadata needed to decrypt the file</param>
        /// <param name="password">The password to the encryption to use</param>
        /// <param name="output">The stream to output plain, unencrypted data to. If the password is incorrect, corrupt data may have been written to this stream</param>
        /// <returns><see langword="true"/> if the data could be decrypted using the password, otherwise <see langword="false"/></returns>
        public bool TryDecrypt(Stream input, string password, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            // Load hash
            var hash = HashPassword(password);

            try
            {
                // Load IV
                var iv = EncryptionAlgorithm.ReadIvHeader(input);

                // Decrypt
                EncryptionAlgorithm.Decrypt(input, hash, iv, output);
            }
            catch (CryptographicException)
            {
                return false;
            }

            return true;
        }

        private byte[] HashPassword(string password)
        {
            return PasswordHash.Hash(Encoding.Unicode.GetBytes(password));
        }
    }
}
