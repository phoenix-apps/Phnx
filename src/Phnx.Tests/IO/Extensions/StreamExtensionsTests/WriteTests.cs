using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace Phnx.IO.Tests.Extensions.StreamExtensionsTests
{
    public class WriteTests
    {
        [Test]
        public void Write_WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.Write("test"));
        }

        [Test]
        public void Write_WhenTextIsNull_WritesNothing()
        {
            var pipe = new PipeStream();

            pipe.Write(null);

            Assert.AreEqual(0, pipe.Length);
        }

        [Test]
        public void Write_WhenTextHasContent_WritesText()
        {
            string expected = "asdf";
            var pipe = new PipeStream();

            pipe.Write(expected);

            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }

        [Test]
        public void WriteWithEncoding_WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.Write("test", Encoding.UTF32));
        }

        [Test]
        public void WriteWithEncoding_WhenTextIsNull_WritesPreamble()
        {
            var pipe = new PipeStream();

            pipe.Write(null, Encoding.UTF32);

            Assert.AreEqual(Encoding.UTF32.GetPreamble().Length, pipe.Length);
        }

        [Test]
        public void WriteWithEncoding_WhenTextHasContent_WritesPreambleAndText()
        {
            string expected = "asdf";
            var pipe = new PipeStream();

            pipe.Write(expected, Encoding.UTF32);

            int expectedLength = Encoding.UTF32.GetPreamble().Length + Encoding.UTF32.GetByteCount(expected);
            Assert.AreEqual(expectedLength, pipe.Length);
            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }
    }
}
