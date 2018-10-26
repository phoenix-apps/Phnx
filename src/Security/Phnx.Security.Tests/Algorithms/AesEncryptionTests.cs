using NUnit.Framework;
using Phnx.IO;
using Phnx.Security.Algorithms;
using System.IO;

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

            var input = new MemoryStream();
            input.Write(plaintext);

            var output = new MemoryStream();

            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            Aes.Encrypt(input, key, iv, output);

            output.Position = 0;
            var results = output.ReadToEndAsString();

            Assert.AreNotEqual(plaintext, results);
        }

        [Test]
        public void EncryptThenDecrypt_GetsOriginalMessage()
        {
            string plaintext = "This is some really long test text";
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            byte[] encrypted;

            {
                var input = new MemoryStream();
                input.Write(plaintext);
                input.Position = 0;

                var output = new MemoryStream();

                Aes.Encrypt(input, key, iv, output);
                encrypted = output.ToArray();
            }

            string results;
            {
                var input = new MemoryStream(encrypted);
                var output = new MemoryStream();

                Aes.Decrypt(input, key, iv, output);

                output.Position = 0;
                results = output.ReadToEndAsString();
            }

            Assert.AreEqual(plaintext, results);
        }
    }
}
