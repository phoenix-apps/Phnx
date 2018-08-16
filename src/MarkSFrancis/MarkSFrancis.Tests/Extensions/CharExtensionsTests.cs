using MarkSFrancis.Extensions;
using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions
{
    public class CharExtensionsTests
    {
        [Test]
        public void CharToString_With0Characters_GetsEmptyString()
        {
            char baseChar = 'a';
            string result = baseChar.ToString(0);

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void CharToString_With50Characters_GetStringWithLength50()
        {
            char baseChar = '&';
            string result = baseChar.ToString(50);

            Assert.AreEqual(50, result.Length);
        }

        [Test]
        public void CharToString_With5Characters_GetsStringWithCharRepeated5Times()
        {
            char baseChar = '6';
            string resultShouldBe = "66666";

            string result = baseChar.ToString(5);

            Assert.AreEqual(resultShouldBe, result);
        }
    }
}
