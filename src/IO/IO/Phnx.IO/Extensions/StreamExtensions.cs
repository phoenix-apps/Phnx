using System;
using System.IO;

namespace Phnx.IO
{
    /// <summary>
    /// Extensions for <see cref="Stream"/> and <see cref="TextReader"/>
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Read to the end of the current stream, and return all the data read
        /// </summary>
        /// <param name="stream">The stream to read all data from</param>
        /// <returns>A <see cref="T:byte"/> containing all the data read from the <paramref name="stream"/></returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed</exception>
        public static byte[] ReadToEnd(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            byte[] data = new byte[stream.Length - stream.Position];

            stream.Read(data, 0, data.Length);

            return data;
        }

        /// <summary>
        /// Get whether this stream has been read to the end
        /// </summary>
        /// <param name="stream">The stream to check</param>
        /// <returns>Whether this stream has been read to the end</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed</exception>
        public static bool ReachedEnd(this Stream stream)
        {
            return stream.Position == stream.Length;
        }

        /// <summary>
        /// Get whether this text reader has been read to the end
        /// </summary>
        /// <param name="reader">The text reader to check</param>
        /// <returns>Whether this text reader has been read to the end</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the text reader was closed</exception>
        public static bool ReachedEnd(this TextReader reader)
        {
            return reader.Peek() == -1;
        }
    }
}
