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
            HashManagerService hashManager = new HashManagerService(new HashGeneratorVersionZero());
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
            HashManagerService hashManager = new HashManagerService(new HashGeneratorVersionZero());
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
            HashManagerService hashManager = new HashManagerService(
                new HashGeneratorVersionZero());

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
            HashManagerService hashManager = new HashManagerService(
                new HashGeneratorVersionZero(),
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
            HashManagerService hashManager = new HashManagerService(
                new HashGeneratorVersionZero());

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