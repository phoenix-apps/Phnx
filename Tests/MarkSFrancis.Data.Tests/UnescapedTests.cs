using NUnit.Framework;

namespace MarkSFrancis.Data.Tests
{
    public class UnescapedTests
    {
        [Test]
        public void UnescapingText_WithNoContent_ReturnsEmptyString()
        {
            string text = string.Empty;

            string result = Escaped.Unescape(text, '\0', '\0');

            Assert.AreEqual(text, result);
        }

        [Test]
        public void UnescapingText_WithContentAndNoEscapeChars_ReturnsContent()
        {
            string text = "asdf";

            string result = Escaped.Unescape(text, '\0', '\0');

            Assert.AreEqual(text, result);
        }

        [Test]
        public void UnescapingText_WithEscapeChar_ReturnsUnescaped()
        {
            string text = "as\0\0df";

            string result = Escaped.Unescape(text, '\0');

            Assert.AreEqual("as\0df", result);
        }

        [Test]
        public void UnescapingText_WithCharsToEscape_ReturnsUnescaped()
        {
            string text = "\0asdf";

            string result = Escaped.Unescape(text, '\0', 'a');

            Assert.AreEqual("asdf", result);
        }

        [Test]
        public void UnescapingText_WithEscapeCharAndCharsToEscape_ReturnsUnescaped()
        {
            string text = "\0as\0\0df";

            string result = Escaped.Unescape(text, '\0', 'a');

            Assert.AreEqual("as\0df", result);
        }
    }
}