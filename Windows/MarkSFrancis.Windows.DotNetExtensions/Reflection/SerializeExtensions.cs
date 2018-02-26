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
        public static byte[] SerializeBinary<T>(this T t)
        {
            var binaryWrite = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryWrite.Serialize(memoryStream, t);
                return memoryStream.ToArray();
            }
        }

        public static T DeserializeBinary<T>(this byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                var binaryRead = new BinaryFormatter();
                return (T)binaryRead.Deserialize(memoryStream);
            }
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