using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace MarkSFrancis.IO.Threaded.Tests
{
    public class ThreadedReaderTests
    {
        [Test]
        public void ReadFromList_WithValidEntries_ReturnsFirstItem()
        {
            // Arrange
            List<string> items = new List<string> { "asdf", "asdf2", "asdf3" };
            string expectedResult = items.First();

            using (ThreadedReader<string> reader = new ThreadedReader<string>(() => items.First()))
            {
                // Act
                string value = reader.Read();

                // Assert
                Assert.AreEqual(expectedResult, value);
            }
        }

        [Test]
        public void ReadFromList_WithValidEntries_ReturnsIndexedItem()
        {
            // Arrange
            List<string> items = new List<string> { "asdf", "asdf2", "asdf3" };

            for (int index = 0; index < items.Count; index++)
            {
                using (ThreadedReader<string> reader = new ThreadedReader<string>(() => items[index]))
                {
                    // Act
                    string value = reader.Read();

                    // Assert
                    Assert.AreEqual(items[index], value);
                }
            }
        }

        [Test]
        public void ReadFromListUsingLookAhead_WithValidEntries_ReturnsIndexedItem()
        {
            // Arrange
            List<string> items = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            List<string> results = new List<string>(items.Count);

            {
                int leadIndex = 0;
                using (ThreadedReader<string> reader = new ThreadedReader<string>(() =>
                    {
                        var returnValue = items[leadIndex];
                        leadIndex++;
                        return returnValue;
                    }
                    , 20, 3))
                {
                    Thread.Sleep(100);

                    // Act
                    for (int followerIndex = 0; followerIndex < items.Count; followerIndex++)
                    {
                        results.Add(reader.Read());
                    }
                }
            }

            //Assert
            Assert.AreEqual(items, results);
        }

        [Test]
        public void ReadFromStreamUsingLookAhead_WithValidEntries_ReturnsIndexedItem()
        {
            // Arrange
            List<string> items = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };
            MemoryStream ms = new MemoryStream();
            StreamWriter msWriter = new StreamWriter(ms);
            foreach (var item in items)
            {
                msWriter.WriteLine(item);
            }
            msWriter.Flush();
            ms.Flush();

            ms.Position = 0;
            StreamReader msReader = new StreamReader(ms);

            List<string> results = new List<string>(items.Count);

            {
                using (ThreadedReader<string> reader = new ThreadedReader<string>(() => msReader.ReadLine(), 20, 3))
                {
                    Thread.Sleep(100);

                    // Act
                    for (int followerIndex = 0; followerIndex < items.Count; followerIndex++)
                    {
                        results.Add(reader.Read());
                    }
                }
            }

            //Assert
            Assert.AreEqual(items, results);
        }
    }
}