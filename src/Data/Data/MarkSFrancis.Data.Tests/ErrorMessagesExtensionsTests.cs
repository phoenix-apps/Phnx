using NUnit.Framework;

namespace MarkSFrancis.Data.Tests
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
