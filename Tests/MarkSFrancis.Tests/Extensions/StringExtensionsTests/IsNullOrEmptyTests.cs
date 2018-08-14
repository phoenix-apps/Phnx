using MarkSFrancis.Extensions;
using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions.StringExtensionsTests
{
    public class IsNullOrEmptyTests
    {
        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsNull_ReturnsTrue()
        {
            string test = null;

            Assert.IsTrue(test.IsNullOrEmpty());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsEmpty_ReturnsTrue()
        {
            string test = string.Empty;

            Assert.IsTrue(test.IsNullOrEmpty());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsText_ReturnsFalse()
        {
            string test = '%'.ToString(5);

            Assert.IsFalse(test.IsNullOrEmpty());
        }

        [Test]
        public void GetIfStringIsNullOrEmptyOrWhitespace_WhenStringIsUnicodeText_ReturnsFalse()
        {
            string test = "😂";

            Assert.IsFalse(test.IsNullOrEmpty());
        }
    }
}
