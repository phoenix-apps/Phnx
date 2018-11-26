using Phnx.Collections;
using System;
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
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="outputStream"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="outputStream"/> cannot be written to</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"><typeparamref name="T"/> does not support serialization</exception>
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
                throw new ArgumentException($"{nameof(outputStream)} cannot be written to");
            }

            Formatter.Serialize(outputStream, value);
        }

        /// <summary>
        /// Serialize an object to a <see cref="T:byte[]"/>
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <returns><paramref name="value"/> serialized</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"><typeparamref name="T"/> does not support serialization</exception>
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
        /// <exception cref="ArgumentNullException"><paramref name="inputStream"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="inputStream"/> cannot be read from</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"><typeparamref name="T"/> does not support serialization</exception>
        public T Deserialize<T>(Stream inputStream)
        {
            if (inputStream is null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (!inputStream.CanRead)
            {
                throw new ArgumentException($"{nameof(inputStream)} cannot be read from");
            }

            return (T)Formatter.Deserialize(inputStream);
        }

        /// <summary>
        /// Deserialize an object from the data in a <see cref="T:byte[]"/>
        /// </summary>
        /// <typeparam name="T">The type of value to deserialize</typeparam>
        /// <param name="bytes">The <see cref="T:byte[]"/> to deserialize the value from</param>
        /// <returns>The value that was deserialized</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is <see langword="null"/></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"><typeparamref name="T"/> does not support serialization</exception>
        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            using (var memoryStream = bytes.ToMemoryStream())
            {
                return Deserialize<T>(memoryStream);
            }
        }
    }
}
