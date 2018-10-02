using NUnit.Framework;

namespace Phnx.Data.Tests
{
    public class ErrorMessagesExtensionsTests
    {
        [Test]
        public void CannotSetValue_GetsMessageWhichIsNotNullOrEmpty()
        {
            var message = ErrorMessage.Factory.CannotSetValue();

            Assert.IsFalse(string.IsNullOrWhiteSpace(message));
        }
    }
}
