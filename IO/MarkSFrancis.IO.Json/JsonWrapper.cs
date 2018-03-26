using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json
{
    /// <summary>
    /// Provides methods for converting between a property dictionary and json
    /// </summary>
    internal static class JsonWrapper
    {
        public const string ChildPropertyDelimiter = ".";

        public static Dictionary<string, string> Unwrap(string json)
        {
            var jObject = JObjectConverter.FromJson(json);

            return Unwrap(jObject);
        }
        
        public static Dictionary<string, string> Unwrap(JObject obj, string baseSource = "")
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();

            foreach (var val in obj)
            {
                if (val.Value is JObject jObjValue)
                {
                    var childProperties = Unwrap(jObjValue, baseSource + val.Key + ChildPropertyDelimiter);

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

        public static JObject Wrap(Dictionary<string, string> data)
        {
            JObject newJObj = new JObject();

            var sortedData = data.OrderBy(d => d.Key);

            Dictionary<string, JObject> heirarchy = new Dictionary<string, JObject>();
            foreach (var dataProp in sortedData)
            {
                if (!dataProp.Key.Contains(ChildPropertyDelimiter))
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

        private static void AppendToHeirarchy(JObject baseObject, Dictionary<string, JObject> heirarchySoFar, string key, string value)
        {
            string parentKey = key.Substring(0, key.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal));

            if (!heirarchySoFar.ContainsKey(parentKey))
            {
                // Create parent jObject(s)

                Stack<string> missingParents = new Stack<string>();

                string tempKey = parentKey;
                while (tempKey.Contains(".") && !heirarchySoFar.ContainsKey(tempKey))
                {
                    missingParents.Push(tempKey);

                    tempKey = tempKey.Substring(0, tempKey.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal));
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
            heirarchySoFar[parentKey].Add(key.Substring(key.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal) + 1), value);
        }

        private static string GetJObjectKeyNameFromFullyQualifiedName(string fullyQualifiedName)
        {
            if (fullyQualifiedName.Contains(ChildPropertyDelimiter))
            {
                // Is a child property
                return fullyQualifiedName.Substring(fullyQualifiedName.LastIndexOf(ChildPropertyDelimiter,
                    StringComparison.Ordinal));
            }

            // Is already property name
            return fullyQualifiedName;
        }
    }
}
