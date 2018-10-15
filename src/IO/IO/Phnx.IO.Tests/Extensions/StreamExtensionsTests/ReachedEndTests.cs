using NUnit.Framework;
using System;
using System.IO;

namespace Phnx.IO.Tests.Extensions.StreamExtensionsTests
{
    public class ReachedEndTests
    {
        [Test]
        public void ReachedEnd_WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream s = null;
            Assert.Throws<ArgumentNullException>(() => s.ReachedEnd());
        }

        [Test]
        public void ReachedEnd_WhenStreamAtStartWithData_ReturnsFalse()
        {
            PipeStream pipe = new PipeStream(new byte[2]);

            var result = pipe.ReachedEnd();

            Assert.IsFalse(result);
        }

        [Test]
        public void ReachedEnd_WhenStreamInMiddleWithData_ReturnsFalse()
        {
            PipeStream pipe = new PipeStream(new byte[2]);
            pipe.ReadByte();

            var result = pipe.ReachedEnd();

            Assert.IsFalse(result);
        }

        [Test]
        public void ReachedEnd_WhenStreamAtEndWithData_ReturnsTrue()
        {
            PipeStream pipe = new PipeStream(new byte[2]);
            pipe.ReadByte();
            pipe.ReadByte();

            var result = pipe.ReachedEnd();

            Assert.IsTrue(result);
        }

        [Test]
        public void ReachedEnd_WhenStreamEmpty_ReturnsTrue()
        {
            PipeStream pipe = new PipeStream();

            var result = pipe.ReachedEnd();

            Assert.IsTrue(result);
        }

        [Test]
        public void ReachedEndTextReader_WhenIsNull_ThrowsArgumentNullException()
        {
            TextReader reader = null;
            Assert.Throws<ArgumentNullException>(() => reader.ReachedEnd());
        }

        [Test]
        public void ReachedEndTextReader_WhenAtStartWithData_ReturnsFalse()
        {
            PipeStream pipe = new PipeStream(new byte[2]);

            var result = pipe.Out.ReachedEnd();

            Assert.IsFalse(result);
        }

        [Test]
        public void ReachedEndTextReader_WhenInMiddleWithData_ReturnsFalse()
        {
            PipeStream pipe = new PipeStream(new byte[2]);
            pipe.ReadByte();

            var result = pipe.Out.ReachedEnd();

            Assert.IsFalse(result);
        }

        [Test]
        public void ReachedEndTextReader_WhenAtEndWithData_ReturnsTrue()
        {
            PipeStream pipe = new PipeStream(new byte[2]);
            pipe.ReadByte();
            pipe.ReadByte();

            var result = pipe.Out.ReachedEnd();

            Assert.IsTrue(result);
        }

        [Test]
        public void ReachedEndTextReader_WhenEmpty_ReturnsTrue()
        {
            PipeStream pipe = new PipeStream();

            var result = pipe.Out.ReachedEnd();

            Assert.IsTrue(result);
        }
    }
}
