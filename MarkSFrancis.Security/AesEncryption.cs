using System;
using System.IO;
using System.Security.Cryptography;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security
{
    public class AesEncryption : ISymmetricEncryption
    {
        public const int BlockBitSize = 128;
        public const int KeyBitSize = 256;

        public byte[] CreateRandomKey()
        {
            return SecureRandomBytes.Generate(KeyBitSize / 8);
        }

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
