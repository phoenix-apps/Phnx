using NUnit.Framework;
using Phnx.IO;
using Phnx.Security.Algorithms;
using System;
using System.IO;

namespace Phnx.Security.Tests
{
    public class PasswordProtectedEncryptionServiceTests
    {
        private PasswordProtectedEncryptionService Create()
        {
            var aes = new AesEncryption();
            var hasher = new Sha256Hash();
            return new PasswordProtectedEncryptionService(aes, hasher);
        }

        [Test]
        public void New_WhenSymmetricEncryptionIsNull_ThrowsArgumentNullException()
        {
            var hasher = new Sha256Hash();
            Assert.Throws<ArgumentNullException>(() => new PasswordProtectedEncryptionService(null, hasher));
        }

        [Test]
        public void New_WhenPasswordHasherIsNull_ThrowsArgumentNullException()
        {
            var aes = new AesEncryption();
            Assert.Throws<ArgumentNullException>(() => new PasswordProtectedEncryptionService(aes, null));
        }

        [Test]
        public void New_WithValidArgs_SetsPasswordHashAndEncryptionAlgorithm()
        {
            var aes = new AesEncryption();
            var hasher = new Sha256Hash();
            var encryptor = new PasswordProtectedEncryptionService(aes, hasher);

            Assert.AreEqual(aes, encryptor.EncryptionAlgorithm);
            Assert.AreEqual(hasher, encryptor.PasswordHash);
        }

        [Test]
        public void Encrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.Encrypt(null, "test", new MemoryStream()));
        }

        [Test]
        public void Encrypt_WithNullPassword_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.Encrypt(new MemoryStream(), null, new MemoryStream()));
        }

        [Test]
        public void Encrypt_WithNullOutput_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.Encrypt(new MemoryStream(), "test", null));
        }

        [Test]
        public void TryDecrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.TryDecrypt(null, "test", new MemoryStream()));
        }

        [Test]
        public void TryDecrypt_WithNullPassword_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.TryDecrypt(new MemoryStream(), null, new MemoryStream()));
        }

        [Test]
        public void TryDecrypt_WithNullOutput_ThrowsArgumentNullException()
        {
            var encryptor = Create();

            Assert.Throws<ArgumentNullException>(() => encryptor.TryDecrypt(new MemoryStream(), "test", null));
        }

        [Test]
        public void EncryptThenDecrypt_WithValidArguments_GetsOriginalData()
        {
            var encryptor = Create();
            var plaintext = "ajstiglra;dsf gasdfgaewsy ha4reg 4wer3fg5rt f3g5etsrbfs hg;hg eg hs tger hel";
            string password = "This is just @ te5T";

            var original = new PipeStream();
            original.Write(plaintext);

            var encrypted = new PipeStream();
            encryptor.Encrypt(original, password, encrypted);

            var decrypted = new PipeStream();
            var success = encryptor.TryDecrypt(encrypted, password, decrypted);

            Assert.IsTrue(success);

            var results = decrypted.ReadToEndAsString();
            Assert.AreEqual(plaintext, results);
        }

        [Test]
        public void EncryptThenDecrypt_WithInvalidPassword_ReturnsFalse()
        {
            var encryptor = Create();
            var plaintext = "ajstiglra;dsf gasdfgaewsy ha4reg 4wer3fg5rt f3g5etsrbfs hg;hg eg hs tger hel";
            string password = "This is just @ te5T";

            var original = new PipeStream();
            original.Write(plaintext);

            var encrypted = new PipeStream();
            encryptor.Encrypt(original, password, encrypted);

            var decrypted = new PipeStream();
            var success = encryptor.TryDecrypt(encrypted, string.Empty, decrypted);

            Assert.IsFalse(success);
        }
    }
}
