using NUnit.Framework;
using Phnx.IO;
using Phnx.Security.Algorithms;
using Phnx.Security.Tests.TestHelpers;
using System;

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
        public void CreateRandomKey_GetsKeyOfSizeKeyBitsDividedBy8()
        {
            var key = Aes.CreateRandomKey();

            Assert.AreEqual(AesEncryption.KeyBits / 8, key.Length);
        }

        [Test]
        public void CreateRandomIv_GetsKeyOfSizeIVBitsDividedBy8()
        {
            var key = Aes.CreateRandomIv();

            Assert.AreEqual(AesEncryption.IvBits / 8, key.Length);
        }

        [Test]
        public void EncryptWithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(null, new byte[0]));
        }

        [Test]
        public void EncryptWithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(new byte[0], null));
        }

        [Test]
        public void Encrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(null, key, iv, output));
        }

        [Test]
        public void Encrypt_WithNullKey_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(input, null, iv, output));
        }

        [Test]
        public void Encrypt_WithNullIv_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(input, key, null, output));
        }

        [Test]
        public void Encrypt_WithNullOutput_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();

            Assert.Throws<ArgumentNullException>(() => Aes.Encrypt(input, key, iv, null));
        }

        [Test]
        public void Encrypt_WithUnreadableInput_ThrowsArgumentException()
        {
            var input = new OneWayStream(false);
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentException>(() => Aes.Encrypt(input, key, iv, output));
        }

        [Test]
        public void Encrypt_WithReadOnlyOutput_ThrowsArgumentException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new OneWayStream(true);

            Assert.Throws<ArgumentException>(() => Aes.Encrypt(input, key, iv, output));
        }

        [Test]
        public void DecryptWithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(null, new byte[0]));
        }

        [Test]
        public void DecryptWithNullKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(new byte[0], null));
        }

        [Test]
        public void Decrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(null, key, iv, output));
        }

        [Test]
        public void Decrypt_WithNullKey_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(input, null, iv, output));
        }

        [Test]
        public void Decrypt_WithNullIv_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var output = new PipeStream();

            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(input, key, null, output));
        }

        [Test]
        public void Decrypt_WithNullOutput_ThrowsArgumentNullException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();

            Assert.Throws<ArgumentNullException>(() => Aes.Decrypt(input, key, iv, null));
        }

        [Test]
        public void Decrypt_WithUnreadableInput_ThrowsArgumentException()
        {
            var input = new OneWayStream(false);
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new PipeStream();

            Assert.Throws<ArgumentException>(() => Aes.Decrypt(input, key, iv, output));
        }

        [Test]
        public void Decrypt_WithReadOnlyOutput_ThrowsArgumentException()
        {
            var input = new PipeStream();
            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            var output = new OneWayStream(true);

            Assert.Throws<ArgumentException>(() => Aes.Decrypt(input, key, iv, output));
        }

        [Test]
        public void Encrypt_GeneratesUnreadableBytes()
        {
            string plaintext = "Test text";

            var input = new PipeStream();
            input.In.Write(plaintext);

            var output = new PipeStream();

            var key = Aes.CreateRandomKey();
            var iv = Aes.CreateRandomIv();
            Aes.Encrypt(input, key, iv, output);

            var results = output.Out.ReadToEnd();

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
            original.In.Write(plaintext);

            var encrypted = new PipeStream();

            Aes.Encrypt(original, key, iv, encrypted);

            var output = new PipeStream();
            Aes.Decrypt(encrypted, key, iv, output);

            var results = output.Out.ReadToEnd();

            Assert.AreEqual(plaintext, results);
        }
    }
}
