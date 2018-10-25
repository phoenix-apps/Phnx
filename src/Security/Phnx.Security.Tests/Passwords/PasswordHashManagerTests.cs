using NUnit.Framework;
using Phnx.Security.Passwords;
using System;

namespace Phnx.Security.Tests.Passwords
{
    [TestFixture]
    public class PasswordHashManagerTests
    {
        public PasswordHashManager NewPasswordManager()
        {
            var mgr = new PasswordHashManager
            {
                { 0, new PasswordHashDefault() }
            };

            return mgr;
        }

        [Test]
        public void HashWithLatest_WithAValidPassword_GeneratesHash()
        {
            PasswordHashManager hashManager = NewPasswordManager();
            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            Assert.IsNotEmpty(securedPassword);
        }

        [Test]
        public void HashWithLatest_WithBrokenHashGenerator_ThrowsArgumentException()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashVersionBroken() }
            };

            Assert.Throws<ArgumentException>(() => hashManager.HashWithLatest("testPassword"));
        }

        [Test]
        public void HashWithLatest_WhenPasswordIsNull_TrhowsArgumentNullException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentNullException>(() => hashManager.HashWithLatest(null));
        }

        [Test]
        public void ShouldUpdateHash_WithOldHash_ReturnsTrue()
        {
            PasswordHashManager hashManager = NewPasswordManager();

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            hashManager.Add(1, new PasswordHashVersionMock());
            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            Assert.True(needsUpdate);
        }

        [Test]
        public void ShouldUpdateHash_WithNewHash_ReturnsFalse()
        {
            PasswordHashManager hashManager = NewPasswordManager();
            hashManager.Add(1, new PasswordHashVersionMock());

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            Assert.False(needsUpdate);
        }

        [Test]
        public void ShouldUpdateHash_WithNullHash_ThrowsArgumentNullException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentNullException>(() => hashManager.ShouldUpdateHash(null));
        }

        [Test]
        public void ShouldUpdateHash_WithHashMissingVersion_ThrowsArgumentException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentException>(() => hashManager.ShouldUpdateHash(new byte[0]));
        }

        [Test]
        public void PasswordMatchesHash_WithMatchingPasswords_ReturnsTrue()
        {
            PasswordHashManager hashManager = NewPasswordManager();
            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);
            bool matchesPassword = hashManager.PasswordMatchesHash(password, securedPassword);

            Assert.True(matchesPassword);
        }

        [Test]
        public void PasswordMatchesHash_WithOldHashThatMatches_ReturnsTrue()
        {
            PasswordHashManager hashManager = NewPasswordManager();

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            hashManager.Add(1, new PasswordHashVersionMock());

            bool passwordsMatch = hashManager.PasswordMatchesHash(password, securedPassword);

            Assert.True(passwordsMatch);
        }

        [Test]
        public void PasswordMatchesHash_WithHashTooLong_ThrowsArgumentException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentException>(() => hashManager.PasswordMatchesHash("asdf", new byte[200]));
        }

        [Test]
        public void PasswordMatchesHash_WhenPasswordIsNull_ThrowsArgumentNullException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentNullException>(() => hashManager.PasswordMatchesHash(null, new byte[0]));
        }

        [Test]
        public void PasswordMatchesHash_WhenHashIsNull_ThrowsArgumentNullException()
        {
            var hashManager = NewPasswordManager();

            Assert.Throws<ArgumentNullException>(() => hashManager.PasswordMatchesHash(string.Empty, null));
        }
    }
}
