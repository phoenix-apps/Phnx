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
            Assert.Throws<ArgumentNullException>(() => new TextIo(null, new TailedStream().Head));
        }

        [Test]
        public void CreatingTextIo_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TextIo(new TailedStream().Tail, null));
        }
    }
}
