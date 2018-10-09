using NUnit.Framework;
using System;
using System.IO;

namespace Phnx.IO.Tests
{
    public class StreamExtensionsTests
    {
        [Test]
        public void ReadToEnd_WithNullStream_ThrowsArgumentNullException()
        {
            Stream s = null;

            Assert.Throws<ArgumentNullException>(() => s.ReadToEnd());
        }

        [Test]
        public void ReadToEnd_WithContent_ReadsToEnd()
        {

        }
    }
}
