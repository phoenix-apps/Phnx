using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Phnx.IO.Json.Streams
{
    /// <summary>
    /// Provides a way to write Json data to a stream from a series of objects
    /// </summary>
    public class JsonStreamWriter : IDisposable
    {
        /// <summary>
        /// Create a new <see cref="JsonStreamWriter"/>
        /// </summary>
        /// <param name="output">The stream to write Json to</param>
        /// <param name="closeStreamWhenDisposed">Whether to close the stream when this <see cref="JsonStreamWriter"/> is disposed</param>
        public JsonStreamWriter(TextWriter output, bool closeStreamWhenDisposed = true)
        {
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            CloseStreamWhenDisposed = closeStreamWhenDisposed;

            BaseJsonWriter = new JsonTextWriter(output)
            {
                CloseOutput = CloseStreamWhenDisposed
            };
        }

        /// <summary>
        /// Create a new <see cref="JsonStreamWriter"/>
        /// </summary>
        /// <param name="jsonWriter">The output to write Json to</param>
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
        public void WriteJson(string json)
        {
            var newObj = JObject.Parse(json);

            WriteJObject(newObj);
        }

        /// <summary>
        /// Serialize and write an object as Json
        /// </summary>
        /// <param name="o">The object to write to the stream</param>
        public void WriteObject(object o)
        {
            var newObj = JObject.FromObject(o);

            WriteJObject(newObj);
        }

        /// <summary>
        /// Serialize and write a <see cref="JObject"/> as Json
        /// </summary>
        /// <param name="jObject">The object to write to the stream</param>
        public virtual void WriteJObject(JObject jObject)
        {
            jObject.WriteTo(BaseJsonWriter);
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
