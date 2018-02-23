using System.IO;
using System.Text;

namespace MarkSFrancis.IO.Tests
{
    public static class StreamHelper
    {
        public static MemoryStream ToStream(this string s)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(s);

            return new MemoryStream(byteArray);
        }

        public static StreamWriter CreateStreamWriter(out MemoryStream ms)
        {
            ms = new MemoryStream();
            return new StreamWriter(ms);
        }

        public static string ReadStream(Stream s)
        {
            s.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(s);
            return reader.ReadToEnd();
        }
    }
}
