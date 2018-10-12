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

        public static JObject FromDictionary(Dictionary<string, string> properties)
        {
            JObject result = new JObject();

            var sortedProperties = properties.OrderBy(d => d.Key);

            Dictionary<string, JObject> knownProperties = new Dictionary<string, JObject>();
            foreach (var property in sortedProperties)
            {
                AddToJObject(knownProperties, result, property.Key, property.Value);
            }

            return result;
        }

        private static void AddToJObject(Dictionary<string, JObject> knownProperties, JObject baseObject, string propertyName, JToken propertyValue)
        {
            JObject keyParent = baseObject;
            Stack<string> missingAncestors = new Stack<string>();
            foreach (var parent in GetMemberAncestors(propertyName))
            {
                if (knownProperties.TryGetValue(parent, out keyParent))
                {
                    break;
                }
                else
                {
                    missingAncestors.Push(parent);
                }
            }

            if (keyParent is null)
            {
                keyParent = baseObject;
            }

            keyParent = AddMissingAncestors(knownProperties, keyParent, missingAncestors);

            keyParent.Add(GetMemberName(propertyName), propertyValue);
        }

        /// <param name="knownProperties"></param>
        /// <param name="knownAncestor">The parent of the first missing ancestor</param>
        /// <param name="missingAncestors">Must be sorted with the closest to the root first</param>
        private static JObject AddMissingAncestors(IDictionary<string, JObject> knownProperties, JObject knownAncestor, IEnumerable<string> missingAncestors)
        {
            JObject missingAncestorParent = knownAncestor;
            JObject missingAncestorValue = missingAncestorParent;

            foreach (var missingAncestor in missingAncestors)
            {
                missingAncestorValue = new JObject();

                // Add to jObject
                missingAncestorParent.Add(GetMemberName(missingAncestor), missingAncestorValue);

                //Add to known properties
                knownProperties.Add(missingAncestor, missingAncestorValue);
            }

            return missingAncestorValue;
        }

        /// <returns>Ancestors in descending order (child-most first, so a.b.c, then a.b, then a)</returns>
        private static IEnumerable<string> GetMemberAncestors(string fullyQualifiedName)
        {
            string parentName = GetParentFullName(fullyQualifiedName);

            while (parentName != null)
            {
                yield return parentName;

                parentName = GetParentFullName(parentName);
            }
        }

        /// <returns>Returns <see langword="null"/> if <paramref name="fullyQualifiedName"/> has no parents</returns>
        private static string GetParentFullName(string fullyQualifiedName)
        {
            var lastDelimiterIndex = fullyQualifiedName.LastIndexOf(ChildPropertyDelimiter,
                    StringComparison.Ordinal);

            if (lastDelimiterIndex < 0)
            {
                // No parent
                return null;
            }

            // Has parent
            return fullyQualifiedName.Substring(0, lastDelimiterIndex);
        }

        private static string GetMemberName(string fullyQualifiedName)
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
