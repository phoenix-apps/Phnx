using Phnx.Console.Tests.TestHelpers;
using NUnit.Framework;

namespace Phnx.Console.Tests.TextIoTests
{
    public class ReadWriteComboTests
    {
        [Test]
        public void GetString_WhenQuestionIsNull_WritesNothing()
        {
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.GetString(null);

            Assert.AreEqual(string.Empty, from.ReadToEnd());
        }

        [Test]
        public void GetString_WhenQuestionIsNotNull_WritesQuestion()
        {
            string expected = "Test";
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.GetString(expected);

            Assert.AreEqual(expected, from.ReadToEnd());
        }

        [Test]
        public void Get_WithNullQuestion_WritesNothing()
        {
            string expected = null;
            var textIo = Factory.TextIo(out var from, out var to);

            to.WriteLine("ignore");

            textIo.Get(data => data, expected);

            var result = from.ReadLine();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Get_WithQuestion_WritesQuestion()
        {
            string expected = "a test";
            var textIo = Factory.TextIo(out var from, out var to);

            to.WriteLine("ignore");

            textIo.Get(data => data, expected);

            var result = from.ReadLine();

            Assert.AreEqual(expected, result);
        }
    }
}
