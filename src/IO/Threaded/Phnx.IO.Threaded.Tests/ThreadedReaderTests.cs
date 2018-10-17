using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        public void Read_WithZeroLookAhead_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ThreadedReader<string>(() => null, 0));
        }

        [Test]
        public void Read_WhenUsingLookAhead_ReadsInOrder()
        {
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            string[] results = new string[values.Count];

            int index = 0;
            using (var reader = new ThreadedReader<string>(() => values[index++], 20))
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
            var ex = new NullReferenceException();

            using (var reader = new ThreadedReader<string>(() => throw ex))
            {
                Assert.Throws<NullReferenceException>(() => reader.Read());
            }
        }

        [Test]
        public void Read_UsingLookAhead_CachesAheadOfReads()
        {
            string[] values = new string[] { "test1", "test2", "test3" };

            int index = 0;
            using (var reader = new ThreadedReader<string>(() => values[index++], 2))
            {
                Thread.Sleep(2);
                Assert.AreEqual(2, reader.CachedCount);
            }
        }

        [Test]
        public void Read_BeyondEndOfCache_ContinuesToBuildCacheAheadOfReads()
        {
            string[] expected = new string[] { "test1", "test2", "test3" };
            List<string> results = new List<string>(3);

            int index = 0;
            using (var reader = new ThreadedReader<string>(() => expected[index++], 1))
            {
                results.Add(reader.Read());
                results.Add(reader.Read());
                results.Add(reader.Read());
            }

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void Read_ThreadStabilityTest_AlwaysReadsAll()
        {
            var expected = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };

            for (int loopCount = 0; loopCount < 10000; loopCount++)
            {
                List<string> results = new List<string>(5);

                int index = 0;
                using (var reader = new ThreadedReader<string>(() => index >= expected.Count ? null : expected[index++], 5))
                {
                    results.Add(reader.Read());
                    results.Add(reader.Read());
                    results.Add(reader.Read());
                    results.Add(reader.Read());
                    results.Add(reader.Read());
                }

                CollectionAssert.AreEqual(expected, results);
            }
        }
    }
}
