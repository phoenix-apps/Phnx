using NUnit.Framework;
using System;
using System.Text;

namespace Phnx.IO.Tests.PipeStreamTests
{
    public class NewTests
    {
        [Test]
        public void NewPipe_CreatesEmptyPipe()
        {
            var pipe = new PipeStream();

            Assert.AreEqual(0, pipe.Length);
        }

        [Test]
        public void NewPipe_WithInitialStringData_WritesDataToPipe()
        {
            var expected = "asdf";
            var pipe = new PipeStream(expected);

            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }

        [Test]
        public void NewPipe_WithNullInitialBytesData_ThrowsArgumentNullException()
        {
            byte[] data = null;

            Assert.Throws<ArgumentNullException>(() => new PipeStream(data));
        }

        [Test]
        public void NewPipe_WithInitialBytesData_WritesDataToPipe()
        {
            var expected = new byte[] { 123, 1, 36, 35, 1 };

            var pipe = new PipeStream(expected);

            Assert.AreEqual(expected.Length, pipe.Length);
            Assert.AreEqual(expected, pipe.ReadToEnd());
        }

        [Test]
        public void NewPipe_WithOffsetAndCountAndNullInitialBytesData_ThrowsArgumentNullException()
        {
            byte[] data = null;

            Assert.Throws<ArgumentNullException>(() => new PipeStream(data, 0, 0));
        }

        [Test]
        public void NewPipe_WithOffsetLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var expected = new byte[] { 123, 1, 36, 35, 1 };

            Assert.Throws<ArgumentOutOfRangeException>(() => new PipeStream(expected, -1, 0));
        }

        [Test]
        public void NewPipe_WithCountLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var expected = new byte[] { 123, 1, 36, 35, 1 };

            Assert.Throws<ArgumentOutOfRangeException>(() => new PipeStream(expected, 0, -1));
        }

        [Test]
        public void NewPipe_WithOffsetAndCountCombinedGreaterThanBufferSize_ThrowsArgumentException()
        {
            var expected = new byte[] { 123, 1, 36, 35, 1 };

            Assert.Throws<ArgumentException>(() => new PipeStream(expected, 2, 4));
        }

        [Test]
        public void NewPipe_WithOffsetAndCountOfZero_WritesNoDate()
        {
            var expected = new byte[] { 123 };

            var pipe = new PipeStream(expected, 0, 0);

            Assert.AreEqual(0, pipe.Length);
        }

        [Test]
        public void NewPipe_WithOffsetAndCountAndInitialBytesData_WritesDataToPipe()
        {
            var expected = new byte[] { 1, 36 };
            var range = new byte[] { 123, 1, 36, 35, 1 };

            var pipe = new PipeStream(range, 1, 2);

            Assert.AreEqual(expected.Length, pipe.Length);
            Assert.AreEqual(expected, pipe.ReadToEnd());
        }

        [Test]
        public void NewPipe_WithEncoding_WritesPreamble()
        {
            var encoding = Encoding.UTF32;
            var expected = encoding.GetPreamble();

            var pipe = new PipeStream(encoding);

            Assert.AreEqual(expected, pipe.ReadToEnd());
        }

        [Test]
        public void NewPipe_WithEncodingAndNullInitialDataString_WritesPreamble()
        {
            var encoding = Encoding.UTF8;
            string testText = null;
            var expected = encoding.GetPreamble();

            var pipe = new PipeStream(testText, encoding);

            Assert.AreEqual(expected, pipe.ReadToEnd());
        }

        [Test]
        public void NewPipe_WithEncodingAndInitialDataString_WritesPreambleAndData()
        {
            var encoding = Encoding.BigEndianUnicode;
            var testText = "asdf";
            var preamble = encoding.GetPreamble();
            var testTextBytes = encoding.GetBytes(testText);
            var expected = new byte[preamble.Length + testTextBytes.Length];

            for (int index = 0; index < preamble.Length; index++)
                expected[index] = preamble[index];
            for (int index = preamble.Length; index < expected.Length; index++)
                expected[index] = testTextBytes[index - preamble.Length];

            var pipe = new PipeStream(testText, encoding);

            Assert.AreEqual(expected, pipe.ReadToEnd());
        }
    }
}
