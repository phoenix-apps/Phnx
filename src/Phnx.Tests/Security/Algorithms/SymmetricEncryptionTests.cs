using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.IO;
using System.Text;

namespace Phnx.Security.Tests.Algorithms
{
    public class SymmetricEncryptionTests
    {
        public SymmetricEncryption Aes { get; }

        public SymmetricEncryptionTests()
        {
            Aes = new AesEncryption();
        }

        [Test]
        public void Encrypt_WithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(null, new byte[0]));
        }

        [Test]
        public void Encrypt_WithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(new byte[0], null));
        }

        [Test]
        public void Encrypt_WithValidData_CreatesUnreadableBytes()
        {
            byte[] key = Aes.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var result = Aes.Encrypt(plainBytes, key);

            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void EncryptWithIv_WithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(null, new byte[0], new byte[0]));
        }

        [Test]
        public void EncryptWithIv_WithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(new byte[0], null, new byte[0]));
        }

        [Test]
        public void EncryptWithIv_WithNullIv_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(new byte[0], new byte[0], null));
        }

        [Test]
        public void EncryptWithIv_WithValidData_CreatesUnreadableBytes()
        {
            byte[] key = Aes.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            var iv = Aes.CreateRandomIv();

            var result = Aes.Encrypt(plainBytes, key, iv);

            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void EncryptThenDecrypt_WithValidData_RestoresOriginalMessage()
        {
            byte[] key = Aes.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var encrypted = Aes.Encrypt(plainBytes, key);
            var decrypted = Aes.Decrypt(encrypted, key);

            Assert.AreEqual(plainBytes, decrypted);
        }

        [Test]
        public void WriteThenReadHeader_WithValidIv_ReturnsSameHeader()
        {
            var iv = Aes.CreateRandomIv();

            var output = new MemoryStream();
            Aes.WriteIvHeader(iv, output);
            output.Position = 0;
            var restoredIv = Aes.ReadIvHeader(output);

            Assert.AreEqual(iv, restoredIv);
        }
    }
}
