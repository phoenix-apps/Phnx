using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.Text;

namespace Phnx.Security.Tests.Algorithms
{
    class RsaEncryptionTests
    {
        public RsaEncryptionTests()
        {
            RsaEncryption = new RsaEncryption();
        }

        public RsaEncryption RsaEncryption { get; }

        [Test]
        public void CreateRandomKeys_WithNegativeKeySize_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RsaEncryption.CreateRandomKeys(-1, out _, out _));
        }

        [Test]
        public void CreateRandomKeys_WithZeroKeySize_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RsaEncryption.CreateRandomKeys(0, out _, out _));
        }

        [Test]
        public void Encrypt_WithNullData_ThrowsArgumentNullException()
        {
            RsaEncryption.CreateRandomKeys(2048, out var publicKey, out var privateKey);

            Assert.Throws<ArgumentNullException>(() => RsaEncryption.Encrypt(null, publicKey));
        }

        [Test]
        public void Encrypt_WithNullKey_ThrowsArgumentNullException()
        {
            var data = Encoding.UTF8.GetBytes("asdf");

            Assert.Throws<ArgumentNullException>(() => RsaEncryption.Encrypt(data, null));
        }

        [Test]
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
        {
            RsaEncryption.CreateRandomKeys(2048, out byte[] publicBlob, out byte[] privateBlob);

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var result = RsaEncryption.Encrypt(plainBytes, publicBlob);

            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void Decrypt_WithNullData_ThrowsArgumentNullException()
        {
            RsaEncryption.CreateRandomKeys(2048, out var publicKey, out var privateKey);

            Assert.Throws<ArgumentNullException>(() => RsaEncryption.Decrypt(null, publicKey));
        }

        [Test]
        public void Decrypt_WithNullKey_ThrowsArgumentNullException()
        {
            var data = Encoding.UTF8.GetBytes("asdf");

            Assert.Throws<ArgumentNullException>(() => RsaEncryption.Decrypt(data, null));
        }

        [Test]
        public void EncryptingAndDecryptingText_WithMessage_RestoresOriginalMessage()
        {
            RsaEncryption.CreateRandomKeys(2048, out byte[] publicBlob, out byte[] privateBlob);

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var encrypted = RsaEncryption.Encrypt(plainBytes, publicBlob);
            var decrypted = RsaEncryption.Decrypt(encrypted, privateBlob);

            Assert.AreEqual(plainBytes, decrypted);
        }
    }
}
