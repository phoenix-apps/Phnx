using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarkSFrancis.IO.Factory
{
    public class MemoryStreamFactory
    {
        protected readonly Encoding DefaultEncoding = Encoding.UTF8;

        public MemoryStream Create()
        {
            return new MemoryStream();
        }

        public MemoryStream Create(string data)
        {
            return Create(data, DefaultEncoding);
        }

        public MemoryStream Create(string data, Encoding encoding)
        {
            byte[] byteArray = encoding.GetBytes(data);

            return new MemoryStream(byteArray);
        }

        public MemoryStream Create(IEnumerable<string> data)
        {
            return Create(data, Environment.NewLine);
        }

        public MemoryStream Create(IEnumerable<string> data, string delimiter)
        {
            return Create(data, delimiter, DefaultEncoding);
        }

        public MemoryStream Create(IEnumerable<string> data, string delimiter, Encoding encoding)
        {
            string joinedData = string.Join(delimiter, data);

            return Create(joinedData, encoding);
        }
    }
}
