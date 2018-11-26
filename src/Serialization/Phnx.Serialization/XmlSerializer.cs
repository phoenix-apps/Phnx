using System;
using System.IO;
using System.Text;

namespace Phnx.Serialization
{
    using DotNetXmlSerializer = System.Xml.Serialization.XmlSerializer;

    /// <summary>
    /// XML (Extensible Markup Language) serialization for transferring a value and its members to and from a <see cref="string"/>
    /// </summary>
    public class XmlSerializer : ISerializer
    {
        /// <summary>
        /// Create a new <see cref="System.Xml.Serialization.XmlSerializer"/> for a given type
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <returns>An XML serializer</returns>
        protected DotNetXmlSerializer CreateSerializer<T>()
        {
            return new DotNetXmlSerializer(typeof(T));
        }

        /// <summary>
        /// Serialize an object, and append it to a stream
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <param name="outputStream">The stream to serialize the value to</param>
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

            var formatter = CreateSerializer<T>();

            formatter.Serialize(outputStream, value);
        }

        /// <summary>
        /// Serialize an object to a <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <returns><paramref name="value"/> serialized</returns>
        public string Serialize<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using (var stream = new MemoryStream())
            {
                Serialize(value, stream);

                var bytes = stream.ToArray();
                return Encoding.UTF8.GetString(bytes);
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
            if (inputStream is null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (!inputStream.CanRead)
            {
                throw new ArgumentException($"Cannot read from {inputStream}");
            }

            var formatter = CreateSerializer<T>();

            return (T)formatter.Deserialize(inputStream);
        }

        /// <summary>
        /// Deserialize an object from the data in a <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">The type of value to deserialize</typeparam>
        /// <param name="xml">The <see cref="string"/> to deserialize the value from</param>
        /// <returns>The value that was deserialized</returns>
        public T Deserialize<T>(string xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var xmlAsBytes = Encoding.UTF8.GetBytes(xml);

            using (var stream = new MemoryStream(xmlAsBytes))
            {
                return Deserialize<T>(stream);
            }
        }
    }
}
