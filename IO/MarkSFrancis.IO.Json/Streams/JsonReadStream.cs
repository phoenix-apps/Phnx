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

        public virtual T ReadObject<T>()
        {
            var loadedValue = ReadJObject();

            return JObjectConverter.ToObject<T>(loadedValue);
        }

        public virtual Dictionary<string, string> ReadPropertyDictionary()
        {
            var loadedValue = ReadJObject();

            return JObjectConverter.ToPropertyDictionary(loadedValue);
        }

        public virtual JObject ReadJObject()
        {
            return JObject.Load(Reader);
        }

        public virtual string ReadJson()
        {
            return JObjectConverter.ToJson(JObject.Load(Reader));
        }

        public bool ReachedEnd => TextReader.ReachedEnd();

        public virtual void Dispose()
        {
            Reader.Close();

            if (CloseBaseStreamWhenDisposed)
            {
                TextReader?.Dispose();
            }
        }
    }
}