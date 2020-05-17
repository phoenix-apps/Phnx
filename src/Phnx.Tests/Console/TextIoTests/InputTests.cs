using NUnit.Framework;
using Phnx.Console.Tests.TestHelpers;
using System;

namespace Phnx.Console.Tests.TextIoTests
{
    public class InputTests
    {
        [Test]
        public void ReadLine_WhenNoTextIsWritten_ReturnsNull()
        {
            var textIo = Factory.TextIo(out _, out _);

            var text = textIo.ReadLine();

            Assert.AreEqual(null, text);
        }

        [Test]
        public void ReadLine_WhenDataIsWritten_GetsSingleLine()
        {
            var firstLine = "test <>!dshg@ Data";
            var secondLine = "TrIBUTE2 2";

            var textIo = Factory.TextIo(out _, out var to);
            to.Write(firstLine + Environment.NewLine + secondLine);

            var result = textIo.ReadLine();

            Assert.AreEqual(firstLine, result);
        }

        [Test]
        public void ReadLine_WhenAtEndOfData_ReturnsNull()
        {
            var firstLine = "test <>!dshg@ Data";
            var secondLine = "TrIBUTE2 2";

            var textIo = Factory.TextIo(out _, out var to);
            to.Write(firstLine + Environment.NewLine + secondLine);

            textIo.ReadLine();
            textIo.ReadLine();

            var result = textIo.ReadLine();

            Assert.AreEqual(null, result);
        }
    }
}
