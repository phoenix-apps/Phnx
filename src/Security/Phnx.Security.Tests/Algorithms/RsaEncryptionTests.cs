using NUnit.Framework;
using Phnx.Security.Algorithms;
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
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
        {
            RsaEncryption.CreateRandomKeys(2048, out byte[] publicBlob, out byte[] privateBlob);

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var result = RsaEncryption.Encrypt(plainBytes, publicBlob);

            Assert.AreNotEqual(plainBytes, result);
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
