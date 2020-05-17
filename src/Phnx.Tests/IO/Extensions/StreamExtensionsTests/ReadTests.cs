using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace Phnx.IO.Tests.Extensions.StreamExtensionsTests
{
    public class ReadTests
    {
        [Test]
        public void ReadToEnd_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.ReadToEnd());
        }

        [Test]
        public void ReadToEnd_FromStart_WithContent_ReadsToEnd()
        {
            byte[] expected = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(expected);

            var result = s.ReadToEnd();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEnd_FromMiddle_WithContent_ReadsToEnd()
        {
            byte[] expected = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(expected);

            var result = s.ReadToEnd();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEnd_FromEnd_WithContent_ReadsEmptyArray()
        {
            byte[] expected = new byte[] { 61 };
            byte[] testBytes = new byte[] { 25, 251, 61 };
            Stream s = new MemoryStream(testBytes);
            s.ReadByte();
            s.ReadByte();

            var result = s.ReadToEnd();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsString_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.ReadToEndAsString());
        }

        [Test]
        public void ReadToEndAsString_FromStart_WithContent_ReadsToEnd()
        {
            string expected = "asdf";
            Stream s = new PipeStream(expected);

            var result = s.ReadToEndAsString();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsString_FromMiddle_WithContent_ReadsToEnd()
        {
            string expected = "df";
            string test = "asdf";

            var s = new PipeStream(test);
            s.ReadByte();
            s.ReadByte();

            var result = s.ReadToEndAsString();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsString_FromEnd_WithContent_ReadsEmptyString()
        {
            string expected = string.Empty;
            string test = "asdf";

            PipeStream pipe = new PipeStream(test);
            pipe.ReadByte();
            pipe.ReadByte();
            pipe.ReadByte();
            pipe.ReadByte();

            var result = pipe.ReadToEndAsString();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringWithEncoding_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.ReadToEndAsString(Encoding.ASCII));
        }

        [Test]
        public void ReadToEndAsString_WithPreambledContent_DetectsEncodingAndReadsToEnd()
        {
            string expected = "asdf";

            var pipe = new PipeStream(expected, Encoding.UTF32);

            var result = pipe.ReadToEndAsString();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringWithEncoding_FromStart_WithContent_ReadsToEnd()
        {
            string expected = "asdf";

            var pipe = new PipeStream(expected, Encoding.UTF32);

            var result = pipe.ReadToEndAsString(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringWithEncoding_FromMiddle_WithContent_ReadsToEnd()
        {
            string expected = "df";
            string test = "asdf";

            var pipe = new PipeStream(test, Encoding.UTF32);
            pipe.Read(new byte[4]); // Skip preamble

            string textToSkip = test.Substring(0, test.Length - expected.Length);
            int skipByteCount = Encoding.UTF32.GetByteCount(textToSkip);
            pipe.Read(new byte[skipByteCount]); // Read text

            var result = pipe.ReadToEndAsString(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadToEndAsStringWithEncoding_FromEnd_WithContent_ReadsEmptyString()
        {
            string expected = string.Empty;
            string test = "asdf";

            PipeStream pipe = new PipeStream(test, Encoding.UTF32);
            pipe.Read(new byte[4]); // Skip preamble

            string textToSkip = test.Substring(0, test.Length - expected.Length);
            int skipByteCount = Encoding.UTF32.GetByteCount(textToSkip);
            pipe.Read(new byte[skipByteCount]); // Read text

            var result = pipe.ReadToEndAsString(Encoding.UTF32);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
