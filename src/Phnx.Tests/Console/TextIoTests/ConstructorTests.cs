using Phnx.Console.Tests.TestHelpers;
using NUnit.Framework;
using System;

namespace Phnx.Console.Tests.TextIoTests
{
    public class ConstructorTests
    {
        [Test]
        public void CreatingTextIo_WhenInputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TextIoHelper(null, new PipeStream().Head));
        }

        [Test]
        public void CreatingTextIo_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TextIoHelper(new PipeStream().Tail, null));
        }
    }
}
