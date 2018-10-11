using Newtonsoft.Json.Linq;
using System;
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
        public static JObject From(Dictionary<string, string> propertyDictionary)
        {
            if (propertyDictionary is null)
            {
                throw new ArgumentNullException(nameof(propertyDictionary));
            }

            return JsonWrapper.FromDictionary(propertyDictionary);
        }

        /// <summary>
        /// Convert a property dictionary to a <see cref="JObject"/>
        /// </summary>
        /// <typeparam name="T">The type of object represented by <paramref name="propertyDictionary"/></typeparam>
        /// <param name="propertyDictionary">The property dictionary to convert</param>
        /// <returns><paramref name="propertyDictionary"/> as a <see cref="JObject"/></returns>
        public static T From<T>(Dictionary<string, string> propertyDictionary)
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
        public static Dictionary<string, string> To(object o)
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
        public static Dictionary<string, string> To(JObject jObj)
        {
            if (jObj is null)
            {
                throw new ArgumentNullException(nameof(jObj));
            }

            return JsonWrapper.ToDictionary(jObj);
        }
    }
}
