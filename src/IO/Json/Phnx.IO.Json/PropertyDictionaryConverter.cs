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
        /// <param name="childPropertyDelimiter">The delimiter to seperate child properties from parents in complex objects</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="propertyDictionary"/> or <paramref name="childPropertyDelimiter"/> is <see langword="null"/></exception>
        public JToken From(Dictionary<string, string> propertyDictionary, string childPropertyDelimiter = ".")
        {
            if (propertyDictionary is null)
            {
                throw new ArgumentNullException(nameof(propertyDictionary));
            }
            if (childPropertyDelimiter is null)
            {
                throw new ArgumentNullException(nameof(childPropertyDelimiter));
            }

            return FromDictionary(propertyDictionary, childPropertyDelimiter);
        }

        /// <summary>
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <typeparam name="T">The type of object represented by <paramref name="propertyDictionary"/></typeparam>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <param name="childPropertyDelimiter">The delimiter to seperate child properties from parents in complex objects</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="propertyDictionary"/> or <paramref name="childPropertyDelimiter"/> is <see langword="null"/></exception>
        public T From<T>(Dictionary<string, string> propertyDictionary, string childPropertyDelimiter = ".")
        {
            if (propertyDictionary is null)
            {
                throw new ArgumentNullException(nameof(propertyDictionary));
            }
            if (childPropertyDelimiter is null)
            {
                throw new ArgumentNullException(nameof(childPropertyDelimiter));
            }

            var jObject = From(propertyDictionary, childPropertyDelimiter);

            return jObject.ToObject<T>();
        }

        /// <summary>
        /// Convert an <see cref="object"/> to a property dictionary
        /// </summary>
        /// <param name="objectToConvert">The <see cref="object"/> to convert</param>
        /// <param name="childPropertyDelimiter">The delimiter to seperate child properties from parents in complex objects</param>
        /// <returns><paramref name="objectToConvert"/> as a property dictionary</returns>
        /// <exception cref="ArgumentNullException"><paramref name="objectToConvert"/> or <paramref name="childPropertyDelimiter"/> is <see langword="null"/></exception>
        public Dictionary<string, string> To(object objectToConvert, string childPropertyDelimiter = ".")
        {
            if (objectToConvert is null)
            {
                throw new ArgumentNullException(nameof(objectToConvert));
            }
            if (childPropertyDelimiter is null)
            {
                throw new ArgumentNullException(nameof(childPropertyDelimiter));
            }

            var jObject = JObject.FromObject(objectToConvert);

            return To(jObject, childPropertyDelimiter);
        }

        /// <summary>
        /// Convert a <see cref="JObject"/> to a property dictionary
        /// </summary>
        /// <param name="jObjectToConvert">The <see cref="JObject"/> to convert</param>
        /// <param name="childPropertyDelimiter">The delimiter to seperate child properties from parents in complex objects</param>
        /// <returns><paramref name="jObjectToConvert"/> as a property dictionary</returns>
        /// <exception cref="ArgumentNullException"><paramref name="jObjectToConvert"/> or <paramref name="childPropertyDelimiter"/> is <see langword="null"/></exception>
        public Dictionary<string, string> To(JObject jObjectToConvert, string childPropertyDelimiter = ".")
        {
            if (jObjectToConvert is null)
            {
                throw new ArgumentNullException(nameof(jObjectToConvert));
            }
            if (childPropertyDelimiter is null)
            {
                throw new ArgumentNullException(nameof(childPropertyDelimiter));
            }

            Dictionary<string, string> propertyDictionary = new Dictionary<string, string>();

            ToDictionary(jObjectToConvert, childPropertyDelimiter, propertyDictionary, string.Empty);

            return propertyDictionary;
        }

        private static void ToDictionary(JToken obj, string childPropertyDelimiter, Dictionary<string, string> propertyDictionarySoFar, string currentPropertyFullName)
        {
            if (obj is JObject complexType)
            {
                foreach (var property in complexType)
                {
                    string childPropertyName;
                    if (string.IsNullOrEmpty(currentPropertyFullName))
                    {
                        childPropertyName = property.Key;
                    }
                    else
                    {
                        childPropertyName = currentPropertyFullName + childPropertyDelimiter + property.Key;
                    }

                    // Could cause StackOverflowException in deep objects. Can this be optimised to use tailed recursion?
                    // Has children to be deserialized
                    ToDictionary(
                        property.Value,
                        childPropertyDelimiter,
                        propertyDictionarySoFar,
                        childPropertyName);
                }
            }
            else if (obj is JArray jArrayProperty)
            {
                // Enumerate through array, and convert each
                for (var index = 0; index < jArrayProperty.Count; index++)
                {
                    var curEntry = jArrayProperty[index];

                    ToDictionary(curEntry, childPropertyDelimiter, propertyDictionarySoFar, currentPropertyFullName + $"[{index}]");
                }
            }
            else
            {
                var jsonValue = obj.ToString();

                propertyDictionarySoFar.Add(
                    currentPropertyFullName,
                    jsonValue.Trim('{', '}'));
            }

        }

        private static JToken FromDictionary(Dictionary<string, string> properties, string childPropertyDelimiter)
        {
            JObject result = new JObject();

            var sortedProperties = properties.OrderBy(d => d.Key);

            Dictionary<string, JObject> knownProperties = new Dictionary<string, JObject>();
            foreach (var property in sortedProperties)
            {
                AddToJObject(knownProperties, result, childPropertyDelimiter, property.Key, property.Value);
            }

            return result;
        }

        private static void AddToJObject(Dictionary<string, JObject> knownProperties, JObject baseObject, string childPropertyDelimiter, string propertyName, JToken propertyValue)
        {
            JObject keyParent = baseObject;
            Stack<string> missingAncestors = new Stack<string>();
            foreach (var parent in GetMemberAncestors(propertyName, childPropertyDelimiter))
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

            keyParent = AddMissingAncestors(knownProperties, childPropertyDelimiter, keyParent, missingAncestors);

            if (IsArrayProperty(propertyName))
            {
                // Form array
                AppendArrayElement(baseObject, propertyName, childPropertyDelimiter, propertyValue);
            }
            else
            {
                keyParent.Add(GetMemberName(propertyName, childPropertyDelimiter), propertyValue);
            }
        }

        /// <param name="knownProperties"></param>
        /// <param name="childPropertyDelimiter"></param>
        /// <param name="knownAncestor">The parent of the first missing ancestor</param>
        /// <param name="missingAncestors">Must be sorted with the closest to the root first</param>
        private static JObject AddMissingAncestors(IDictionary<string, JObject> knownProperties, string childPropertyDelimiter, JObject knownAncestor, IEnumerable<string> missingAncestors)
        {
            JObject missingAncestorParent = knownAncestor;
            JObject missingAncestorValue = missingAncestorParent;

            foreach (var missingAncestor in missingAncestors)
            {
                if (IsArrayProperty(missingAncestor))
                {
                    // Parent becomes the current value - got to jump a generation
                    missingAncestorParent = missingAncestorValue;
                    missingAncestorValue = new JObject();

                    AppendArrayElement(missingAncestorParent, missingAncestor, childPropertyDelimiter, missingAncestorValue);
                }
                else
                {
                    missingAncestorValue = new JObject();

                    // Add to jObject
                    missingAncestorParent.Add(GetMemberName(missingAncestor, childPropertyDelimiter), missingAncestorValue);
                }

                //Add to known properties
                knownProperties.Add(missingAncestor, missingAncestorValue);
            }

            return missingAncestorValue;
        }

        private static void AppendArrayElement(JObject myParent, string currentArrayMemberName, string childPropertyDelimiter, JToken value)
        {
            // Is JArray - check if JArray already exists
            var jArrayMemberName = GetMemberName(currentArrayMemberName, childPropertyDelimiter);

            JArray jArray;
            if (myParent.TryGetValue(jArrayMemberName, out var token))
            {
                jArray = token as JArray;

                if (jArray is null)
                {
                    // Another property with the same name as the array, which is not part of the array, was in the json
                    throw new FormatException($"Could not cast to {nameof(JObject)} due to bad array formatting for {currentArrayMemberName}");
                }
            }
            else
            {
                jArray = new JArray();

                myParent.Add(jArrayMemberName, jArray);
            }

            jArray.Add(value);
        }

        /// <returns>Ancestors in descending order (child-most first, so a.b.c, then a.b, then a)</returns>
        private static IEnumerable<string> GetMemberAncestors(string fullyQualifiedName, string childPropertyDelimiter)
        {
            string parentName = GetParentFullName(fullyQualifiedName, childPropertyDelimiter);

            while (parentName != null)
            {
                yield return parentName;

                parentName = GetParentFullName(parentName, childPropertyDelimiter);
            }
        }

        /// <returns>Returns <see langword="null"/> if <paramref name="fullyQualifiedName"/> has no parents</returns>
        private static string GetParentFullName(string fullyQualifiedName, string childPropertyDelimiter)
        {
            var lastDelimiterIndex = fullyQualifiedName.LastIndexOf(childPropertyDelimiter,
                    StringComparison.Ordinal);

            if (lastDelimiterIndex < 0)
            {
                // No parent
                return null;
            }

            // Has parent
            return fullyQualifiedName.Substring(0, lastDelimiterIndex);
        }

        private static bool IsArrayProperty(string name)
        {
            return name.EndsWith("]");
        }

        private static string GetMemberName(string fullyQualifiedName, string childPropertyDelimiter)
        {
            var lastDelimiterIndex = fullyQualifiedName.LastIndexOf(childPropertyDelimiter,
                    StringComparison.Ordinal);

            if (lastDelimiterIndex >= 0)
            {
                // Is a child property
                var memberName = fullyQualifiedName.Substring(lastDelimiterIndex + childPropertyDelimiter.Length);

                if (IsArrayProperty(memberName))
                {
                    // Trim array off end
                    return memberName.Substring(0, memberName.LastIndexOf('['));
                }
                else
                {
                    return memberName;
                }
            }

            // Is already property name
            if (IsArrayProperty(fullyQualifiedName))
            {
                // Trim array off end
                return fullyQualifiedName.Substring(0, fullyQualifiedName.LastIndexOf('['));
            }
            else
            {
                return fullyQualifiedName;
            }
        }
    }
}
