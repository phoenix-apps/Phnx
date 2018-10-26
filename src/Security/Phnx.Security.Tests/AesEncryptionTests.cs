using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.Text;

namespace Phnx.Security.Tests
{
    public class AesEncryptionTests
    {
        public AesEncryptionTests()
        {
            AesEncryption = new AesEncryption();
        }

        public AesEncryption AesEncryption { get; }

        [Test]
        public void CreateRandomKey_WithUnspecifiedSize_GetsKeyOfSizeKeySizeDividedBy8()
        {
            var key = AesEncryption.CreateRandomKey();

            Assert.AreEqual(AesEncryption.KeyBits / 8, key.Length);
        }

        [Test]
        public void CreateRandomKey_WithSpecifiedSize_GetsKeyOfSize()
        {
            var size = 17;

            var key = AesEncryption.CreateRandomKey(size);

            Assert.AreEqual(size, key.Length);
        }

        [Test]
        public void CreateRandomKey_WithSizeLessThanZero_ThrowsArgumentLessThanZeroException()
        {
            var size = -1;

            Assert.Throws<ArgumentLessThanZeroException>(() => AesEncryption.CreateRandomKey(size));
        }

        [Test]
        public void CreateRandomIv_WithUnspecifiedSize_GetsKeyOfSizeIVSizeDividedBy8()
        {
            var key = AesEncryption.CreateRandomIv();

            Assert.AreEqual(AesEncryption.IvBits / 8, key.Length);
        }

        [Test]
        public void CreateRandomIv_WithSpecifiedSize_GetsKeyOfSize()
        {
            var size = 17;

            var key = AesEncryption.CreateRandomIv(size);

            Assert.AreEqual(size, key.Length);
        }

        [Test]
        public void CreateRandomIv_WithSizeLessThanZero_ThrowsArgumentLessThanZeroException()
        {
            var size = -1;

            Assert.Throws<ArgumentLessThanZeroException>(() => AesEncryption.CreateRandomIv(size));
        }

        [Test]
        public void Encrypt_WithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(null, new byte[0]));
        }

        [Test]
        public void Encrypt_WithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(new byte[0], null));
        }

        [Test]
        public void Encrypt_WithValidData_CreatesUnreadableBytes()
        {
            // Arrange
            byte[] key = AesEncryption.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Act
            var result = AesEncryption.Encrypt(plainBytes, key);

            // Assert
            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void EncryptWithIv_WithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(null, new byte[0], new byte[0]));
        }

        [Test]
        public void EncryptWithIv_WithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(new byte[0], null, new byte[0]));
        }

        [Test]
        public void EncryptWithIv_WithNullIv_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(new byte[0], new byte[0], null));
        }

        [Test]
        public void EncryptWithIv_WithValidData_CreatesUnreadableBytes()
        {
            byte[] key = AesEncryption.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            var iv = AesEncryption.CreateRandomIv();

            var result = AesEncryption.Encrypt(plainBytes, key, iv);

            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void EncryptThenDecrypt_WithValidData_RestoresOriginalMessage()
        {
            byte[] key = AesEncryption.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var encrypted = AesEncryption.Encrypt(plainBytes, key);
            var decrypted = AesEncryption.Decrypt(encrypted, key);

            Assert.AreEqual(plainBytes, decrypted);
        }
    }
}
