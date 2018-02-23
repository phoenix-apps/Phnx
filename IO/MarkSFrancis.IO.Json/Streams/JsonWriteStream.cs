using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private JsonWriter Writer { get; }

        private TextWriter TextWriter { get; }
        public bool CloseBaseStreamWhenDisposed { get; }

        public void Write<T>(T data)
        {
            var newObj = JsonConverter.ToJObject(data);

            Write(newObj);
        }

        public void Write(object o)
        {
            Write<object>(o);
        }

        protected void Write(JObject jObject)
        {
            jObject.WriteTo(Writer);
        }

        public void WrapAndWrite(Dictionary<string, string> data)
        {
            var loadedValue = Wrap(data);

            Write(loadedValue);
        }

        public static JObject Wrap(object o)
        {
            return JsonConverter.ToJObject(o);
        }

        public static JObject Wrap(Dictionary<string, string> data)
        {
            JObject newJObj = new JObject();

            var sortedData = data.OrderBy(d => d.Key);

            Dictionary<string, JObject> heirarchy = new Dictionary<string, JObject>();
            foreach (var dataProp in sortedData)
            {
                if (!dataProp.Key.Contains("."))
                {
                    newJObj.Add(dataProp.Key, dataProp.Value);
                }
                else
                {
                    // Child property
                    AppendToHeirarchy(newJObj, heirarchy, dataProp.Key, dataProp.Value);
                }
            }

            return newJObj;
        }

        private static string GetJObjectKeyNameFromFullyQualifiedName(string fullyQualifiedName)
        {
            return fullyQualifiedName.Contains(".")
                ? fullyQualifiedName.Substring(fullyQualifiedName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                : fullyQualifiedName;
        }

        private static void AppendToHeirarchy(JObject baseObject, Dictionary<string, JObject> heirarchySoFar, string key, string value)
        {
            string parentKey = key.Substring(0, key.LastIndexOf(".", StringComparison.Ordinal));

            if (!heirarchySoFar.ContainsKey(parentKey))
            {
                // Create parent jObject(s)

                Stack<string> missingParents = new Stack<string>();

                string tempKey = parentKey;
                while (tempKey.Contains(".") && !heirarchySoFar.ContainsKey(tempKey))
                {
                    missingParents.Push(tempKey);

                    tempKey = tempKey.Substring(0, tempKey.LastIndexOf('.'));
                }

                JObject curParent;
                if (!heirarchySoFar.ContainsKey(tempKey))
                {
                    curParent = baseObject;
                    missingParents.Push(tempKey);
                }
                else
                {
                    // Append object to existing object in heirarchy
                    curParent = heirarchySoFar[tempKey];
                }


                while (missingParents.Count > 0)
                {
                    // Create and append missing parents
                    var newParentKey = missingParents.Pop();
                    var newParent = new JObject();

                    heirarchySoFar.Add(newParentKey, newParent);

                    curParent.Add(GetJObjectKeyNameFromFullyQualifiedName(newParentKey), newParent);
                    curParent = newParent;
                }
            }

            // Append to parent jObject
            heirarchySoFar[parentKey].Add(key.Substring(key.LastIndexOf('.') + 1), value);
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