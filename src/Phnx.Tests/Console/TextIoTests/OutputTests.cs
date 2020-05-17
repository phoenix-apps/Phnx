using Phnx.Console.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Console.Tests
{
    public class OutputTests
    {
        [Test]
        public void WriteLine_WithOneRow_WritesSingleRow()
        {
            var shouldWrite = "test <>!dshg@ Data";
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.WriteLine(shouldWrite);

            Assert.AreEqual(shouldWrite + Environment.NewLine, from.ReadToEnd());
        }

        [Test]
        public void WriteLine_WithTwoRows_WritesTwoRows()
        {
            var shouldWrite = $"test <>!dshg@ Data{Environment.NewLine}TrIBUTE2 2";
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.WriteLine(shouldWrite);

            Assert.AreEqual(shouldWrite + Environment.NewLine, from.ReadToEnd());
        }

        [Test]
        public void WriteLine_WithNullObject_WritesEmptyRow()
        {
            object shouldWrite = null;
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.WriteLine(shouldWrite);

            Assert.AreEqual(Environment.NewLine, from.ReadToEnd());
        }

        [Test]
        public void Write_WritesString()
        {
            var shouldWrite = $"test <>!dshg@ Data{Environment.NewLine}TrIBUTE2 2";
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.Write(shouldWrite);

            Assert.AreEqual(shouldWrite, from.ReadToEnd());
        }

        [Test]
        public void Write_WithNullObject_WritesNothing()
        {
            object shouldWrite = null;
            var textIo = Factory.TextIo(out var from, out var to);

            textIo.Write(shouldWrite);

            Assert.AreEqual(string.Empty, from.ReadToEnd());
        }

        [Test]
        public void WriteCollection_WithCustomToString_WritesCollection()
        {
            string expected = "1, 2, 3, 4, 5";
            var collection = new List<object>
            {
                null, null, null, null, null
            };

            var textIo = Factory.TextIo(out var from, out var to);

            int itemNumber = 0;
            textIo.WriteCollection(collection, ", ", o => (++itemNumber).ToString());

            Assert.AreEqual(expected, from.ReadToEnd());
        }

        [Test]
        public void WriteCollection_WithoutCustomToString_UsesDefaultToString()
        {
            string expected = "1265, 12, 2763, 126, 8765";
            var collection = new List<int>
            {
                1265, 12, 2763, 126, 8765
            };

            var textIo = Factory.TextIo(out var from, out var to);

            textIo.WriteCollection(collection, ", ");

            Assert.AreEqual(expected, from.ReadToEnd());
        }
    }
}
