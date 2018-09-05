using MarkSFrancis.Console.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarkSFrancis.Console.Tests.TextIoTests
{
    public class InputTests
    {
        [Test]
        public void GetString_WhenNoTextIsWritten_ReturnsNull()
        {
            var textIo = Factory.TextIo(out var from, out var to);

            var text = textIo.GetString();

            Assert.AreEqual(null, text);
        }

        [Test]
        public void GetString_WhenDataIsWritten_GetsSingleLine()
        {
            var firstLine = "test <>!dshg@ Data";
            var secondLine = "TrIBUTE2 2";

            var textIo = Factory.TextIo(out var from, out var to);
            to.Write(firstLine + Environment.NewLine + secondLine);

            var result = textIo.GetString();

            Assert.AreEqual(firstLine, result);
        }

        [Test]
        public void GetStringAfterEnd_WhenDataIsWritten_ReturnsNull()
        {
            var firstLine = "test <>!dshg@ Data";
            var secondLine = "TrIBUTE2 2";

            var textIo = Factory.TextIo(out var from, out var to);
            to.Write(firstLine + Environment.NewLine + secondLine);

            textIo.GetString();
            textIo.GetString();

            var result = textIo.GetString();

            Assert.AreEqual(null, result);
        }

        [Test]
        public void GetInt_WhenDataIsValid_GetsInt()
        {
            var data = 1256;

            var textIo = Factory.TextIo(out var from, out var to);
            to.Write(data);

            var result = textIo.GetInt();

            Assert.AreEqual(1256, result);
        }

        [Test]
        public void GetSelection_WhenOptionsIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<string> options = null;

            var textIo = Factory.TextIo(out var from, out var to);

            Assert.Throws<ArgumentNullException>(() => textIo.GetSelection(options));
        }

        [Test]
        public void GetSelection_WhenDataIsValid_GetsSelectionIndex()
        {
            var options = new List<string>
            {
                "Option 1",
                "Option 2"
            };

            var textIo = Factory.TextIo(out var from, out var to);
            to.WriteLine("2");

            var selectedOption = textIo.GetSelection(options);

            Assert.AreEqual(1, selectedOption);
        }

        [Test]
        public void GetSelection_WhenDataIsOutOfRange_GetsSelectionIndex()
        {
            var options = new List<string>
            {
                "Option 1",
                "Option 2"
            };

            var textIo = Factory.TextIo(out var from, out var to);
            to.WriteLine("4");

            Assert.Throws<InvalidCastException>(() => textIo.GetSelection(options));
        }

        [Test]
        public void GetLong_WhenDataIsValid_GetsLong()
        {
            const long expected = long.MaxValue - 10;
            var textIo = Factory.TextIo(out var from, out var to);
            to.WriteLine(expected);

            var result = textIo.GetLong();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetDecimal_WhenDataIsValid_GetsLong()
        {
            const decimal expected = decimal.MaxValue - 10;
            var textIo = Factory.TextIo(out var from, out var to);
            to.WriteLine(expected);

            var result = textIo.GetDecimal();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetDateTime_WhenDataIsValid_GetsLong()
        {
            DateTime expected = new DateTime(1925, 12, 12, 1, 53, 12);
            var textIo = Factory.TextIo(out var from, out var to);
            to.WriteLine(expected);

            var result = textIo.GetDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Get_WithNullConverter_ThrowsArgumentNullException()
        {
            var textIo = Factory.TextIo(out var from, out var to);

            Assert.Throws<ArgumentNullException>(() => textIo.Get<object>(null));
        }

        [Test]
        public void Get_WithEmptyInput_ThrowsEndOfStreamException()
        {
            var textIo = Factory.TextIo(out var from, out var to);

            Assert.Throws<EndOfStreamException>(() => textIo.Get(data => data));
        }

        [Test]
        public void Get_WithBrokenConverter_ThrowsInvalidCastException()
        {
            object brokenConverter(string data) => throw new NotImplementedException();

            var textIo = Factory.TextIo(out var from, out var to);
            to.Write("ignore");

            Assert.Throws<InvalidCastException>(() => textIo.Get(brokenConverter));
        }
    }
}
