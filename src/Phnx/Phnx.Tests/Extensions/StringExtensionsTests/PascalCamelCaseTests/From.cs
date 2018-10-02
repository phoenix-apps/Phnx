using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.StringExtensionsTests.PascalCamelCaseTests
{
    public class From
    {
        [Test]
        public void ToCamelCase_WithNullString_ThrowsArgumentNullException()
        {
            string testString = null;

            Assert.Throws<ArgumentNullException>(() => testString.ToCamelCase(false));
            Assert.Throws<ArgumentNullException>(() => testString.ToPascalCase());
        }

        [Test]
        public void ToCamelCase_WithEmptyString_ReturnsEmptyString()
        {
            string testString = string.Empty;

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(testString, result);

            result = testString.ToPascalCase();

            Assert.AreEqual(testString, result);
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
            string expected = "packMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToPascalCase_WithSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with five dozen liquor jugs";
            string expected = "PackMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToCamelCase_WithSentenceContainingDoubleSpace_IgnoresDoubleSpaceAndReturnsCamelCase()
        {
            string testString = "Pack  my  box  with  five  dozen  liquor  jugs";
            string expected = "packMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToPascalCase_WithSentenceContainingDoubleSpace_IgnoresDoubleSpaceAndReturnsPascalCase()
        {
            string testString = "Pack  my  box  with  five  dozen  liquor  jugs";
            string expected = "PackMyBoxWithFiveDozenLiquorJugs";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToCamelCase_WithNumbersInSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with 54 dozen liquor jugs";
            string expected = "packMyBoxWith54DozenLiquorJugs";

            string result = testString.ToCamelCase(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToPascalCase_WithNumbersInSentence_ReturnsCamelCase()
        {
            string testString = "Pack my box with 54 dozen liquor jugs";
            string expected = "PackMyBoxWith54DozenLiquorJugs";

            string result = testString.ToPascalCase();

            Assert.AreEqual(expected, result);
        }
    }
}
