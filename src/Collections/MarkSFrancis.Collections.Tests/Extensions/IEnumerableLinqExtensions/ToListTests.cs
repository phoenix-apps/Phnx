using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class ToListTests
    {
        [Test]
        public void ToList_WhenListIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.ToList(0));
        }


    }
}
