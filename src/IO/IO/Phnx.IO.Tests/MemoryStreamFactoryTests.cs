using NUnit.Framework;
using System;
using System.Text;

namespace Phnx.IO.Tests
{
    public class MemoryStreamFactoryTests
    {
        [Test]
        public void NewMemoryStreamFactory_WithNoEncoding_SetsEncodingToUTF8()
        {
            var factory = new MemoryStreamFactory();

            Assert.AreEqual(Encoding.UTF8, factory.DefaultEncoding);
        }

        [Test]
        public void NewMemoryStreamFactory_WithEncoding_UsesEncoding()
        {
            var factory = new MemoryStreamFactory(Encoding.UTF7);

            Assert.AreEqual(Encoding.UTF7, factory.DefaultEncoding);
        }

        [Test]
        public void NewMemoryStreamFactory_WithNullEncoding_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MemoryStreamFactory(null));
        }

        [Test]
        public void Create_Empty_GetsEmptyMemoryStream()
        {
            var factory = new MemoryStreamFactory();

            var newStream = factory.Create();

            Assert.AreEqual(0, newStream.Length);
        }

        [Test]
        public void Create_WithText_GetsMemoryStreamWithData()
        {
            var expected = "test";
            var factory = new MemoryStreamFactory();

            var newStream = factory.Create(expected);

            var content = newStream.ReadToEnd();
            var contentAsString = factory.DefaultEncoding.GetString(content);

            Assert.AreEqual(expected, contentAsString);
        }

        [Test]
        public void Create_WithNullText_GetsEmptyMemoryStream()
        {
            var factory = new MemoryStreamFactory();

            var newStream = factory.Create(null);

            Assert.AreEqual(0, newStream.Length);
        }

        [Test]
        public void Create_WithTextAndEncoding_GetsMemoryStreamWithData()
        {
            var expected = "test";
            var encoding = Encoding.UTF32;
            var factory = new MemoryStreamFactory();

            var newStream = factory.Create(expected, encoding);

            var content = newStream.ReadToEnd();
            var contentAsString = encoding.GetString(content);

            Assert.AreEqual(expected, contentAsString);
        }

        [Test]
        public void Create_WithEncodingAndNullText_GetsEmptyMemoryStream()
        {
            var factory = new MemoryStreamFactory();

            var newStream = factory.Create(null, Encoding.UTF32);

            Assert.AreEqual(0, newStream.Length);
        }

        [Test]
        public void Create_WithTextAndNullEncoding_ThrowsArgumentNullException()
        {
            var factory = new MemoryStreamFactory();

            Assert.Throws<ArgumentNullException>(() => factory.Create("test", null));
        }
    }
}
