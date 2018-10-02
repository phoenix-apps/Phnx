using MarkSFrancis.IO.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Phnx.IO.Threaded.Tests
{
    public class ThreadedReaderTests
    {
        public ThreadedReaderTests()
        {
            StreamFactory = new MemoryStreamFactory();
        }

        private MemoryStreamFactory StreamFactory { get; }

        [Test]
        public void CreateThreadedReader_WithNullReadFunc_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ThreadedReader<object>(null));
        }

        [Test]
        public void ReadFromList_WithValidEntries_ReturnsFirstValue()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3" };
            string expectedResult = values.First();

            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values.First()))
            {
                // Act
                string value = reader.Read();

                // Assert
                Assert.AreEqual(expectedResult, value);
            }
        }

        [Test]
        public void ReadFromListUsingLookAhead_WithValidEntries_ReturnsIndexedValue()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            List<string> results = new List<string>(values.Count);

            int index = 0;
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() =>
                {
                    var returnValue = values[index];
                    index++;
                    return returnValue;
                }
                , 20, 3))
            {
                Thread.Sleep(100);

                // Act
                for (int followerIndex = 0; followerIndex < values.Count; followerIndex++)
                {
                    results.Add(reader.Read());
                }
            }

            //Assert
            Assert.AreEqual(values, results);
        }

        [Test]
        public void ReadFromStreamUsingLookAhead_WithValidEntries_ReturnsAllValues()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };

            var ms = StreamFactory.Create(values);

            StreamReader msReader = new StreamReader(ms);

            List<string> results = new List<string>(values.Count);

            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => msReader.ReadLine(), 20, 3))
            {
                Thread.Sleep(100);

                // Act
                for (int followerIndex = 0; followerIndex < values.Count; followerIndex++)
                {
                    results.Add(reader.Read());
                }
            }

            //Assert
            Assert.AreEqual(values, results);
        }
    }
}