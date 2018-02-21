using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MarkSFrancis.IO.Threaded.Tests
{
    public class ThreadedWriterTests
    {
        [Test]
        public void Write_WithValidEntries_WritesFirstItem()
        {
            // Arrange
            List<string> items = new List<string> { "asdf", "asdf2", "asdf3" };
            string result = null;
            string expectedResult = items.First();

            using (ThreadedWriter<string> writer = new ThreadedWriter<string>(s => result = s))
            {
                // Act
                writer.Write(items.First());
                writer.Dispose();

                // Assert
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}