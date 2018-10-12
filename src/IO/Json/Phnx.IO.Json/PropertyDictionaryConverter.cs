using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.IO.Json
{
    /// <summary>
    /// Helper methods related to converting <see cref="JObject"/>s
    /// </summary>
    public class PropertyDictionaryConverter
    {
        /// <summary>
        /// Create a new <see cref="PropertyDictionaryConverter"/>
        /// </summary>
        public PropertyDictionaryConverter() { }

        /// <summary>
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        public JObject From(Dictionary<string, string> propertyDictionary)
        {
            if (propertyDictionary is null)
            {
                throw new ArgumentNullException(nameof(propertyDictionary));
            }

            return FromDictionary(propertyDictionary);
        }

        /// <summary>
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <typeparam name="T">The type of object represented by <paramref name="propertyDictionary"/></typeparam>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        public T From<T>(Dictionary<string, string> propertyDictionary)
        {
            if (propertyDictionary is null)
            {
                throw new ArgumentNullException(nameof(propertyDictionary));
            }

            var jObject = From(propertyDictionary);

            return jObject.ToObject<T>();
        }

        /// <summary>
        /// Convert an <see cref="object"/> to a property dictionary
        /// </summary>
        /// <param name="o">The <see cref="object"/> to convert</param>
        /// <returns><paramref name="o"/> as a property dictionary</returns>
        public Dictionary<string, string> To(object o)
        {
            if (o is null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            var jObject = JObject.FromObject(o);

            return To(jObject);
        }

        /// <summary>
        /// Convert a <see cref="JObject"/> to a property dictionary
        /// </summary>
        /// <param name="jObj">The <see cref="JObject"/> to convert</param>
        /// <returns><paramref name="jObj"/> as a property dictionary</returns>
        public Dictionary<string, string> To(JObject jObj)
        {
            if (jObj is null)
            {
                throw new ArgumentNullException(nameof(jObj));
            }

            Dictionary<string, string> propertyDictionary = new Dictionary<string, string>();

            ToDictionary(jObj, propertyDictionary, string.Empty);

            return propertyDictionary;
        }

        /// <summary>
        /// The delimiter between properties and children in property dictionaries
        /// </summary>
        public const string ChildPropertyDelimiter = ".";

        private static void ToDictionary(JObject obj, Dictionary<string, string> propertyDictionarySoFar, string currentPropertyFullName)
        {
            foreach (var property in obj)
            {
                string propertyFullName = currentPropertyFullName + property.Key;

                // Could cause StackOverflowException in deep objects. Can this be optimised to use tailed recursion?
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

        private static JObject FromDictionary(Dictionary<string, string> properties)
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
