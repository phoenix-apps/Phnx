using NUnit.Framework;
using System;

namespace Phnx.Tests.Exceptions
{
    public class ArgumentLessThanZeroExceptionTests
    {
        [Test]
        public void ArgumentLessThanZero_WithNoInfo_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ArgumentLessThanZeroException());
        }

        [Test]
        public void ArgumentLessThanZero_WithParamName_CreatesMessage()
        {
            string paramName = "asdf";
            var error = new ArgumentLessThanZeroException(paramName);

            Assert.IsTrue(error.Message.Contains(paramName));
        }

        [Test]
        public void ArgumentLessThanZero_WithParamNameAndCollectionName_CreatesMessage()
        {
            string paramName = "asdf", message = "test message";
            var error = new ArgumentLessThanZeroException(paramName, message);

            Assert.IsTrue(error.Message.Contains(paramName));
            Assert.IsTrue(error.Message.Contains(message));
        }

        [Test]
        public void ArgumentLessThanZero_WithMessageAndInnerException_CreatesMessageAndInnerException()
        {
            string message = "test message";
            Exception innerException = new IndexOutOfRangeException();
            var error = new ArgumentLessThanZeroException(message, innerException);

            Assert.AreEqual(message, error.Message);
            Assert.AreEqual(innerException, error.InnerException);
        }
    }
}
