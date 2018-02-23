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

        public void Write(object o)
        {
            var newObj = JsonConverter.ToJObject(o);

            WriteJObject(newObj);
        }

        public void WrapAndWrite(Dictionary<string, string> data)
        {
            var loadedValue = Wrap(data);

            WriteJObject(loadedValue);
        }

        protected JObject Wrap(Dictionary<string, string> data)
        {
            return JsonWrapper.Wrap(data);
        }

        protected void WriteJObject(JObject jObject)
        {
            jObject.WriteTo(Writer);
        }

        public void Dispose()
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