using MarkSFrancis.Security.Passwords;
using NUnit.Framework;

namespace MarkSFrancis.Security.Tests.Hash
{
    [TestFixture]
    public class HashManagerServiceTests
    {
        [Test]
        public void HashingAPassword_WithANewSalt_GeneratesHash()
        {
            // Arrange
            PasswordHashManager hashManager = new PasswordHashManager(new PasswordHashVersionZero());
            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashPasswordWithLatestHashGenerator(password);

            // Assert
            Assert.IsNotEmpty(securedPassword);
        }

        [Test]
        public void CheckingIfPasswordsMatch_WithMatchingPasswords_MatchesPassword()
        {
            // Arrange
            PasswordHashManager hashManager = new PasswordHashManager(new PasswordHashVersionZero());
            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashPasswordWithLatestHashGenerator(password);
            bool matchesPassword = hashManager.PasswordMatch(password, securedPassword);

            // Assert
            Assert.True(matchesPassword);
        }

        [Test]
        public void CheckingIfPasswordIsLatest_WithOldHash_AsksForRehash()
        {
            // Arrange
            PasswordHashManager hashManager = new PasswordHashManager(
                new PasswordHashVersionZero());

            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashPasswordWithLatestHashGenerator(password);

            hashManager.AddHashGenerator(new HashGeneratorMock());
            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            // Assert
            Assert.True(needsUpdate);
        }

        [Test]
        public void CheckingIfPasswordIsLatest_WithNewHash_DoesNotAskForRehash()
        {
            // Arrange
            PasswordHashManager hashManager = new PasswordHashManager(
                new PasswordHashVersionZero(),
                new HashGeneratorMock());

            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashPasswordWithLatestHashGenerator(password);
            
            bool needsUpdate = hashManager.ShouldUpdateHash(securedPassword);

            // Assert
            Assert.False(needsUpdate);
        }

        [Test]
        public void LoggingIn_WithOldHash_StillLogsIn()
        {
            // Arrange
            PasswordHashManager hashManager = new PasswordHashManager(
                new PasswordHashVersionZero());

            string password = "password";

            // Act
            byte[] securedPassword = hashManager.HashPasswordWithLatestHashGenerator(password);

            hashManager.AddHashGenerator(new HashGeneratorMock());

            bool passwordsMatch = hashManager.PasswordMatch(password, securedPassword);

            // Assert
            Assert.True(passwordsMatch);
        }
    }
}