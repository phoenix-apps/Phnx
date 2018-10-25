using NUnit.Framework;
using Phnx.Security.Passwords;

namespace Phnx.Security.Tests.Passwords
{
    [TestFixture]
    public class PasswordHashManagerTest
    {
        public PasswordHashManager NewPasswordManager()
        {
            var mgr = new PasswordHashManager();
            mgr.Add(0, new PasswordHashDefault());
            return mgr;
        }

        [Test]
        public void HashingAPassword_WithANewSalt_GeneratesHash()
        {
            // Arrange
            PasswordHashManager hashManager = NewPasswordManager();
            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashWithLatest(password);

            // Assert
            Assert.IsNotEmpty(securedPassword);
        }

        [Test]
        public void CheckingIfPasswordsMatch_WithMatchingPasswords_MatchesPassword()
        {
            PasswordHashManager hashManager = NewPasswordManager();
            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);
            bool matchesPassword = hashManager.PasswordMatchesHash(password, securedPassword);

            Assert.True(matchesPassword);
        }

        [Test]
        public void CheckingIfPasswordIsLatest_WithOldHash_AsksForRehash()
        {
            PasswordHashManager hashManager = NewPasswordManager();

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            hashManager.Add(1, new PasswordHashVersionMock());
            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            Assert.True(needsUpdate);
        }

        [Test]
        public void CheckingIfPasswordIsLatest_WithNewHash_DoesNotAskForRehash()
        {
            PasswordHashManager hashManager = NewPasswordManager();
            hashManager.Add(1, new PasswordHashVersionMock());

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            Assert.False(needsUpdate);
        }

        [Test]
        public void LoggingIn_WithOldHash_StillLogsIn()
        {
            PasswordHashManager hashManager = NewPasswordManager();

            string password = "password";

            byte[] securedPassword = hashManager.HashWithLatest(password);

            hashManager.Add(1, new PasswordHashVersionMock());

            bool passwordsMatch = hashManager.PasswordMatchesHash(password, securedPassword);

            Assert.True(passwordsMatch);
        }
    }
}