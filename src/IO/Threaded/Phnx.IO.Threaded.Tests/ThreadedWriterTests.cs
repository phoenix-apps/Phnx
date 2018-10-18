using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Phnx.IO.Threaded.Tests
{
    public class ThreadedWriterTests
    {
        [Test]
        public void New_WithNullWriteFunc_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ThreadedWriter<object>(null));
        }

        [Test]
        public void New_WithNegativeMaximumQueue_ThrowsArgumentLessThanZero()
        {
            Assert.Throws<ArgumentLessThanZeroException>(() => new ThreadedWriter<object>(o => { }, -1));
        }

        [Test]
        public void Write_WithZeroQueue_WritesAllValues()
        {
            // Arrange
            var expected = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            var result = new List<string>();

            using (var writer = new ThreadedWriter<string>(s => result.Add(s), 0))
            {
                for (int index = 0; index < expected.Count; index++)
                {
                    writer.Write(expected[index]);
                }
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Write_WithQueue_WritesInOrder()
        {
            var expected = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            var result = new List<string>();

            using (var writer = new ThreadedWriter<string>(s => result.Add(s)))
            {
                for (int index = 0; index < expected.Count; index++)
                {
                    writer.Write(expected[index]);
                }
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Write_WhenWriteThrows_RethrowsInNextWrite()
        {
            var ex = new NullReferenceException();

            var writer = new ThreadedWriter<string>(s => throw ex);
            writer.Write(string.Empty);
            Thread.Sleep(2);

            Assert.Throws<NullReferenceException>(() => writer.Write(string.Empty));
        }

        [Test]
        public void Write_WhenWriteThrows_RethrowsWhenDiposing()
        {
            var ex = new NullReferenceException();

            var writer = new ThreadedWriter<string>(s => throw ex);
            writer.Write(string.Empty);

            Assert.Throws<NullReferenceException>(() => writer.Dispose());
        }

        [Test]
        public void Write_WhenQueueIsFull_WaitsForPreviousToFinish()
        {
            var expected = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            var results = new List<string>();

            using (var writer = new ThreadedWriter<string>(s =>
            {
                Thread.Sleep(1);
                results.Add(s);
            }, 1))
            {
                for (int index = 0; index < expected.Count; index++)
                {
                    writer.Write(expected[index]);

                    // Results have at least the current number of write requests minus the maximum queue size
                    Assert.IsTrue(results.Count >= index - 1);
                }
            }

            Assert.AreEqual(expected.Count, results.Count);
        }

        [Test]
        public void Write_AfterDispose_ThrowsObjectDisposedException()
        {
            var writer = new ThreadedWriter<object>(o => { });

            writer.Dispose();

            Assert.Throws<ObjectDisposedException>(() => writer.Write(null));
        }

        [Test]
        public void Write_ThreadStabilityTest_AlwaysWritesAll()
        {
            var expected = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };

            for (int loopCount = 0; loopCount < 10000; loopCount++)
            {
                var results = new List<string>(5);
                using (var writer = new ThreadedWriter<string>(s => results.Add(s)))
                {
                    foreach (var item in expected)
                    {
                        writer.Write(item);
                    }
                }

                CollectionAssert.AreEqual(expected, results);
            }
        }
    }
}
