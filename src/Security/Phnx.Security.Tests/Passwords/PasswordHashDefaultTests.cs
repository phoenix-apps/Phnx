using NUnit.Framework;
using Phnx.Security.Algorithms;
using Phnx.Security.Passwords;
using System;

namespace Phnx.Security.Tests.Passwords
{
    public class PasswordHashDefaultTests
    {
        [Test]
        public void GenerateHash_WithNullPassword_ThrowsArgumentNullException()
        {
            var hashGenerator = new PasswordHashDefault(new Pbkdf2Hash());

            Assert.Throws<ArgumentNullException>(() => hashGenerator.GenerateHash(null, new byte[0]));
        }

        [Test]
        public void GenerateHash_WithNullSalt_ThrowsArgumentNullException()
        {
            var hashGenerator = new PasswordHashDefault(new Pbkdf2Hash());

            Assert.Throws<ArgumentNullException>(() => hashGenerator.GenerateHash(new byte[0], null));
        }

        [Test]
        public void GenerateSalt_MatchesSaltBytesLength()
        {
            var hashGenerator = new PasswordHashDefault(new Pbkdf2Hash());

            var salt = hashGenerator.GenerateSalt();

            Assert.AreEqual(hashGenerator.SaltBytesLength, salt.Length);
        }
    }
}
