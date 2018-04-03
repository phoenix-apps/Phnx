using NUnit.Framework;

namespace MarkSFrancis.Data.Tests
{
    public class EscapedTests
    {
        [Test]
        public void EscapingText_WithNoContent_ReturnsEmptyString()
        {
            string text = string.Empty;

            string result = Escaped.Escape(text, '\0', '\0');

            Assert.AreEqual(text, result);
        }

        [Test]
        public void EscapingText_WithContentAndNoEscapeChars_ReturnsContent()
        {
            string text = "asdf";

            string result = Escaped.Escape(text, '\0', '\0');

            Assert.AreEqual(text, result);
        }

        [Test]
        public void EscapingText_WithEscapeChar_ReturnsEscaped()
        {
            string text = "as\0df";

            string result = Escaped.Escape(text, '\0');

            Assert.AreEqual("as\0\0df", result);
        }

        [Test]
        public void EscapingText_WithCharsToEscape_ReturnsEscaped()
        {
            string text = "asdf";

            string result = Escaped.Escape(text, '\0', 'a');

            Assert.AreEqual("\0asdf", result);
        }

        [Test]
        public void EscapingText_WithEscapeCharAndCharsToEscape_ReturnsEscaped()
        {
            string text = "as\0df";

            string result = Escaped.Escape(text, '\0', 'a');

            Assert.AreEqual("\0as\0\0df", result);
        }
    }
}
