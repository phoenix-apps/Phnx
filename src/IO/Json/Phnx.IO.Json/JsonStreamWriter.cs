using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Phnx.IO.Json
{
    /// <summary>
    /// Provides a way to write Json data to a stream from a series of objects
    /// </summary>
    public class JsonStreamWriter : IDisposable
    {
        /// <summary>
        /// Create a new <see cref="JsonStreamWriter"/>
        /// </summary>
        /// <param name="stream">The stream to write Json to</param>
        /// <param name="closeStreamWhenDisposed">Whether to close the stream when this <see cref="JsonStreamWriter"/> is disposed</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        public JsonStreamWriter(TextWriter stream, bool closeStreamWhenDisposed = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            BaseJsonWriter = new JsonTextWriter(stream)
            {
                CloseOutput = closeStreamWhenDisposed
            };

            CloseStreamWhenDisposed = closeStreamWhenDisposed;
        }

        /// <summary>
        /// Create a new <see cref="JsonStreamWriter"/>
        /// </summary>
        /// <param name="jsonWriter">The output to write Json to</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonWriter"/> is <see langword="null"/></exception>
        public JsonStreamWriter(JsonTextWriter jsonWriter)
        {
            BaseJsonWriter = jsonWriter ?? throw new ArgumentNullException(nameof(jsonWriter));
        }

        /// <summary>
        /// The writer used to serialize objects to json
        /// </summary>
        public JsonWriter BaseJsonWriter { get; }

        /// <summary>
        /// Whether to close the stream when this is disposed
        /// </summary>
        public bool CloseStreamWhenDisposed
        {
            get => BaseJsonWriter.CloseOutput;
            set => BaseJsonWriter.CloseOutput = value;
        }

        /// <summary>
        /// Write Json text to the stream after validating that it is valid Json
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="json"/> is <see langword="null"/></exception>
        /// <exception cref="JsonReaderException">><paramref name="json"/> is not valid JSON</exception>
        public void Write(string json)
        {
            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            var newObj = JObject.Parse(json);

            Write(newObj);
        }

        /// <summary>
        /// Serialize and write an object as Json
        /// </summary>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <exception cref="ArgumentNullException"><paramref name="objectToWrite"/> is <see langword="null"/></exception>
        public void Write(object objectToWrite)
        {
            if (objectToWrite is null)
            {
                throw new ArgumentNullException(nameof(objectToWrite));
            }

            var newObj = JObject.FromObject(objectToWrite);

            Write(newObj);
        }

        /// <summary>
        /// Serialize and write a <see cref="JObject"/> as Json
        /// </summary>
        /// <param name="jObjectToWrite">The object to write to the stream</param>
        /// <exception cref="ArgumentNullException"><paramref name="jObjectToWrite"/> is <see langword="null"/></exception>
        public virtual void Write(JObject jObjectToWrite)
        {
            if (jObjectToWrite is null)
            {
                throw new ArgumentNullException(nameof(jObjectToWrite));
            }

            jObjectToWrite.WriteTo(BaseJsonWriter);
        }

        /// <summary>
        /// Dispose of this writer, and close the stream if specified
        /// </summary>
        public virtual void Dispose()
        {
            BaseJsonWriter?.Flush();

            BaseJsonWriter?.Close();
        }
    }
}
