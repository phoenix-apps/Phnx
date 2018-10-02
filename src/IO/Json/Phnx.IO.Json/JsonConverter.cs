using Newtonsoft.Json;

namespace Phnx.IO.Json
{
    /// <summary>
    /// Provides helpers for converting between json text and objects
    /// </summary>
    public static class JsonConverter
    {
        /// <summary>
        /// Convert a json string to an object
        /// </summary>
        /// <typeparam name="T">The type of object to convert the json to</typeparam>
        /// <param name="json">The json to convert</param>
        /// <returns><paramref name="json"/> deserialized to an object</returns>
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Convert an object to a json string
        /// </summary>
        /// <param name="data">The object to serialize to json</param>
        /// <returns><paramref name="data"/> serialized to a json string</returns>
        public static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
