using NUnit.Framework;

namespace MarkSFrancis.Tests.ConverterHelperTests
{
    public class IntStringConverterTests
    {
        [Test]
        public void GettingConverter_FromStringToInt_IsNotNull()
        {
            var converter = ConverterHelpers.GetDefaultConverter<string, int>();

            Assert.IsNotNull(converter);
        }

        [Test]
        public void GettingConverter_FromIntToString_IsNotNull()
        {
            var converter = ConverterHelpers.GetDefaultConverter<int, string>();

            Assert.IsNotNull(converter);
        }

        [Test]
        public void ConvertingFromStringToInt_WithValidString_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<string, int>();

            int resultShouldBe = 123;
            var result = converter(resultShouldBe.ToString());

            Assert.AreEqual(resultShouldBe, result);
        }

        [Test]
        public void Converting_FromIntToString_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<int, string>();

            int resultShouldBe = 123;
            var result = converter(resultShouldBe);

            Assert.AreEqual(resultShouldBe.ToString(), result);
        }
    }
}
