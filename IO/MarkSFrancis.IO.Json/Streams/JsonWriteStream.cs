using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json.Streams
{
    public class JsonWriteStream : IDisposable
    {
        public JsonWriteStream(TextWriter stream, bool closeBaseStreamWhenDisposed = true)
        {
            TextWriter = stream;
            CloseBaseStreamWhenDisposed = closeBaseStreamWhenDisposed;

            Writer = new JsonTextWriter(stream)
            {
                CloseOutput = CloseBaseStreamWhenDisposed
            };
        }

        protected JsonWriter Writer { get; }

        protected TextWriter TextWriter { get; }

        public bool CloseBaseStreamWhenDisposed { get; set; }

        public virtual void WriteJson(string json)
        {
            var newObj = JObjectConverter.FromJson(json);

            WriteJObject(newObj);
        }

        public virtual void WriteObject(object o)
        {
            var newObj = JObjectConverter.FromObject(o);

            WriteJObject(newObj);
        }

        public virtual void WritePropertyDictionary(Dictionary<string, string> data)
        {
            var loadedValue = JObjectConverter.FromPropertyDictionary(data);

            WriteJObject(loadedValue);
        }

        public virtual void WriteJObject(JObject jObject)
        {
            jObject.WriteTo(Writer);
        }

        public virtual void Dispose()
        {
            Writer.Flush();

            Writer.Close();

            if (CloseBaseStreamWhenDisposed)
            {
                TextWriter.Dispose();
            }
        }
    }
}