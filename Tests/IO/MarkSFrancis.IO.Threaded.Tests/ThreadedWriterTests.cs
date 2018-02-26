using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarkSFrancis.IO.Factory;
using NUnit.Framework;

namespace MarkSFrancis.IO.Threaded.Tests
{
    public class ThreadedWriterTests
    {
        public ThreadedWriterTests()
        {
            StreamFactory = new MemoryStreamFactory();
        }

        private MemoryStreamFactory StreamFactory { get; }

        [Test]
        public void Write_WithValidEntries_WritesFirstValue()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3" };
            string result = null;
            string expectedResult = values.First();

            using (ThreadedWriter<string> writer = new ThreadedWriter<string>(s => result = s))
            {
                // Act
                writer.Write(values.First());
                writer.Dispose();

                // Assert
                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public void ReadFromStreamUsingLookAhead_WithValidEntries_ReturnsAllValues()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };

            var ms = StreamFactory.Create();

            StreamWriter msWriter = new StreamWriter(ms);

            using (ThreadedWriter<string> writer = new ThreadedWriter<string>(s => msWriter.WriteLine(s)))
            {
                for (int index = 0; index < values.Count; index++)
                {
                    writer.Write(values[index]);
                }
            }

            msWriter.Flush();

            ms.Position = 0;

            string[] results;
            using (StreamReader reader = new StreamReader(ms))
            {
                results = reader.ReadToEnd().Split(new [] {Environment.NewLine}, StringSplitOptions.None);
            }

            // A blank line is added by the WriteLine method used. This removes that blank line from the end
            var resultsWithoutFinalBlankLine = results.Take(results.Length - 1).ToList();

            //Assert
            Assert.AreEqual(values, resultsWithoutFinalBlankLine);
        }
    }
}