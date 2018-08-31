using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions
{
    public class ErrorFactoryExtensionsTests
    {
        [Test]
        public void GetErrorCollectionEmpty_CreatesError()
        {
            var error = ErrorFactory.Default.CollectionEmpty("asdf");

            Assert.IsInstanceOf<IndexOutOfRangeException>(error);
            Assert.IsTrue(error.Message.Contains("asdf"));
        }

        [Test]
        public void GetErrorKeyNotFound_WithoutCollectionName_CreatesError()
        {
            var error = ErrorFactory.Default.KeyNotFound("asdf");

            Assert.IsInstanceOf<KeyNotFoundException>(error);
            Assert.IsTrue(error.Message.Contains("asdf"));
        }

        [Test]
        public void GetErrorKeyNotFound_WithCollectionName_CreatesError()
        {
            var error = ErrorFactory.Default.KeyNotFound("asdf", "colName");

            Assert.IsInstanceOf<KeyNotFoundException>(error);
            Assert.IsTrue(error.Message.Contains("asdf"));
            Assert.IsTrue(error.Message.Contains("colName"));
        }

        [Test]
        public void GetErrorDuplicateKey_WithoutCollectionName_CreatesError()
        {
            var error = ErrorFactory.Default.DuplicateKey("asdf");

            Assert.IsInstanceOf<ArgumentException>(error);
            Assert.IsTrue(error.Message.Contains("asdf"));
        }

        [Test]
        public void GetErrorDuplicateKey_WithCollectionName_CreatesError()
        {
            var error = ErrorFactory.Default.DuplicateKey("asdf", "colName");

            Assert.IsInstanceOf<ArgumentException>(error);
            Assert.IsTrue(error.Message.Contains("asdf"));
            Assert.IsTrue(error.Message.Contains("colName"));
        }
    }
}
