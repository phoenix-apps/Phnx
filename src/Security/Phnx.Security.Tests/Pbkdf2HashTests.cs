using NUnit.Framework;
using Phnx.Security.Algorithms;
using System.Text;

namespace Phnx.Security.Tests
{
    public class Pbkdf2HashTests
    {
        public Pbkdf2HashTests()
        {
            Pbkdf2Hash = new Pbkdf2Hash();
        }

        private Pbkdf2Hash Pbkdf2Hash { get; }

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
