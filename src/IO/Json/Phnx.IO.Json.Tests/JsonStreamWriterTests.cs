using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Phnx.IO.Json.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phnx.IO.Json.Tests.Streams
{
    public class JsonStreamWriterTests
    {
        [Test]
        public void New_WhenTextWriterIsNull_ThrowsArgumentNullException()
        {
            TextWriter writer = null;
            Assert.Throws<ArgumentNullException>(() => new JsonStreamWriter(writer));
        }

        [Test]
        public void New_WhenCloseStreamIsTrue_SetsCloseStreamToTrue()
        {
            var writer = new PipeStream().In;

            var jsonWriter = new JsonStreamWriter(writer, true);

            Assert.IsTrue(jsonWriter.CloseStreamWhenDisposed);
        }

        [Test]
        public void New_WhenCloseStreamIsFalse_SetsCloseStreamToFalse()
        {
            var writer = new PipeStream().In;

            var jsonWriter = new JsonStreamWriter(writer, false);

            Assert.IsFalse(jsonWriter.CloseStreamWhenDisposed);
        }

        [Test]
        public void New_WhenJsonWriterIsNull_ThrowsArgumentNullException()
        {
            JsonTextWriter jsonWriter = null;
            Assert.Throws<ArgumentNullException>(() => new JsonStreamWriter(jsonWriter));
        }

        [Test]
        public void New_WithJsonWriter_SetsBaseJsonWriterToWriter()
        {
            var pipe = new PipeStream();
            var writer = new JsonTextWriter(pipe.In);

            var jsonStream = new JsonStreamWriter(writer);

            Assert.AreEqual(writer, jsonStream.BaseJsonWriter);
        }

        [Test]
        public void Write_WhenObjectIsNull_ThrowsArgumentNullException()
        {
            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            object dataToWrite = null;

            Assert.Throws<ArgumentNullException>(() => jsonStream.Write(dataToWrite));
        }

        [Test]
        public void Write_WhenJObjectIsNull_ThrowsArgumentNullException()
        {
            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            JObject dataToWrite = null;

            Assert.Throws<ArgumentNullException>(() => jsonStream.Write(dataToWrite));
        }

        [Test]
        public void Write_OneComplexObject_WritesObject()
        {
            var expectedAsObject = new DeepFake
            {
                Collection = new List<ShallowFake>
                {
                    new ShallowFake
                    {
                        Id = 123
                    }
                },
                Single = new ShallowFake
                {
                    Id = 25
                }
            };

            var expected = JsonConvert.SerializeObject(expectedAsObject);
            var pipe = new PipeStream();
            var writer = new JsonTextWriter(pipe.In);
            var jsonStream = new JsonStreamWriter(writer);

            jsonStream.Write(expectedAsObject);

            var result = pipe.ReadToEndAsString();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Write_TwoComplexObjects_WritesOneThenNext()
        {
            var expectedFirst = new ShallowFake
            {
                Id = 123
            };

            var expectedSecond = new ShallowFake
            {
                Id = 25
            };

            var expected =
                JsonConvert.SerializeObject(expectedFirst) +
                JsonConvert.SerializeObject(expectedSecond);

            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            jsonStream.Write(expectedFirst);
            jsonStream.Write(expectedSecond);

            var result = pipe.ReadToEndAsString();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Write_WhenJsonIsNull_ThrowsArgumentNullException()
        {
            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            Assert.Throws<ArgumentNullException>(() => jsonStream.Write((string)null));
        }

        [Test]
        public void Write_InvalidJSON_ThrowsJsonReaderException()
        {
            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            Assert.Throws<JsonReaderException>(() => jsonStream.Write("this is just a tribute"));
        }

        [Test]
        public void Write_ValidJSON_WritesJsonToStream()
        {
            var expectedObject = new ShallowFake
            {
                Id = 25
            };
            var expected = JsonConvert.SerializeObject(expectedObject);

            var pipe = new PipeStream();
            var jsonStream = new JsonStreamWriter(pipe.In);

            jsonStream.Write(expected);

            var result = pipe.ReadToEndAsString();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Dispose_WhenCloseStreamWhenDisposedIsTrue_ClosesStream()
        {
            var stream = new FakeWriter();
            var writer = new JsonStreamWriter(stream)
            {
                CloseStreamWhenDisposed = true
            };

            Assert.IsTrue(stream.IsOpen);
            writer.Dispose();

            Assert.IsFalse(stream.IsOpen);
        }

        [Test]
        public void Dispose_WhenCloseStreamWhenDisposedIsFalse_DoesNotCloseStream()
        {
            var stream = new FakeWriter();
            var writer = new JsonStreamWriter(stream)
            {
                CloseStreamWhenDisposed = false
            };

            Assert.IsTrue(stream.IsOpen);
            writer.Dispose();

            Assert.IsTrue(stream.IsOpen);
        }
    }
}
