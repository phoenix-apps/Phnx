using MarkSFrancis.Extensions;
using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions.StringExtensionsTests
{
    public class NullEmptyWhitespaceTests
    {
        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsNull_ReturnsTrue()
        {
            string test = null;

            Assert.IsTrue(test.IsNullOrEmptyOrWhitespace());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsEmpty_ReturnsTrue()
        {
            string test = string.Empty;

            Assert.IsTrue(test.IsNullOrEmptyOrWhitespace());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIs5Whitespace_ReturnsTrue()
        {
            string test = ' '.ToString(5);

            Assert.IsTrue(test.IsNullOrEmptyOrWhitespace());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsText_ReturnsFalse()
        {
            string test = '%'.ToString(5);

            Assert.IsFalse(test.IsNullOrEmptyOrWhitespace());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsUnicodeText_ReturnsFalse()
        {
            string test = "😂";

            Assert.IsFalse(test.IsNullOrEmptyOrWhitespace());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsTextThatContainsWhitespace_ReturnsFalse()
        {
            string test = " * ";

            Assert.IsFalse(test.IsNullOrEmptyOrWhitespace());
        }
    }
}
