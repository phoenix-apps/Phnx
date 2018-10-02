using NUnit.Framework;
using System;

namespace Phnx.Tests
{
    public class ErrorMessageTests
    {
        [Test]
        public void GetErrorInvalidCast_GetsMessage()
        {
            string paramName = "asdf", type1 = "string1", type2 = "string2";

            var error = ErrorMessage.Factory.InvalidCast(paramName, type1, type2);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(type1));
            Assert.IsTrue(error.Contains(type2));
        }

        [Test]
        public void GetErrorInvalidCast_WithTypes_GetsMessage()
        {
            string paramName = "asdf";
            Type type1 = typeof(string), type2 = typeof(Guid);

            var error = ErrorMessage.Factory.InvalidCast(paramName, type1, type2);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(type1.FullName));
            Assert.IsTrue(error.Contains(type2.FullName));
        }

        [Test]
        public void GetErrorIndexOutOfRange_GetsMessage()
        {
            string paramName = "asdf";

            var error = ErrorMessage.Factory.IndexOutOfRange(paramName);

            Assert.IsTrue(error.Contains(paramName));
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValue_GetsMessage()
        {
            string paramName = "asdf";
            int index = 42;

            var error = ErrorMessage.Factory.IndexOutOfRange(paramName, index);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(index.ToString()));
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionSize_GetsMessage()
        {
            string paramName = "asdf";
            int index = 42, size = 13;

            var error = ErrorMessage.Factory.IndexOutOfRange(paramName, index, size);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(index.ToString()));
            Assert.IsTrue(error.Contains(size.ToString()));
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionName_GetsMessage()
        {
            string paramName = "asdf", collectionName = "myCol";
            int index = 42;

            var error = ErrorMessage.Factory.IndexOutOfRange(paramName, index, collectionName);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(index.ToString()));
            Assert.IsTrue(error.Contains(collectionName));
        }

        [Test]
        public void GetErrorIndexOutOfRange_WithValueAndCollectionNameAndCollectionSize_GetsMessage()
        {
            string paramName = "asdf", collectionName = "myCol";
            int index = 42, size = 13;

            var error = ErrorMessage.Factory.IndexOutOfRange(paramName, index, collectionName, size);

            Assert.IsTrue(error.Contains(paramName));
            Assert.IsTrue(error.Contains(index.ToString()));
            Assert.IsTrue(error.Contains(collectionName));
            Assert.IsTrue(error.Contains(size.ToString()));
        }

        [Test]
        public void GetErrorNotImplemented_WithTodo_GetsMessage()
        {
            string todoNote = "Note";
            var error = ErrorMessage.Factory.NotImplemented(todoNote);

            Assert.IsTrue(error.Contains(todoNote));
        }
    }
}
