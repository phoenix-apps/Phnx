using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phnx.IO.Json.Streams
{
    /// <summary>
    /// Provides a way to write Json data to a stream from a series of objects
    /// </summary>
    public class JsonWriteStream : IDisposable
    {
        /// <summary>
        /// Create a new <see cref="JsonWriteStream"/>
        /// </summary>
        /// <param name="stream">The stream to write Json to</param>
        /// <param name="closeStreamWhenDisposed">Whether to close the stream when this <see cref="JsonWriteStream"/> is disposed</param>
        public JsonWriteStream(TextWriter stream, bool closeStreamWhenDisposed = true)
        {
            TextWriter = stream;
            CloseStreamWhenDisposed = closeStreamWhenDisposed;

            Writer = new JsonTextWriter(stream)
            {
                CloseOutput = CloseStreamWhenDisposed
            };
        }

        /// <summary>
        /// The writer used to serialize objects to json
        /// </summary>
        protected JsonWriter Writer { get; }

        /// <summary>
        /// The stream to write Json data to
        /// </summary>
        protected TextWriter TextWriter { get; }

        /// <summary>
        /// Whether to close the stream when this is disposed
        /// </summary>
        public bool CloseStreamWhenDisposed { get; set; }

        /// <summary>
        /// Write Json text to the stream after validating that it is valid Json
        /// </summary>
        public virtual void WriteJson(string json)
        {
            var newObj = JObject.Parse(json);

            WriteJObject(newObj);
        }

        /// <summary>
        /// Serialize and write an object as Json
        /// </summary>
        /// <param name="o">The object to write to the stream</param>
        public virtual void WriteObject(object o)
        {
            var newObj = JObject.FromObject(o);

            WriteJObject(newObj);
        }

        /// <summary>
        /// Serialize, then write a property dictionary as Json
        /// </summary>
        /// <param name="data">The property dictionary to write</param>
        public virtual void WritePropertyDictionary(Dictionary<string, string> data)
        {
            var loadedValue = PropertyDictionaryConverter.From(data);

            WriteJObject(loadedValue);
        }

        /// <summary>
        /// Serialize and write a <see cref="JObject"/> as Json
        /// </summary>
        /// <param name="jObject">The object to write to the stream</param>
        public virtual void WriteJObject(JObject jObject)
        {
            jObject.WriteTo(Writer);
        }

        /// <summary>
        /// Dispose of this writer, and close the stream if specified
        /// </summary>
        public virtual void Dispose()
        {
            Writer?.Flush();

            Writer?.Close();

            if (CloseStreamWhenDisposed)
            {
                TextWriter?.Dispose();
            }
        }
    }
}