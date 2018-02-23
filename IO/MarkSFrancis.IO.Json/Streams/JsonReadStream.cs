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
        public JsonReadStream(TextReader stream)
        {
            TextReader = stream;

            Reader = new JsonTextReader(stream)
            {
                CloseInput = true
            };
        }

        private JsonReader Reader { get; }

        private TextReader TextReader { get; }

        protected JObject ReadObject()
        {
            return JObject.Load(Reader);
        }

        public T Read<T>()
        {
            var loadedValue = ReadObject();

            return loadedValue.ToObject<T>();
        }

        public Dictionary<string, string> ReadAndUnwrap()
        {
            var loadedValue = ReadObject();

            return Unwrap(loadedValue);
        }

        public bool ReachedEnd => TextReader.ReachedEnd();

        public static Dictionary<string, string> Unwrap(JObject obj, string baseSource = "")
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();

            foreach (var val in obj)
            {
                if (val.Value is JObject jObjValue)
                {
                    var childProperties = Unwrap(jObjValue, baseSource + val.Key + ".");
                    foreach (var property in childProperties)
                    {
                        retValue.Add(property.Key, property.Value);
                    }
                }
                else
                {
                    retValue.Add(baseSource + val.Key, val.Value.ToString().Trim('{', '}'));
                }
            }

            return retValue;
        }

        public void Dispose()
        {
            Reader.Close();
            TextReader.Dispose();
        }
    }
}