using System.Text;
using NUnit.Framework;

namespace MarkSFrancis.Security.Tests
{
    class RsaEncryptionTests
    {
        public RsaEncryptionTests()
        {
            RsaEncryption = new RsaEncryption();
        }

        private RsaEncryption RsaEncryption { get; }

        [Test]
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
        {
            // Arrange
            RsaEncryption.CreateRandomKeys(out byte[] publicBlob, out byte[] privateBlob);

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Act
            var result = RsaEncryption.Encrypt(plainBytes, publicBlob);

            // Assert
            Assert.AreNotEqual(plainBytes, result);
        }

        [Test]
        public void EncryptingAndDecryptingText_WithMessage_RestoresOriginalMessage()
        {
            // Arrange
            RsaEncryption.CreateRandomKeys(out byte[] publicBlob, out byte[] privateBlob);

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Act
            var encrypted = RsaEncryption.Encrypt(plainBytes, publicBlob);
            var decrypted = RsaEncryption.Decrypt(encrypted, privateBlob);

            // Assert
            Assert.AreEqual(plainBytes, decrypted);
        }
    }
}
