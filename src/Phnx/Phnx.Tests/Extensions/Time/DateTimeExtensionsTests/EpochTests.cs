using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class EpochTests
    {
        [Test]
        public void Epoch_Is1970_1_1()
        {
            Assert.AreEqual(new DateTime(1970, 1, 1), DateTimeExtensions.Epoch);
        }
    }
}
