using Newtonsoft.Json;
using Phnx.IO.Json.Streams;
using Phnx.Serialization.Interfaces;
using System.IO;
using System.Text;

namespace Phnx.Serialization
{
    /// <summary>
    /// Provides extension methods for serializing and deserializing objects and <see cref="T:byte[]"/>
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Serializes an object to bytes by converting to JSON, then converting the JSON to UTF-8 bytes
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="value">The object to serialize</param>
        /// <returns>The serialized value</returns>
        public static byte[] Serialize<T>(T value)
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
        /// <exception cref="System.ArgumentException">Object is not of type <typeparamref name="T"/></exception>
        public static T Deserialize<T>(byte[] bytes)
        {
            var jsonObject = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(jsonObject);
        }

        /// <summary>
        /// Serializes an object and appends it to a stream
        /// </summary>
        /// <typeparam name="T">The type of object to write</typeparam>
        /// <param name="value">The value to write</param>
        /// <param name="outputStream">The stream to output the serialized data to</param>
        public void Serialize<T>(T value, Stream outputStream)
        {
            using (var output = new JsonWriteStream(new StreamWriter(outputStream), false))
            {
                output.WriteObject(value);
            }
        }

        /// <summary>
        /// Deserializes the next object from a stream
        /// </summary>
        /// <typeparam name="T">The type of object to read</typeparam>
        /// <param name="inputStream">The stream to deserialize from</param>
        /// <returns>The deserialized value read from the stream</returns>
        /// <exception cref="System.ArgumentException">Object is not of type <typeparamref name="T"/></exception>
        public T Deserialize<T>(Stream inputStream)
        {
            using (var input = new JsonReadStream(new StreamReader(inputStream), false))
            {
                return input.ReadObject<T>();
            }
        }
    }
}
