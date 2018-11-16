using NUnit.Framework;
using Phnx.IO;
using Phnx.Security.Algorithms;

namespace Phnx.Security.Tests.Algorithms
{
    public class AesEncryptionTests
    {
        public AesEncryptionTests()
        {
            Aes = new AesEncryption();
        }

        public AesEncryption Aes { get; }

        [Test]
        public void CreateRandomKey_GetsKeyOfSizeKeySizeDividedBy8()
        {
            var key = Aes.CreateRandomKey();

            Assert.AreEqual(AesEncryption.KeyBits / 8, key.Length);
        }

        [Test]
        public void CreateRandomIv_GetsKeyOfSizeIVSizeDividedBy8()
        {
            var key = Aes.CreateRandomIv();

            Assert.AreEqual(AesEncryption.IvBits / 8, key.Length);
        }

        [Test]
        public void Encrypt_GeneratesUnreadableBytes()
        {
            string plaintext = "Test text";

            var input = new PipeStream();
            input.Write(plaintext);

            var output = new PipeStream();

            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            Aes.Encrypt(input, key, iv, output);

            var results = output.ReadToEndAsString();

            Assert.IsFalse(string.IsNullOrEmpty(results));
            Assert.AreNotEqual(plaintext, results);
        }

        [Test]
        public void EncryptThenDecrypt_GetsOriginalMessage()
        {
            string plaintext = "This is some really long test text";
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();

            var original = new PipeStream();
            original.Write(plaintext);

            var encrypted = new PipeStream();

            Aes.Encrypt(original, key, iv, encrypted);

            var output = new PipeStream();
            Aes.Decrypt(encrypted, key, iv, output);

            var results = output.ReadToEndAsString();

            Assert.AreEqual(plaintext, results);
        }
    }
}
