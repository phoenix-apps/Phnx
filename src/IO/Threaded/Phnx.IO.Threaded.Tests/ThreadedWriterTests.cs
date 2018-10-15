using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.IO.Threaded.Tests
{
    public class ThreadedWriterTests
    {
        [Test]
        public void CreateThreadedWriter_WithNullWriteFunc_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ThreadedWriter<object>(null));
        }

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
        public void WriteToStreamUsingLookAhead_WithValidEntries_ReturnsAllValues()
        {
            // Arrange
            List<string> values = new List<string> { "asdf", "asdf2", "asdf3", "asdf4", "asdf5" };

            var pipe = new PipeStream();

            using (ThreadedWriter<string> writer = new ThreadedWriter<string>(s => pipe.In.WriteLine(s)))
            {
                for (int index = 0; index < values.Count; index++)
                {
                    writer.Write(values[index]);
                }
            }

            string[] results = pipe.Out.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // A blank line is added by the WriteLine method used. This removes that blank line from the end
            var resultsWithoutFinalBlankLine = results.Take(results.Length - 1).ToList();

            //Assert
            Assert.AreEqual(values, resultsWithoutFinalBlankLine);
        }
    }
}
