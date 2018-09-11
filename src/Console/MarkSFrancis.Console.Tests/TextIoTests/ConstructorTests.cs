using MarkSFrancis.Console.Tests.TestHelpers;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Console.Tests.TextIoTests
{
    public class ConstructorTests
    {
        [Test]
        public void CreatingTextIo_WhenInputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TextIoHelper(null, new TailedStream().Head));
        }

        [Test]
        public void CreatingTextIo_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TextIoHelper(new TailedStream().Tail, null));
        }
    }
}
