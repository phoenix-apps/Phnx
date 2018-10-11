using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.IO.Json
{
    /// <summary>
    /// Provides methods for converting between a property dictionary and json
    /// </summary>
    internal static class JsonWrapper
    {
        public const string ChildPropertyDelimiter = ".";

        public static Dictionary<string, string> ToDictionary(JObject obj)
        {
            Dictionary<string, string> propertyDictionary = new Dictionary<string, string>();

            ToDictionary(obj, propertyDictionary, string.Empty);

            return propertyDictionary;
        }

        private static void ToDictionary(JObject obj, Dictionary<string, string> propertyDictionarySoFar, string currentPropertyFullName)
        {
            foreach (var property in obj)
            {
                string propertyFullName = currentPropertyFullName + property.Key;

                if (property.Value is JObject jObjProperty)
                {
                    // Has children to be deserialized
                    ToDictionary(
                        jObjProperty,
                        propertyDictionarySoFar,
                        propertyFullName + ChildPropertyDelimiter);
                }
                else
                {
                    var jsonValue = property.Value.ToString();

                    propertyDictionarySoFar.Add(
                        propertyFullName,
                        jsonValue.Trim('{', '}'));
                }
            }
        }

        public static JObject FromDictionary(Dictionary<string, string> data)
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
                    // Contains child property
                    AppendToHeirarchy(newJObj, heirarchy, dataProp.Key, dataProp.Value);
                }
            }

            return newJObj;
        }

        private static void AppendToHeirarchy(JObject baseObject, Dictionary<string, JObject> heirarchySoFar, string key, string value)
        {
            string parentKey = GetMemberParentFullyQualifiedName(key);

            if (parentKey is null)
            {
                // No parents
                return;
            }

            if (!heirarchySoFar.ContainsKey(parentKey))
            {
                // Create parent jObject(s)

                Stack<string> missingParents = new Stack<string>();

                string ancestoryKey = parentKey;
                while (ancestoryKey.Contains(".") && !heirarchySoFar.ContainsKey(ancestoryKey))
                {
                    missingParents.Push(ancestoryKey);

                    ancestoryKey = ancestoryKey.Substring(0, ancestoryKey.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal));
                }

                JObject knownAncestor;
                if (!heirarchySoFar.ContainsKey(ancestoryKey))
                {
                    knownAncestor = baseObject;
                    missingParents.Push(ancestoryKey);
                }
                else
                {
                    // Append object to existing object in heirarchy
                    knownAncestor = heirarchySoFar[ancestoryKey];
                }

                while (missingParents.Count > 0)
                {
                    // Create and append missing parents
                    var newParentKey = missingParents.Pop();
                    var newParent = new JObject();

                    heirarchySoFar.Add(newParentKey, newParent);

                    knownAncestor.Add(GetPropertyName(newParentKey), newParent);
                    knownAncestor = newParent;
                }
            }

            // Append to parent jObject
            heirarchySoFar[parentKey].Add(key.Substring(key.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal) + 1), value);
        }

        private static string GetMemberParentFullyQualifiedName(string memberFullyQualifiedName)
        {
            int parentEndIndex = memberFullyQualifiedName.LastIndexOf(ChildPropertyDelimiter, StringComparison.Ordinal);

            if (parentEndIndex < 0)
            {
                // No parent
                return null;
            }

            return memberFullyQualifiedName.Substring(0, parentEndIndex);
        }

        private static void AddAncestors(string key)
        {

        }

        private static IEnumerable<string> GetMemberAncestors(string fullyQualifiedName)
        {
            int ancestorEndIndex = fullyQualifiedName.IndexOf(ChildPropertyDelimiter, StringComparison.Ordinal);

            while (ancestorEndIndex >= 0)
            {
                yield return fullyQualifiedName.Substring(0, ancestorEndIndex);

                fullyQualifiedName = fullyQualifiedName.Substring(ancestorEndIndex + ChildPropertyDelimiter.Length);

                ancestorEndIndex = fullyQualifiedName.IndexOf(ChildPropertyDelimiter);
            }
        }

        private static string GetPropertyName(string fullyQualifiedName)
        {
            var lastDelimiterIndex = fullyQualifiedName.LastIndexOf(ChildPropertyDelimiter,
                    StringComparison.Ordinal);

            if (lastDelimiterIndex >= 0)
            {
                // Is a child property
                return fullyQualifiedName.Substring(lastDelimiterIndex + ChildPropertyDelimiter.Length);
            }

            // Is already property name
            return fullyQualifiedName;
        }
    }
}
