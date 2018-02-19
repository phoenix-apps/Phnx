using System.IO;

namespace MarkSFrancis.Extensions.IO
{
    public static class StreamExtensions
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            byte[] data = new byte[stream.Length - stream.Position];

            stream.Read(data, 0, data.Length);

            return data;
        }
    }
}