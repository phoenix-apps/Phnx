using NUnit.Framework;

namespace MarkSFrancis.Collections.Tests.Exceptions
{
    public class ErrorMessageExtensionsTests
    {
        [Test]
        public void SequenceEmpty_WithoutCollectionName_MakesMessage()
        {
            var message = ErrorMessage.Factory.SequenceEmpty();

            Assert.IsFalse(string.IsNullOrWhiteSpace(message));
        }

        [Test]
        public void SequenceEmpty_WithParamName_CreatesMessage()
        {
            string collectionName = "asdf";

            var message = ErrorMessage.Factory.SequenceEmpty(collectionName);

            Assert.IsTrue(message.Contains(collectionName));
        }
    }
}
