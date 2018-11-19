using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.Text;

namespace Phnx.Security.Tests.Algorithms
{
    public class Pbkdf2HashTests
    {
        public Pbkdf2HashTests()
        {
            Pbkdf2Hash = new Pbkdf2Hash();
        }

        private Pbkdf2Hash Pbkdf2Hash { get; }

        [Test]
        public void Hash_WithNullData_ThrowsArgumentNullException()
        {
            var salt = Pbkdf2Hash.GenerateSalt();

            Assert.Throws<ArgumentNullException>(() => Pbkdf2Hash.Hash(null, salt, 1));
        }

        [Test]
        public void Hash_WithNullSalt_ThrowsArgumentNullException()
        {
            var data = Encoding.UTF8.GetBytes("asdf");

            Assert.Throws<ArgumentNullException>(() => Pbkdf2Hash.Hash(data, null, 1));
        }

        [Test]
        public void Hash_WithNegativeIterations_ThrowsArgumentLessThanZeroException()
        {
            var data = Encoding.UTF8.GetBytes("asdf");
            var salt = Pbkdf2Hash.GenerateSalt();

            Assert.Throws<ArgumentLessThanZeroException>(() => Pbkdf2Hash.Hash(data, salt, -1));
        }

        [Test]
        public void Hash_WithZeroIterations_ReturnsOriginalData()
        {
            var data = Encoding.UTF8.GetBytes("asdf");
            var salt = Pbkdf2Hash.GenerateSalt();

            var hashed = Pbkdf2Hash.Hash(data, salt, 0);

            CollectionAssert.AreEqual(data, hashed);
        }

        [Test]
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
        {
            // Arrange
            byte[] salt = Pbkdf2Hash.GenerateSalt();

            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Act
            var result = Pbkdf2Hash.Hash(plainBytes, salt, 1);

            // Assert
            Assert.AreNotEqual(plainBytes, result);
        }
    }
}
