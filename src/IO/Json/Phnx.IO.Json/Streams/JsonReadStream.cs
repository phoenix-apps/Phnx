using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Phnx.IO.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phnx.IO.Json.Streams
{
    /// <summary>
    /// Provides a way to read a stream containing Json data into a series of objects
    /// </summary>
    public class JsonReadStream : IDisposable
    {
        /// <summary>
        /// Create a new <see cref="JsonReadStream"/>
        /// </summary>
        /// <param name="stream">The stream to read Json from</param>
        /// <param name="closeStreamWhenDisposed">Whether to close the stream when this <see cref="JsonReadStream"/> is disposed</param>
        public JsonReadStream(TextReader stream, bool closeStreamWhenDisposed = true)
        {
            TextReader = stream;
            CloseStreamWhenDisposed = closeStreamWhenDisposed;

            Reader = new JsonTextReader(stream)
            {
                CloseInput = true
            };
        }

        /// <summary>
        /// The reader used to deserialize json into objects
        /// </summary>
        protected JsonReader Reader { get; }

        /// <summary>
        /// The stream from which to get Json data
        /// </summary>
        protected TextReader TextReader { get; }

        /// <summary>
        /// Whether to close the stream when this is disposed
        /// </summary>
        protected bool CloseStreamWhenDisposed { get; }

        /// <summary>
        /// Read and deserialize a Json object from the stream as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <returns>A deseriliazed object from the stream</returns>
        /// <exception cref="System.ArgumentException">Object is not of type <typeparamref name="T"/></exception>
        public virtual T ReadObject<T>()
        {
            var loadedValue = ReadJObject();

            return JObjectConverter.ToObject<T>(loadedValue);
        }

        /// <summary>
        /// Read a deserialize a Json object from the stream, returning it as a property dictionary instead of an object
        /// </summary>
        /// <returns>A property dictionary, where the key is the name of the property, and the value is the value of that property</returns>
        public virtual Dictionary<string, string> ReadPropertyDictionary()
        {
            var loadedValue = ReadJObject();

            return JObjectConverter.ToPropertyDictionary(loadedValue);
        }

        /// <summary>
        /// Read and deserialize a Json object as a <see cref="JObject"/>
        /// </summary>
        /// <returns>A <see cref="JObject"/> representing the Json object read from the stream</returns>
        public virtual JObject ReadJObject()
        {
            return JObject.Load(Reader);
        }

        /// <summary>
        /// Read but don't deserialize a Json object from the stream, returning it as a raw Json string. The string is validated as Json
        /// </summary>
        /// <returns>A Json string</returns>
        public virtual string ReadJson()
        {
            return JObjectConverter.ToJson(JObject.Load(Reader));
        }

        /// <summary>
        /// Whether the end of the stream has been reached
        /// </summary>
        public bool ReachedEnd => TextReader.ReachedEnd();

        /// <summary>
        /// Dispose of this reader, and close the stream if specified
        /// </summary>
        public virtual void Dispose()
        {
            Reader.Close();

            if (CloseStreamWhenDisposed)
            {
                TextReader?.Dispose();
            }
        }
    }
}