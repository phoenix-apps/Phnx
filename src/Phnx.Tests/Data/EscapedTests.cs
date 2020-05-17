using NUnit.Framework;
using System;

namespace Phnx.Data.Tests
{
    public class EscapedTests
    {
        [Test]
        public void EscapingText_WhenTextIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Escaped.Escape(null, ' '));
        }

        [Test]
        public void EscapingText_WhenEscapeTheseCharsIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Escaped.Escape(string.Empty, ' ', null));
        }

        [Test]
        public void EscapingText_WithNoContent_ReturnsEmptyString()
        {
            string text = string.Empty;

            string result = Escaped.Escape(text, '\0', '\0');

            Assert.AreEqual(text, result);
        }

        [Test]
        public void EscapingText_WithContentAndNoEscapeChar_ReturnsContent()
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
        public void EscapingText_WithCharToEscape_ReturnsEscaped()
        {
            string text = "asdf";

            string result = Escaped.Escape(text, '\0', 'a');

            Assert.AreEqual("\0asdf", result);
        }

        [Test]
        public void EscapingText_WithEscapeCharAndCharToEscape_ReturnsEscaped()
        {
            string text = "as\0df";

            string result = Escaped.Escape(text, '\0', 'a');

            Assert.AreEqual("\0as\0\0df", result);
        }

        [Test]
        public void EscapingText_WithEscapeCharAndCharsToEscape_ReturnsEscaped()
        {
            string text = "as\0df";

            string result = Escaped.Escape(text, '\0', 'a', 'd');

            Assert.AreEqual("\0as\0\0\0df", result);
        }
    }
}
