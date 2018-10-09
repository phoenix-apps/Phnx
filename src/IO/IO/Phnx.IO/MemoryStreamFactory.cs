using System;
using System.IO;
using System.Text;

namespace Phnx.IO
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
        /// <param name="defaultEncoding">The default text encoding to use</param>
        /// <exception cref="ArgumentNullException"><paramref name="defaultEncoding"/> was <see langword="null"/></exception>
        public MemoryStreamFactory(Encoding defaultEncoding)
        {
            DefaultEncoding = defaultEncoding ?? throw new ArgumentNullException(nameof(defaultEncoding));
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
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            byte[] byteArray = data is null ? new byte[0] : encoding.GetBytes(data);

            return new MemoryStream(byteArray);
        }
    }
}
