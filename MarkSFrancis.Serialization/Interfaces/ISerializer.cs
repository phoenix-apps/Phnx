using System.IO;

namespace MarkSFrancis.Serialization.Interfaces
{
    /// <summary>
    /// Represents a serializer for objects, such as XML, Json and binary
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize an object, and append it to a stream
        /// </summary>
        /// <typeparam name="T">The type of value to serialize</typeparam>
        /// <param name="value">The value to serialize</param>
        /// <param name="outputStream">The stream to serialize the value to</param>
        void Serialize<T>(T value, Stream outputStream);
        

        /// <summary>
        /// Deserialize an object from the data in a stream
        /// </summary>
        /// <typeparam name="T">The type of value to deserialize</typeparam>
        /// <param name="inputStream">The stream to deserialize the value from</param>
        /// <returns>The value that was deserialized</returns>
        T Deserialize<T>(Stream inputStream);
    }
}
