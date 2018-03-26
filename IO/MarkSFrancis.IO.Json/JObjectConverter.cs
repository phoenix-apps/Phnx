using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json
{
    /// <summary>
    /// Helper methods related to converting <see cref="JObject"/>s
    /// </summary>
    public static class JObjectConverter
    {
        /// <summary>
        /// The string inserted between child and parent properties in a property dictionary
        /// </summary>
        public const string ChildPropertyDelimiter = JsonWrapper.ChildPropertyDelimiter;

        /// <summary>
        /// Serialize an object to a <see cref="JObject"/>
        /// </summary>
        /// <param name="data">The object to convert</param>
        /// <returns><paramref name="data"/> as a <see cref="JObject"/></returns>
        public static JObject FromObject(object data)
        {
            return JObject.FromObject(data);
        }

        /// <summary>
        /// Convert Json to a <see cref="JObject"/>
        /// </summary>
        /// <param name="json">The json to convert</param>
        /// <returns><paramref name="json"/> as a <see cref="JObject"/></returns>
        public static JObject FromJson(string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        public static JObject FromPropertyDictionary(Dictionary<string, string> propertyDictionary)
        {
            return JsonWrapper.Wrap(propertyDictionary);
        }

        /// <summary>
        /// Deserialize a <see cref="JObject"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <param name="jObject">The <see cref="JObject"/> to deserialize</param>
        /// <returns><paramref name="jObject"/> deserialized</returns>
        public static T ToObject<T>(JObject jObject)
        {
            return jObject.ToObject<T>();
        }

        /// <summary>
        /// Serialize a <see cref="JObject"/> to a Json string
        /// </summary>
        /// <param name="jObject">The <see cref="JObject"/> to serialize</param>
        /// <returns>The serialized <paramref name="jObject"/></returns>
        public static string ToJson(JObject jObject)
        {
            return jObject.ToString();
        }

        /// <summary>
        /// Convert a <see cref="JObject"/> to a property dictionary
        /// </summary>
        /// <param name="jObj">The <see cref="JObject"/> to convert</param>
        /// <returns><paramref name="jObj"/> as a property dictionary</returns>
        public static Dictionary<string, string> ToPropertyDictionary(JObject jObj)
        {
            return JsonWrapper.Unwrap(jObj);
        }
    }
}
