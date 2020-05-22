using System;
using System.Security.Cryptography;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// An RSA asymmetric encryption algorithm
    /// </summary>
    public class RsaEncryption : IAsymmetricEncryption
    {
        /// <summary>
        /// Create random secure keys for use by <see cref="Encrypt"/> and <see cref="Decrypt"/>
        /// </summary>
        /// <param name="keySize">The size of the asymetric key to generate. This does not guarantee the size of <paramref name="publicKey"/> or <paramref name="privateKey"/></param>
        /// <param name="publicKey">The generated public key</param>
        /// <param name="privateKey">The generated private key</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="keySize"/> is less than or equal to zero</exception>
        public void CreateRandomKeys(int keySize, out byte[] publicKey, out byte[] privateKey)
        {
            if (keySize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(keySize), $"{nameof(keySize)} cannot be less than or equal to zero");
            }

            var provider = new RSACryptoServiceProvider(keySize);

            publicKey = provider.ExportCspBlob(false);

            privateKey = provider.ExportCspBlob(true);
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="publicKey">The public key to use when encrypting the data</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="publicKey"/> is <see langword="null"/></exception>
        public byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (publicKey is null)
            {
                throw new ArgumentNullException(nameof(publicKey));
            }

            using var rsaServiceProvider = new RSACryptoServiceProvider(2048);
            rsaServiceProvider.ImportCspBlob(publicKey);

            return rsaServiceProvider.Encrypt(data, true);
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="privateKey">The private key to use when decrypting the data</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="encryptedData"/> or <paramref name="privateKey"/> is <see langword="null"/></exception>
        public byte[] Decrypt(byte[] encryptedData, byte[] privateKey)
        {
            if (encryptedData is null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }
            if (privateKey is null)
            {
                throw new ArgumentNullException(nameof(privateKey));
            }

            using var rsaServiceProvider = new RSACryptoServiceProvider(2048);
            rsaServiceProvider.ImportCspBlob(privateKey);

            return rsaServiceProvider.Decrypt(encryptedData, true);
        }
    }
}
