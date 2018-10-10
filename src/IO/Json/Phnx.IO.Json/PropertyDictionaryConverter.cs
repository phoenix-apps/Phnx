using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Phnx.IO.Json
{
    /// <summary>
    /// Helper methods related to converting <see cref="JObject"/>s
    /// </summary>
    public static class PropertyDictionaryConverter
    {
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
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <typeparam name="T">The type of object represented by <paramref name="propertyDictionary"/></typeparam>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        public static T FromPropertyDictionary<T>(Dictionary<string, string> propertyDictionary)
        {
            var jObject = FromPropertyDictionary(propertyDictionary);

            return jObject.ToObject<T>();
        }

        /// <summary>
        /// Convert an <see cref="object"/> to a property dictionary
        /// </summary>
        /// <param name="o">The <see cref="object"/> to convert</param>
        /// <returns><paramref name="o"/> as a property dictionary</returns>
        public static Dictionary<string, string> ToPropertyDictionary(object o)
        {
            var jObject = JObject.FromObject(o);

            return ToPropertyDictionary(jObject);
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
