using NUnit.Framework;

namespace Phnx.Security.Tests.Extensions
{
    public class SecurityErrorMessageExtensionsTests
    {
        [Test]
        public void GetInvalidSaltSize_GetsMessageWithInfo()
        {
            var saltLengthShouldBe = 24;
            var saltLengthWas = 652;

            var message = ErrorMessage.Factory.InvalidSaltSize(saltLengthShouldBe, saltLengthWas);

            Assert.IsFalse(string.IsNullOrWhiteSpace(message));
            Assert.IsTrue(message.Contains(saltLengthShouldBe.ToString()));
            Assert.IsTrue(message.Contains(saltLengthWas.ToString()));
        }

        [Test]
        public void GetInvalidHashConfiguration_GetsMessageWithInfoIn()
        {
            var hashLengthShouldBe = 24;
            var hashLengthWas = 652;

            var message = ErrorMessage.Factory.InvalidHashConfiguration(hashLengthShouldBe, hashLengthWas);

            Assert.IsFalse(string.IsNullOrWhiteSpace(message));
            Assert.IsTrue(message.Contains(hashLengthShouldBe.ToString()));
            Assert.IsTrue(message.Contains(hashLengthWas.ToString()));
        }
    }
}
