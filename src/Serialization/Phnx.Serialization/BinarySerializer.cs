using Phnx.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Phnx.Serialization
{
    /// <summary>
    /// Binary serialization for transferring a value and its members to and from a <see cref="T:byte[]"/>
    /// </summary>
    public class BinarySerializer : ISerializer
    {
        private static readonly BinaryFormatter Formatter = new BinaryFormatter();

        /// <summary>
        /// Serialize an object, and append it to a stream
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <param name="outputStream">The stream to serialize the value to</param>
        public void Serialize<T>(T value, Stream outputStream)
        {
            Formatter.Serialize(outputStream, value);
        }

        /// <summary>
        /// Serialize an object to a <see cref="T:byte[]"/>
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <returns><paramref name="value"/> serialized</returns>
        public byte[] Serialize<T>(T value)
        {
            using (var memoryStream = new MemoryStream())
            {
                Serialize(value, memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deserialize an object from the data in a stream
        /// </summary>
        /// <typeparam name="T">The type of value to deserialize</typeparam>
        /// <param name="inputStream">The stream to deserialize the value from</param>
        /// <returns>The value that was deserialized</returns>
        public T Deserialize<T>(Stream inputStream)
        {
            return (T)Formatter.Deserialize(inputStream);
        }

        /// <summary>
        /// Deserialize an object from the data in a <see cref="T:byte[]"/>
        /// </summary>
        /// <typeparam name="T">The type of value to deserialize</typeparam>
        /// <param name="bytes">The <see cref="T:byte[]"/> to deserialize the value from</param>
        /// <returns>The value that was deserialized</returns>
        public T Deserialize<T>(byte[] bytes)
        {
            using (var memoryStream = bytes.ToMemoryStream())
            {
                return Deserialize<T>(memoryStream);
            }
        }
    }
}
