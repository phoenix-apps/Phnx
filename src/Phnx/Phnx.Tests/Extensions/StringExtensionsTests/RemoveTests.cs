using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.StringExtensionsTests
{
    public class RemoveTests
    {
        [Test]
        public void RemovingChars_WhenStringIsNull_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.Remove(' ', 'a', 'b'));
        }

        [Test]
        public void RemovingChars_WhenCharsToRemoveAreNull_ThrowsArgumentNullException()
        {
            string testString = "asdf";
            char[] charsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => testString.Remove(charsToRemove));
        }

        [Test]
        public void RemovingChars_WhenStringIsEmpty_ReturnsEmptyString()
        {
            string testString = string.Empty;

            var result = testString.Remove(' ', 'a', 'b');

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void RemovingChars_WhenStringDoesNotContainCharsToRemove_ReturnsOriginalString()
        {
            string testString = "asdf";

            var result = testString.Remove(' ', 'b', 'c');

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void RemovingChars_WhenStringDoesContainCharsToRemove_ReturnsCleansedStringWithOrderPreserved()
        {
            string testString = "asbda fa";

            var result = testString.Remove(' ', 'a', 'b');

            Assert.AreEqual("sdf", result);
        }

        [Test]
        public void RemovingStrings_WhenStringIsNull_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.Remove(" ", "a", "b"));
        }

        [Test]
        public void RemovingStrings_WhenStringsToRemoveAreNull_ThrowsArgumentNullException()
        {
            string testString = "asdf";
            string[] charsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => testString.Remove(charsToRemove));
        }

        [Test]
        public void RemovingStrings_WhenStringIsEmpty_ReturnsEmptyString()
        {
            string testString = string.Empty;

            var result = testString.Remove(" ", "a", "b");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void RemovingStrings_WhenStringDoesNotContainCharsToRemove_ReturnsOriginalString()
        {
            string testString = "asdf";

            var result = testString.Remove(" ", "b", "c");

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void RemovingStrings_WhenStringDoesContainCharsToRemove_ReturnsCleansedStringWithOrderPreserved()
        {
            string testString = "asbda ab fa";

            var result = testString.Remove(" ", "ab", "a");

            Assert.AreEqual("sbdf", result);
        }

        [Test]
        public void RemovingStrings_WhenStringDoesContainStringsToRemoveWhenStringsToRemoveContainsOtherStringsToRemove_ReturnsCleansedStringWithOrderPreserved()
        {
            string testString = "asbda ab fa";

            var result = testString.Remove(" ", "a", "ab");

            Assert.AreEqual("sbdf", result);
        }
    }
}
