using System;
using System.IO;
using System.Security.Cryptography;

namespace Phnx.Security
{
    /// <summary>
    /// An AES symmetric encryption algorithm. Suitable for password protecting documents, but not suited for storing passwords or sending data to 3rd parties. Consider using <see cref="Pbkdf2Hash"/> for storing passwords, or <see cref="RsaEncryption"/> for sending data to 3rd parties. If you're using AES with passwords, you'll need to hash the passwords to the required length (see <see cref="KeyBitSize"/>). Consider using <see cref="Sha256Hash"/> for this
    /// </summary>
    public class AesEncryption : ISymmetricEncryption
    {
        /// <summary>
        /// The block size used by the internal AES algorithm. Commonly referred to in AES as "Initialization Vector" or "IV"
        /// </summary>
        public const int BlockBitSize = 128;

        /// <summary>
        /// The key used to encrypt and decrypt the data
        /// </summary>
        public const int KeyBitSize = 256;

        /// <summary>
        /// Generate a random secure key of length <see cref="KeyBitSize"/>
        /// </summary>
        /// <returns></returns>
        public byte[] CreateRandomKey()
        {
            return SecureRandomBytes.Generate(KeyBitSize / 8);
        }

        /// <summary>
        /// Encrypt data using a key. The randomly generated IV is stored at the beginning of the returned data ready to be used when you decrypt the data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use when encrypting the data</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            //User Error Checks
            if (key == null || key.Length != KeyBitSize / 8)
                throw new ArgumentException(String.Format("Key needs to be {0} bit!", KeyBitSize), "cryptKey");

            if (data == null || data.Length < 1)
                throw new ArgumentException("Secret Message Required!", "secretMessage");

            byte[] cipherText;
            byte[] iv;

            using (var aes = new AesManaged
            {
                KeySize = KeyBitSize,
                BlockSize = BlockBitSize,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {

                //Use random IV
                aes.GenerateIV();
                iv = aes.IV;

                using (var encrypter = aes.CreateEncryptor(key, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        //Encrypt Data
                        binaryWriter.Write(data);
                    }

                    cipherText = cipherStream.ToArray();
                }

            }

            //Assemble encrypted message and add authentication
            using (var encryptedStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(encryptedStream))
                {
                    //Prepend IV
                    binaryWriter.Write(iv);
                    //Write Ciphertext
                    binaryWriter.Write(cipherText);
                    binaryWriter.Flush();
                }
                return encryptedStream.ToArray();
            }
        }

        /// <summary>
        /// Decrypt data using a key. The randomly generated IV must be stored at the beginning of the data, which is done automatically for you by <see cref="Encrypt"/>
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="key">The key to use when decrypting the data</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] encryptedData, byte[] key)
        {
            //Basic Usage Error Checks
            if (key == null || key.Length != KeyBitSize / 8)
                throw new ArgumentException(String.Format("CryptKey needs to be {0} bit!", KeyBitSize), "cryptKey");

            if (encryptedData == null || encryptedData.Length == 0)
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");

            using (var aes = new AesManaged
            {
                KeySize = KeyBitSize,
                BlockSize = BlockBitSize,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {

                //Grab IV from message
                var iv = new byte[BlockBitSize / 8];
                Array.Copy(encryptedData, 0, iv, 0, iv.Length);

                using (var decrypter = aes.CreateDecryptor(key, iv))
                using (var plainTextStream = new MemoryStream())
                {
                    using (var decrypterStream = new CryptoStream(plainTextStream, decrypter, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(decrypterStream))
                    {
                        //Decrypt Cipher Text from Message
                        binaryWriter.Write(
                            encryptedData,
                            iv.Length,
                            encryptedData.Length - iv.Length
                        );
                    }

                    //Return Plain Text
                    return plainTextStream.ToArray();
                }
            }
        }
    }
}
