using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests
{
    public class ErrorFactoryTests
    {
        [Test]
        public void GetErrorInvalidCast_CreatesError()
        {
            var error = ErrorFactory.Default.InvalidCast("asdf", "string", "string");

            Assert.IsInstanceOf<InvalidCastException>(error);
        }

        [Test]
        public void GetErrorInvalidCast_WithTypes_CreatesError()
        {
            var error = ErrorFactory.Default.InvalidCast("asdf", typeof(string), typeof(string));

            Assert.IsInstanceOf<InvalidCastException>(error);
        }

        [Test]
        public void GetErrorArgumentNull_CreatesError()
        {
            var error = ErrorFactory.Default.ArgumentNull("asdf");

            Assert.IsInstanceOf<ArgumentNullException>(error);
        }

        [Test]
        public void GetErrorArgumentNull_WithMessage_CreatesError()
        {
            string message = "test message";
            var error = ErrorFactory.Default.ArgumentNull("asdf", message);

            Assert.IsInstanceOf<ArgumentNullException>(error);
            Assert.IsTrue(error.Message.Contains(message));
        }

        [Test]
        public void GetErrorArgumentOutOfRange_CreatesError()
        {
            var error = ErrorFactory.Default.ArgumentOutOfRange("asdf");

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorArgumentOutOfRange_WithMessage_CreatesError()
        {
            string message = "test message";
            var error = ErrorFactory.Default.ArgumentOutOfRange("asdf", message);

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
            Assert.IsTrue(error.Message.Contains(message));
        }

        [Test]
        public void GetErrorArgumentLessThanZero_CreatesError()
        {
            var error = ErrorFactory.Default.ArgumentOutOfRange("asdf");

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_CreatesError()
        {
            var error = ErrorFactory.Default.IndexOutOfRange("asdf");

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValue_CreatesError()
        {
            var error = ErrorFactory.Default.IndexOutOfRange("asdf", 0);

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionSize_CreatesError()
        {
            var error = ErrorFactory.Default.IndexOutOfRange("asdf", 0, 0);

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionName_CreatesError()
        {
            var error = ErrorFactory.Default.IndexOutOfRange("asdf", 0, "test");

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionNameAndCollectionSize_CreatesError()
        {
            var error = ErrorFactory.Default.IndexOutOfRange("asdf", 0, "test", 0);

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
        }

        [Test]
        public void GetErrorNotImplemented_CreatesError()
        {
            var error = ErrorFactory.Default.NotImplemented();

            Assert.IsInstanceOf<NotImplementedException>(error);
        }

        [Test]
        public void GetErrorNotImplemented_WithTodo_CreatesError()
        {
            string todoNote = "Note";
            var error = ErrorFactory.Default.NotImplemented(todoNote);

            Assert.IsInstanceOf<NotImplementedException>(error);
            Assert.IsTrue(error.Message.Contains(todoNote));
        }
    }
}
