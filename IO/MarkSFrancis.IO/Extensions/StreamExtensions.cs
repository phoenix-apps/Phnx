using System.IO;

namespace MarkSFrancis.IO.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Stream"/> and <see cref="TextReader"/>
    /// </summary>
    public static class StreamExtensions
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            byte[] data = new byte[stream.Length - stream.Position];

            stream.Read(data, 0, data.Length);

            return data;
        }

        public static bool ReachedEnd(this Stream stream)
        {
            return stream.Position == stream.Length;
        }

        public static bool ReachedEnd(this TextReader reader)
        {
            return reader.Peek() == -1;
        }
    }
}