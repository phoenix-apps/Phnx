using System.Text;
using NUnit.Framework;

namespace Phnx.Security.Tests
{
    public class AesEncryptionTests
    {
        public AesEncryptionTests()
        {
            AesEncryption = new AesEncryption();
        }

        private AesEncryption AesEncryption { get; }

        [Test]
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
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
        public void EncryptingAndDecryptingText_WithMessage_RestoresOriginalMessage()
        {
            // Arrange
            byte[] key = AesEncryption.CreateRandomKey();
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Act
            var encrypted = AesEncryption.Encrypt(plainBytes, key);
            var decrypted = AesEncryption.Decrypt(encrypted, key);

            // Assert
            Assert.AreEqual(plainBytes, decrypted);
        }
    }
}
