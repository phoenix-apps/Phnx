using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.StringExtensionsTests.PascalCamelCaseTests
{
    public class From
    {
        [Test]
        public void FromCamelCase_WithNullString_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.FromCamelAndPascalCase(false));
        }

        [Test]
        public void FromCamelCase_WithEmptyString_ReturnsEmptyString()
        {
            string testString = string.Empty;

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(testString, result);

            result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void FromCamelCase_WithSingleWord_ReturnsSingleWord()
        {
            string testString = "asdf";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void FromCamelCase_WithSingleWordAndCapitalWords_ReturnsSingleWordStartingWithCapital()
        {
            string testString = "asdf";
            string expected = "Asdf";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromCamelCase_WithSentenceAndLowercaseWords_ReturnsSentence()
        {
            string testString = "packMyBoxWithFiveDozenLiquorJugs";
            string expected = "pack my box with five dozen liquor jugs";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromPascalCase_WithSentenceAndUppercaseWords_ReturnsSentence()
        {
            string testString = "PackMyBoxWithFiveDozenLiquorJugs";
            string expected = "Pack My Box With Five Dozen Liquor Jugs";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromCamelCase_WithNumbersInSentenceAndLowercaseWords_ReturnsSentence()
        {
            string testString = "packMyBoxWith54DozenLiquorJugs";
            string expected = "pack my box with 54 dozen liquor jugs";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromPascalCase_WithNumbersInSentenceAndUppercaseWords_ReturnsSentence()
        {
            string testString = "PackMyBoxWith54DozenLiquorJugs";
            string expected = "Pack My Box With 54 Dozen Liquor Jugs";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromCamelCase_WithAcronym_ReturnsSentence()
        {
            string testString = "thisIsAnIDForAUser";
            string expected = "this is an ID for a user";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FromPascalCase_WithAcronym_ReturnsSentence()
        {
            string testString = "thisIsAnIDForAUser";
            string expected = "This Is An ID For A User";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expected, result);
        }
    }
}
