using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    /// <summary>
    /// Extensions for <see cref="object"/>, <see cref="string"/> and <see cref="T:byte[]"/> related to Binary and XML serialization
    /// </summary>
    public static class SerializeExtensions
    {
        private static readonly BinaryFormatter BinaryFormatter = new BinaryFormatter();

        public static byte[] SerializeBinary<T>(this T t)
        {
            using (var memoryStream = new MemoryStream())
            {
                SerializeBinary(t, memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static T DeserializeBinary<T>(this byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return memoryStream.DeserializeBinary<T>();
            }
        }

        public static void SerializeBinary<T>(this T t, Stream serializeTo)
        {
            BinaryFormatter.Serialize(serializeTo, t);
        }

        public static T DeserializeBinary<T>(this Stream serializeFrom)
        {
            return (T)BinaryFormatter.Deserialize(serializeFrom);
        }

        public static StringBuilder SerializeXml<T>(this T t)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, t);

                return stringWriter.GetStringBuilder();
            }
        }

        public static T DeserializeXml<T>(this string s)
        {
            var xml = new XmlSerializer(typeof(T));
            var reader = new StringReader(s);

            return (T)xml.Deserialize(reader);
        }
    }
}