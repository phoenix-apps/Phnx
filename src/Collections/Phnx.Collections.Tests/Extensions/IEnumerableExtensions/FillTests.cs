using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableExtensions
{
    public class FillTests
    {
        [Test]
        public void Fill_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<string> values = null;

            Assert.Throws<ArgumentNullException>(() => values.Fill(string.Empty).ToList());
        }

        [Test]
        public void Fill_WithNull_DoesNotThrow()
        {
            IEnumerable<string> values = new List<string>
            {
            };

            Assert.DoesNotThrow(() => values.Fill(null).ToList());
        }

        [Test]
        public void Fill_WithValue_Fills()
        {
            var expected = new List<int> { 1, 1, 1, 1, 1 };

            IEnumerable<int> items = new List<int>
            {
                1, 161, 3, 36, -13
            };

            var result = items.Fill(1);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
