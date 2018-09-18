using NUnit.Framework;

namespace MarkSFrancis.Tests
{
    public class LazyLoadTests
    {
        [Test]
        public void Converter_GetsValue()
        {
            const string expected = "asdf";
            var lazy = new LazyLoad<string>(() => expected);

            string result = lazy;

            Assert.AreEqual(expected, result);
        }
    }
}
