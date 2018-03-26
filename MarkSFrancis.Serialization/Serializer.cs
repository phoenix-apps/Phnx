using System.Text;
using Newtonsoft.Json;

namespace MarkSFrancis.Serialization
{
    /// <summary>
    /// Provides extension methods for serializing and deserializing objects and <see cref="T:byte[]"/>
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes an object to bytes by converting to JSON, then converting the JSON to UTF-8 bytes
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="value">The object to serialize</param>
        /// <returns>The serialized value</returns>
        public static byte[] Serialize<T>(this T value)
        {
            string jsonObject = JsonConvert.SerializeObject(value);

            return Encoding.UTF8.GetBytes(jsonObject);
        }

        /// <summary>
        /// Deserializes an object from bytes by converting from UTF-8 bytes to a JSON string, then to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of object to deserialized to</typeparam>
        /// <param name="bytes">The bytes containing the serialized data</param>
        /// <returns>The deserialized value</returns>
        public static T Deserialize<T>(this byte[] bytes)
        {
            var jsonObject = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(jsonObject);
        }
    }
}
