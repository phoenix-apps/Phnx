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
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3" };
            string expectedResult = values.First();

            var index = 0;
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[index++]))
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
            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => values[index++], 20))
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

            var pipe = new PipeStream();

            foreach (var value in values)
            {
                pipe.In.WriteLine(value);
            }

            List<string> results = new List<string>(values.Count);

            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => pipe.Out.ReadLine(), 20))
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