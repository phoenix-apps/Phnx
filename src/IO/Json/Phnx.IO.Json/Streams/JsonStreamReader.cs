using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Phnx.IO.Json.Streams
{
    /// <summary>
    /// Provides a way to read a stream containing Json data into a series of objects
    /// </summary>
    public class JsonStreamReader : IDisposable
    {
        private readonly JsonSerializer _jsonSerializer;

        /// <summary>
        /// Create a new <see cref="JsonStreamReader"/>
        /// </summary>
        /// <param name="stream">The stream to read Json from</param>
        /// <param name="closeStreamWhenDisposed">Whether to close the stream when this <see cref="JsonStreamReader"/> is disposed</param>
        public JsonStreamReader(TextReader stream, bool closeStreamWhenDisposed = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            BaseJsonReader = new JsonTextReader(stream)
            {
                CloseInput = closeStreamWhenDisposed,
                SupportMultipleContent = true
            };

            _jsonSerializer = new JsonSerializer();
        }

        /// <summary>
        /// Create a new <see cref="JsonStreamReader"/>
        /// </summary>
        /// <param name="jsonReader">The input to read Json from</param>
        /// <remarks>To support streaming, <paramref name="jsonReader"/>'s property <see cref="JsonReader.SupportMultipleContent"/> will be set to <see langword="true"/></remarks>
        public JsonStreamReader(JsonReader jsonReader)
        {
            BaseJsonReader = jsonReader ?? throw new ArgumentNullException(nameof(jsonReader));
            BaseJsonReader.SupportMultipleContent = true;
        }

        /// <summary>
        /// The reader used to deserialize json into objects
        /// </summary>
        public JsonReader BaseJsonReader { get; }

        /// <summary>
        /// Whether to close the stream when this is disposed
        /// </summary>
        public bool CloseStreamWhenDisposed
        {
            get => BaseJsonReader.CloseInput;
            set => BaseJsonReader.CloseInput = value;
        }

        /// <summary>
        /// Read and deserialize a Json object from the stream as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <returns>A deseriliazed object from the stream</returns>
        /// <exception cref="System.ArgumentException">Object is not of type <typeparamref name="T"/></exception>
        public T ReadObject<T>()
        {
            var loadedValue = ReadJObject();

            return loadedValue.ToObject<T>();
        }

        /// <summary>
        /// Read and deserialize a Json object as a <see cref="JObject"/>
        /// </summary>
        /// <returns>A <see cref="JObject"/> representing the Json object read from the stream</returns>
        /// <exception cref="EndOfStreamException"></exception>
        public virtual JObject ReadJObject()
        {
            if (!BaseJsonReader.Read())
            {
                throw new EndOfStreamException();
            }

            return JObject.Load(BaseJsonReader);
        }

        /// <summary>
        /// Read but don't deserialize a Json object from the stream, returning it as a raw Json string. The string is validated as Json
        /// </summary>
        /// <returns>A Json string</returns>
        public virtual string ReadJson()
        {
            var jObject = ReadJObject();
            return jObject.ToString();
        }

        /// <summary>
        /// Dispose of this reader, and close the stream if specified
        /// </summary>
        public virtual void Dispose()
        {
            BaseJsonReader?.Close();
        }
    }
}
