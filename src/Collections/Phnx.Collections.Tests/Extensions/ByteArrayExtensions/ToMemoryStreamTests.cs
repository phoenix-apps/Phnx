using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ByteArrayExtensions
{
    public class ToMemoryStreamTests
    {
        [Test]
        public void ToMemoryStream_WhenByteArrayIsNull_ThrowsArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => bytes.ToMemoryStream());
        }

        [Test]
        public void ToMemoryStream_WithEmptyArray_ReturnsEmptyMemoryStream()
        {
            byte[] bytes = new byte[0];

            var memoryStream = bytes.ToMemoryStream();

            Assert.AreEqual(0, memoryStream.Length);
        }

        [Test]
        public void ToMemoryStream_With5Bytes_FillsStreamWithBytes()
        {
            var bytes = new byte[] { 1, 64, 126, 25, 232 };

            var memoryStream = bytes.ToMemoryStream();

            Assert.AreEqual(5, memoryStream.Length);

            byte[] read = new byte[memoryStream.Length];
            for (int index = 0; index < read.Length; ++index)
            {
                read[index] = (byte)memoryStream.ReadByte();
            }

            CollectionAssert.AreEqual(bytes, read);
        }
    }
}
