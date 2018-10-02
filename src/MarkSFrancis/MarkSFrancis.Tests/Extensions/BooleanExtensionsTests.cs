using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions
{
    public class BooleanExtensionsTests
    {
        [Test]
        public void ConvertingTrueToString_WithEmojisInString_GetsStrings()
        {
            var trueText = "This 😍 i👌s ju🥧st 😂 a test!_~\U0001f923";
            string falseText = null;

            var result = true.ToString(trueText, falseText);

            Assert.AreEqual(trueText, result);
        }

        [Test]
        public void ConvertingFalseToString_WhenStringIsNull_GetsString()
        {
            var trueText = "This 😍 i👌s ju🥧st 😂 a test!_~\U0001f923";
            string falseText = null;

            var result = false.ToString(trueText, falseText);

            Assert.AreEqual(falseText, result);
        }
    }
}
