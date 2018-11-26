using Newtonsoft.Json;
using Phnx.IO.Json;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Phnx.Serialization
{
    /// <summary>
    /// Provides extension methods for serializing and deserializing objects and <see cref="T:byte[]"/>
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Serializes an object to JSON
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="value">The object to serialize</param>
        /// <returns>The serialized value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static string Serialize<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            string json = JsonConvert.SerializeObject(value);

            return json;
        }

        /// <summary>
        /// Deserializes an object from JSON to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of object to deserialized to</typeparam>
        /// <param name="json">The bytes containing the serialized data</param>
        /// <returns>The deserialized value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="json"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidCastException">Object is not of type <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException"><paramref name="json"/> is not a valid JSON object</exception>
        public static T Deserialize<T>(string json)
        {
            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Serializes an object and appends it to a stream
        /// </summary>
        /// <typeparam name="T">The type of object to write</typeparam>
        /// <param name="value">The value to write</param>
        /// <param name="outputStream">The stream to output the serialized data to</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="outputStream"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="outputStream"/> cannot be written to</exception>
        public void Serialize<T>(T value, Stream outputStream)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (outputStream is null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            if (!outputStream.CanWrite)
            {
                throw new ArgumentException($"Cannot write to {outputStream}");
            }

            using (var output = new JsonStreamWriter(new StreamWriter(outputStream), false))
            {
                output.Write(value);
            }
        }

        /// <summary>
        /// Deserializes the next object from a stream
        /// </summary>
        /// <typeparam name="T">The type of object to read</typeparam>
        /// <param name="inputStream">The stream to deserialize from</param>
        /// <returns>The deserialized value read from the stream</returns>
        /// <exception cref="ArgumentNullException"><paramref name="inputStream"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="inputStream"/> cannot be read from</exception>
        /// <exception cref="InvalidCastException">Object is not of type <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException"><paramref name="inputStream"/> does not contain a valid JSON object</exception>
        public T Deserialize<T>(Stream inputStream)
        {
            if (inputStream is null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (!inputStream.CanRead)
            {
                throw new ArgumentException($"Cannot read from {inputStream}");
            }

            using (var input = new JsonStreamReader(new StreamReader(inputStream), false))
            {
                return input.ReadObject<T>();
            }
        }
    }
}
