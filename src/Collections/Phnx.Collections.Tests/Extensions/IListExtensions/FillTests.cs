using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions.IListExtensions
{
    public class FillTests
    {
        [Test]
        public void Fill_NullCollection_ThrowsArgumentNullException()
        {
            IList<string> values = null;

            Assert.Throws<ArgumentNullException>(() => values.Fill(string.Empty).ToList());
        }

        [Test]
        public void Fill_WithNull_DoesNotThrow()
        {
            IList<string> values = new List<string>
            {
            };

            Assert.DoesNotThrow(() => values.Fill(null).ToList());
        }

        [Test]
        public void Fill_WithValue_Fills()
        {
            var expected = new List<int> { 1, 1, 1, 1, 1 };

            IList<int> items = new List<int>
            {
                1, 161, 3, 36, -13
            };

            var result = items.Fill(1);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
