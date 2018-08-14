using MarkSFrancis.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.StringExtensionsTests
{
    public class PascalCamelCaseTests
    {
        [Test]
        public void ToCamelCase_WithNullString_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.ToCamelCase(false));
            Assert.Throws<ArgumentNullException>(() => testString.ToPascalCase());
        }

        [Test]
        public void ToCamelCase_WithSingleWord_ReturnsSingleWord()
        {
            string testString = "asdf";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(testString, result);
        }

        [Test]
        public void ToPascalCase_WithSingleWord_ReturnsSingleWordWithFirstCharUpperCase()
        {
            string testString = "asdf";
            string expected = "Asdf";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToCamelCase_WithSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with five dozen liquor jugs";
            string expectedResult = "packMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToPascalCase_WithSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with five dozen liquor jugs";
            string expectedResult = "PackMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToCamelCase_WithNumbersInSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with 54 dozen liquor jugs";
            string expectedResult = "packMyBoxWith54DozenLiquorJugs";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToPascalCase_WithNumbersInSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with 54 dozen liquor jugs";
            string expectedResult = "PackMyBoxWith54DozenLiquorJugs";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromCamelCase_WithNullString_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.FromCamelAndPascalCase(false));
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
            string expectedResult = "Asdf";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromCamelCase_WithSentenceAndLowercaseWords_ReturnsSentence()
        {
            string testString = "packMyBoxWithFiveDozenLiquorJugs";
            string expectedResult = "pack my box with five dozen liquor jugs";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromPascalCase_WithSentenceAndUppercaseWords_ReturnsSentence()
        {
            string testString = "PackMyBoxWithFiveDozenLiquorJugs";
            string expectedResult = "Pack My Box With Five Dozen Liquor Jugs";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromCamelCase_WithNumbersInSentenceAndLowercaseWords_ReturnsSentence()
        {
            string testString = "packMyBoxWith54DozenLiquorJugs";
            string expectedResult = "pack my box with 54 dozen liquor jugs";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromPascalCase_WithNumbersInSentenceAndUppercaseWords_ReturnsSentence()
        {
            string testString = "PackMyBoxWith54DozenLiquorJugs";
            string expectedResult = "Pack My Box With 54 Dozen Liquor Jugs";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromCamelCase_WithAcronym_ReturnsSentence()
        {
            string testString = "thisIsAnIDForAUser";
            string expectedResult = "this is an ID for a user";

            string result = testString.FromCamelAndPascalCase(false);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FromPascalCase_WithAcronym_ReturnsSentence()
        {
            string testString = "thisIsAnIDForAUser";
            string expectedResult = "This Is An ID For A User";

            string result = testString.FromCamelAndPascalCase(true);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
