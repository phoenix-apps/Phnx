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
    }
}
