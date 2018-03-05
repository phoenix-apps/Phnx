using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarkSFrancis.IO.Factory
{
    /// <summary>
    /// A factory for creating new instances of <see cref="MemoryStream"/>
    /// </summary>
    public class MemoryStreamFactory
    {
        /// <summary>
        /// The default encoding to use when creating new streams
        /// </summary>
        public Encoding DefaultEncoding { get; }

        /// <summary>
        /// Create an instance of <see cref="MemoryStreamFactory"/> with a default encoding of <see cref="Encoding.UTF8"/>
        /// </summary>
        public MemoryStreamFactory()
        {
            DefaultEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Create an instance of <see cref="MemoryStreamFactory"/> with a custom default encoding
        /// </summary>
        public MemoryStreamFactory(Encoding defaultEncoding)
        {
            DefaultEncoding = defaultEncoding;
        }

        /// <summary>
        /// Create a new empty <see cref="MemoryStream"/>
        /// </summary>
        /// <returns>A new empty <see cref="MemoryStream"/></returns>
        public MemoryStream Create()
        {
            return new MemoryStream();
        }

        /// <summary>
        /// Create a new <see cref="MemoryStream"/> with a <see cref="string"/> encoded using the <see cref="DefaultEncoding"/>
        /// </summary>
        /// <param name="data">The data to populate the new <see cref="MemoryStream"/> with</param>
        /// <returns>A new <see cref="MemoryStream"/> with a <see cref="string"/> encoded using the <see cref="DefaultEncoding"/></returns>
        public MemoryStream Create(string data)
        {
            return Create(data, DefaultEncoding);
        }

        /// <summary>
        /// Create a new <see cref="MemoryStream"/> with an encoded <see cref="string"/>
        /// </summary>
        /// <param name="data">The data to populate the new <see cref="MemoryStream"/> with</param>
        /// <param name="encoding">The encoding to use to load <paramref name="data"/> into the new stream</param>
        /// <returns>A new <see cref="MemoryStream"/> with an encoded <see cref="string"/></returns>
        public MemoryStream Create(string data, Encoding encoding)
        {
            byte[] byteArray = encoding.GetBytes(data);

            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Create a new <see cref="MemoryStream"/> with an encoded <see cref="IEnumerable{T}"/>. The records will be delimited by <see cref="Environment.NewLine"/>
        /// </summary>
        /// <param name="data">The data to populate the new <see cref="MemoryStream"/> with</param>
        /// <returns>A new <see cref="MemoryStream"/> with an encoded <see cref="string"/> of the data in <paramref name="data"/> delimited by an <see cref="Environment.NewLine"/></returns>
        public MemoryStream Create(IEnumerable<string> data)
        {
            return Create(data, Environment.NewLine);
        }

        /// <summary>
        /// Create a new <see cref="MemoryStream"/> with an encoded <see cref="IEnumerable{T}"/>. The records will be delimited by <paramref name="delimiter"/>
        /// </summary>
        /// <param name="data">The data to populate the new <see cref="MemoryStream"/> with</param>
        /// <param name="delimiter">The delimiter to use between each record</param>
        /// <returns>A new <see cref="MemoryStream"/> with an encoded <see cref="string"/> of the data in <paramref name="data"/> delimited by <paramref name="delimiter"/></returns>
        public MemoryStream Create(IEnumerable<string> data, string delimiter)
        {
            return Create(data, delimiter, DefaultEncoding);
        }

        /// <summary>
        /// Create a new <see cref="MemoryStream"/> with an encoded <see cref="IEnumerable{T}"/>. The records will be delimited by <paramref name="delimiter"/>
        /// </summary>
        /// <param name="data">The data to populate the new <see cref="MemoryStream"/> with</param>
        /// <param name="delimiter">The delimiter to use between each record</param>
        /// <param name="encoding">The encoding to use to load each record in <paramref name="data"/>, and the delimiter(s) into the new stream</param>
        /// <returns>A new <see cref="MemoryStream"/> with an encoded <see cref="string"/> of the data in <paramref name="data"/> delimited by <paramref name="delimiter"/></returns>
        public MemoryStream Create(IEnumerable<string> data, string delimiter, Encoding encoding)
        {
            string joinedData = string.Join(delimiter, data);

            return Create(joinedData, encoding);
        }
    }
}
