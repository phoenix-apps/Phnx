using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests
{
    public class ErrorFactoryTests
    {
        [Test]
        public void GetErrorInvalidCast_CreatesError()
        {
            var error = ErrorFactory.InvalidCast("asdf", "string", "string").Create();

            Assert.IsInstanceOf<InvalidCastException>(error);
        }

        [Test]
        public void GetErrorInvalidCast_WithTypes_CreatesError()
        {
            var error = ErrorFactory.InvalidCast("asdf", typeof(string), typeof(string)).Create();

            Assert.IsInstanceOf<InvalidCastException>(error);
        }

        [Test]
        public void GetErrorArgumentNull_CreatesError()
        {
            var error = ErrorFactory.ArgumentNull("asdf").Create();

            Assert.IsInstanceOf<ArgumentNullException>(error);
        }

        [Test]
        public void GetErrorArgumentNull_WithMessage_CreatesError()
        {
            string message = "test message";
            var error = ErrorFactory.ArgumentNull("asdf", message).Create();

            Assert.IsInstanceOf<ArgumentNullException>(error);
            Assert.IsTrue(error.Message.Contains(message));
        }

        [Test]
        public void GetErrorArgumentOutOfRange_CreatesError()
        {
            var error = ErrorFactory.ArgumentOutOfRange("asdf").Create();

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorArgumentOutOfRange_WithMessage_CreatesError()
        {
            string message = "test message";
            var error = ErrorFactory.ArgumentOutOfRange("asdf", message).Create();

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
            Assert.IsTrue(error.Message.Contains(message));
        }

        [Test]
        public void GetErrorArgumentLessThanZero_CreatesError()
        {
            var error = ErrorFactory.ArgumentOutOfRange("asdf").Create();

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_CreatesError()
        {
            var error = ErrorFactory.IndexOutOfRange("asdf").Create();

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValue_CreatesError()
        {
            var error = ErrorFactory.IndexOutOfRange("asdf", 0).Create();

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionSize_CreatesError()
        {
            var error = ErrorFactory.IndexOutOfRange("asdf", 0, 0).Create();

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionName_CreatesError()
        {
            var error = ErrorFactory.IndexOutOfRange("asdf", 0, "test").Create();

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionNameAndCollectionSize_CreatesError()
        {
            var error = ErrorFactory.IndexOutOfRange("asdf", 0, "test", 0).Create();

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorNotImplemented_CreatesError()
        {
            var error = ErrorFactory.NotImplemented().Create();

            Assert.IsInstanceOf<NotImplementedException>(error);
        }

        [Test]
        public void GetErrorNotImplemented_WithTodo_CreatesError()
        {
            string todoNote = "Note";
            var error = ErrorFactory.NotImplemented(todoNote).Create();

            Assert.IsInstanceOf<NotImplementedException>(error);
            Assert.IsTrue(error.Message.Contains(todoNote));
        }
    }
}
