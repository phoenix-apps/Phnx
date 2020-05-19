using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.IO.Tests.Extensions.AsyncStreamExtensionsTests
{
    public class ReadAsyncTests
    {
        [Test]
        public void ReadToEndAsync_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => s.ReadToEndAsync());
        }

        [Test]
        public async Task ReadToEndAsync_FromStart_WithContent_ReadsToEnd()
        {
            byte[] expected = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(expected);

            var result = await s.ReadToEndAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsync_FromMiddle_WithContent_ReadsToEnd()
        {
            byte[] expected = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(expected);

            var result = await s.ReadToEndAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsync_FromEnd_WithContent_ReadsEmptyArray()
        {
            byte[] expected = new byte[] { 61 };
            byte[] testBytes = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(testBytes);
            s.ReadByte();
            s.ReadByte();

            var result = await s.ReadToEndAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringAsync_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => s.ReadToEndAsStringAsync());
        }

        [Test]
        public async Task ReadToEndAsStringAsync_FromStart_WithContent_ReadsToEnd()
        {
            string expected = "asdf";
            Stream s = new PipeStream(expected);

            var result = await s.ReadToEndAsStringAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsStringAsync_FromMiddle_WithContent_ReadsToEnd()
        {
            string expected = "df";
            string test = "asdf";

            var s = new PipeStream(test);
            s.ReadByte();
            s.ReadByte();

            var result = await s.ReadToEndAsStringAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsStringAsync_FromEnd_WithContent_ReadsEmptyString()
        {
            string expected = string.Empty;
            string test = "asdf";

            PipeStream pipe = new PipeStream(test);
            pipe.ReadByte();
            pipe.ReadByte();
            pipe.ReadByte();
            pipe.ReadByte();

            var result = await pipe.ReadToEndAsStringAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringAsyncWithEncoding_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => s.ReadToEndAsStringAsync(Encoding.ASCII));
        }

        [Test]
        public async Task ReadToEndAsStringAsync_WithPreambledContent_DetectsEncodingAndReadsToEnd()
        {
            string expected = "asdf";

            var pipe = new PipeStream(expected, Encoding.UTF32);

            var result = await pipe.ReadToEndAsStringAsync();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsStringAsyncWithEncoding_FromStart_WithContent_ReadsToEnd()
        {
            string expected = "asdf";

            var pipe = new PipeStream(expected, Encoding.UTF32);

            var result = await pipe.ReadToEndAsStringAsync(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsStringAsyncWithEncoding_FromMiddle_WithContent_ReadsToEnd()
        {
            string expected = "df";
            string test = "asdf";

            var pipe = new PipeStream(test, Encoding.UTF32);
            pipe.Read(new byte[4]); // Skip preamble

            string textToSkip = test.Substring(0, test.Length - expected.Length);
            int skipByteCount = Encoding.UTF32.GetByteCount(textToSkip);
            pipe.Read(new byte[skipByteCount]); // Read text

            var result = await pipe.ReadToEndAsStringAsync(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ReadToEndAsStringAsyncWithEncoding_FromEnd_WithContent_ReadsEmptyString()
        {
            string expected = string.Empty;
            string test = "asdf";

            PipeStream pipe = new PipeStream(test, Encoding.UTF32);
            pipe.Read(new byte[4]); // Skip preamble

            string textToSkip = test.Substring(0, test.Length - expected.Length);
            int skipByteCount = Encoding.UTF32.GetByteCount(textToSkip);
            pipe.Read(new byte[skipByteCount]); // Read text

            var result = await pipe.ReadToEndAsStringAsync(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
