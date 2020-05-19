using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.IO.Tests.Extensions.AsyncStreamExtensionsTests
{
    public class WriteAsyncTests
    {
        [Test]
        public void WriteAsync_WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => s.WriteAsync("test"));
        }

        [Test]
        public async Task WriteAsync_WhenTextIsNull_WritesNothing()
        {
            var pipe = new PipeStream();
            string input = null;

            await pipe.WriteAsync(input);

            Assert.AreEqual(0, pipe.Length);
        }

        [Test]
        public async Task WriteAsync_WhenTextHasContent_WritesText()
        {
            string expected = "asdf";
            var pipe = new PipeStream();

            await pipe.WriteAsync(expected);

            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }

        [Test]
        public void WriteAsyncWithEncoding_WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => s.WriteAsync("test", Encoding.UTF32));
        }

        [Test]
        public async Task WriteAsyncWithEncoding_WhenTextIsNull_WritesPreamble()
        {
            var pipe = new PipeStream();

            await pipe.WriteAsync(null, Encoding.UTF32);

            Assert.AreEqual(Encoding.UTF32.GetPreamble().Length, pipe.Length);
        }

        [Test]
        public async Task WriteAsyncWithEncoding_WhenTextHasContent_WritesPreambleAndText()
        {
            string expected = "asdf";
            var pipe = new PipeStream();

            await pipe.WriteAsync(expected, Encoding.UTF32);

            int expectedLength = Encoding.UTF32.GetPreamble().Length + Encoding.UTF32.GetByteCount(expected);
            Assert.AreEqual(expectedLength, pipe.Length);
            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }
    }
}
