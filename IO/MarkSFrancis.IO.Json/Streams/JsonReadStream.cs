using System;
using System.Collections.Generic;
using System.IO;
using MarkSFrancis.IO.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json.Streams
{
    public class JsonReadStream : IDisposable
    {
        public JsonReadStream(TextReader stream, bool closeBaseStreamWhenDisposed = true)
        {
            TextReader = stream;
            CloseBaseStreamWhenDisposed = closeBaseStreamWhenDisposed;

            Reader = new JsonTextReader(stream)
            {
                CloseInput = true
            };
        }

        protected JsonReader Reader { get; }

        protected TextReader TextReader { get; }

        public bool CloseBaseStreamWhenDisposed { get; set; }

        public virtual T Read<T>()
        {
            var loadedValue = ReadJObject();

            return loadedValue.ToObject<T>();
        }

        public virtual Dictionary<string, string> ReadAndUnwrap()
        {
            var loadedValue = ReadJObject();

            return Unwrap(loadedValue);
        }

        protected JObject ReadJObject()
        {
            return JObject.Load(Reader);
        }

        protected Dictionary<string, string> Unwrap(JObject jObject)
        {
            return JsonWrapper.Unwrap(jObject);
        }

        public bool ReachedEnd => TextReader.ReachedEnd();

        public void Dispose()
        {
            Reader.Close();

            if (CloseBaseStreamWhenDisposed)
            {
                TextReader?.Dispose();
            }
        }
    }
}