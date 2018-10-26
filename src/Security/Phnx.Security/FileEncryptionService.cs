using Phnx.Security.Algorithms;
using Phnx.Security.Passwords;
using System;
using System.IO;
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
        /// <param name="fileDataEncryptionAlgorithm">The algorithm to use when encrypting or decrypting the raw data of the file</param>
        /// <param name="passwordHash">The algorithm to use when hashing the password ready for the key</param>
        public PasswordProtectedEncryptionService(ISymmetricEncryption fileDataEncryptionAlgorithm, IPasswordHash passwordHash)
        {
            FileDataEncryptionAlgorithm = fileDataEncryptionAlgorithm;
            PasswordHash = passwordHash;
        }

        /// <summary>
        /// The algorithm used when encrypting or decrypting the raw data of files
        /// </summary>
        public ISymmetricEncryption FileDataEncryptionAlgorithm { get; }

        /// <summary>
        /// The algorithm used when hashing passwords
        /// </summary>
        public IPasswordHash PasswordHash { get; }

        /// <summary>
        /// Encrypt a source stream to an output stream, including the header metadata needed to decrypt the output (if the same password is provided to decrypt)
        /// </summary>
        /// <param name="source">The plain, unencrypted data source</param>
        /// <param name="password">The password to use to encrypt the data</param>
        /// <param name="output">The stream to output all metadata and encrypted data to</param>
        public void Encrypt(Stream source, string password, Stream output)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var salt = PasswordHash.GenerateSalt();

            // Save salt
            output.Write(salt, 0, salt.Length);

            var hash = PasswordHash.GenerateHash(Encoding.Unicode.GetBytes(password), salt);

            var iv = FileDataEncryptionAlgorithm.CreateRandomIv();
            FileDataEncryptionAlgorithm.WriteIvHeader(iv, output);

            FileDataEncryptionAlgorithm.Encrypt(source, hash, iv, output);
        }

        /// <summary>
        /// Attempts to decrypt an encrypted source using a password. If the password is correct, this returns <see langword="true"/>, if it is incorrect, it returns <see langword="false"/>
        /// </summary>
        /// <param name="source">The encrypted data source, which includes all header metadata needed to decrypt the file</param>
        /// <param name="password">The password to the encryption to use</param>
        /// <param name="output">The stream to output plain, unencrypted data to. If the password is incorrect, no data is written to this stream</param>
        /// <returns><see langword="true"/> if the password is correct, otherwise <see langword="false"/></returns>
        public bool TryDecrypt(Stream source, string password, Stream output)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
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
            var salt = new byte[PasswordHash.SaltBytesLength];
            source.Read(salt, 0, salt.Length);
            var hash = PasswordHash.GenerateHash(Encoding.Unicode.GetBytes(password), salt);

            // Load IV
            var iv = FileDataEncryptionAlgorithm.ReadIvHeader(source);

            // Decrypt
            FileDataEncryptionAlgorithm.Decrypt(source, hash, iv, output);

            return true;
        }
    }
}
