using NUnit.Framework;
using System;

namespace Phnx.IO.Tests.PipeStreamTests
{
    public class Other
    {
        [Test]
        public void CanRead_IsTrue()
        {
            var pipe = new PipeStream();

            Assert.IsTrue(pipe.CanRead);
        }

        [Test]
        public void CanWrite_IsTrue()
        {
            var pipe = new PipeStream();

            Assert.IsTrue(pipe.CanWrite);
        }

        [Test]
        public void CanSeek_IsFalse()
        {
            var pipe = new PipeStream();

            Assert.IsFalse(pipe.CanSeek);
        }

        [Test]
        public void GetPosition_BeforeRead_IsZero()
        {
            var pipe = new PipeStream();

            Assert.AreEqual(0, pipe.Position);
        }

        [Test]
        public void GetPosition_AfterRead_IsZero()
        {
            var pipe = new PipeStream(new byte[] { 1, 2 });

            pipe.ReadByte();

            Assert.AreEqual(0, pipe.Position);
        }

        [Test]
        public void SetPosition_ThrowsNotSupportedException()
        {
            var pipe = new PipeStream();

            Assert.Throws<NotSupportedException>(() => pipe.Position = 0);
        }

        [Test]
        public void Seek_ThrowsNotSupportedException()
        {
            var pipe = new PipeStream();

            Assert.Throws<NotSupportedException>(() => pipe.Seek(0, System.IO.SeekOrigin.Begin));
        }

        [Test]
        public void SetLength_ThrowsNotSupportedException()
        {
            var pipe = new PipeStream();

            Assert.Throws<NotSupportedException>(() => pipe.SetLength(0));
        }
    }
}
