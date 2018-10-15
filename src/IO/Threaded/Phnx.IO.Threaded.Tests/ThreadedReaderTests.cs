using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Phnx.IO.Threaded.Tests
{
    public class ThreadedReaderTests
    {
        [Test]
        public void New_WithNullReadFunc_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ThreadedReader<object>(null));
        }

        [Test]
        public void ReadFromList_WithValidEntries_ReturnsFirstValue()
        {
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3" };
            string expectedResult = values.First();

            var index = 0;
            string value;
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[index++]))
            {
                value = reader.Read();
            }

            Assert.AreEqual(expectedResult, value);
        }

        [Test]
        public void Read_WhenUsingLookAhead_ReturnsIndexedValue()
        {
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            string[] results = new string[values.Count];

            int index = 0;
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[index++], 20))
            {
                for (int followerIndex = 0; followerIndex < values.Count; followerIndex++)
                {
                    results[followerIndex] = reader.Read();
                }
            }

            CollectionAssert.AreEqual(values, results);
        }

        [Test]
        public void Read_WhenReadThrows_Rethrows()
        {
            string[] values = new string[0];

            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[0], 5))
            {
                Assert.Throws<IndexOutOfRangeException>(() => reader.Read());
            }
        }

        [Test]
        public void Read_UsingLookAhead_CachesAheadOfReads()
        {
            string[] values = new string[] { "test1", "test2", "test3" };

            int index = 0;
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[index++], 2))
            {
                Thread.Sleep(2);
                Assert.AreEqual(2, reader.CachedCount);
            }
        }
    }
}