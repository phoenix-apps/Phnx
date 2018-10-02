using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Phnx.Serialization.Extensions
{
    /// <summary>
    /// Extensions for <see cref="object"/> related to serialization
    /// </summary>
    public static class ObjectExtensions
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

        /// <summary>
        /// Deep clones an object
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="valueToCopy">The object to serialize</param>
        /// <returns>A deep copy of <paramref name="valueToCopy"/></returns>
        public static T DeepCopy<T>(this T valueToCopy)
        {
            byte[] copy = Serialize(valueToCopy);

            return Deserialize<T>(copy);
        }

        /// <summary>
        /// Shallow clones an object, copying its members
        /// </summary>
        /// <typeparam name="T">The type of object to shallow clone</typeparam>
        /// <param name="valueToCopy">The object to shallow clone</param>
        /// <returns>A shallow copy of <paramref name="valueToCopy"/></returns>
        public static T ShallowCopy<T>(this T valueToCopy)
        {
            var cloneMethod = typeof(T).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

            var clone = (T)cloneMethod.Invoke(valueToCopy, new object[0]);

            return clone;
        }
    }
}
