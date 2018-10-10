using System;
using System.IO;
using System.Text;

namespace Phnx.IO
{
    /// <summary>
    /// Extensions for <see cref="Stream"/> and <see cref="TextReader"/>
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Writes <paramref name="textToWrite"/> to the <paramref name="stream"/>
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="textToWrite">The text to write to the stream</param>
        public static void Write(this Stream stream, string textToWrite)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var writer = new StreamWriter(stream);

            if (textToWrite is null)
            {
                writer.Flush();
                return;
            }

            writer.Write(textToWrite);
            writer.Flush();
        }

        /// <summary>
        /// Writes <paramref name="textToWrite"/> to the <paramref name="stream"/>, in a specified encoding
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="textToWrite">The text to write to the stream</param>
        /// <param name="encoding">The encoding to use when converting <paramref name="textToWrite"/> to bytes</param>
        public static void Write(this Stream stream, string textToWrite, Encoding encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var writer = new StreamWriter(stream, encoding);

            if (textToWrite is null)
            {
                writer.Flush();
                return;
            }

            writer.Write(textToWrite);
            writer.Flush();
        }

        /// <summary>
        /// Read to the end of the current stream, and return all the data as a string, auto-detecting the encoding from byte marks in the stream
        /// </summary>
        /// <param name="stream">The stream to read all data from</param>
        /// <returns>A <see cref="string"/> containing all the data read from the <paramref name="stream"/></returns>
        /// <exception cref="ArgumentException"><paramref name="stream"/> does not support reading</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed</exception>
        public static string ReadToEndAsString(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamReader(stream).ReadToEnd();
        }

        /// <summary>
        /// Read to the end of the current stream, and return all the data as a string
        /// </summary>
        /// <param name="stream">The stream to read all text from</param>
        /// <param name="encoding">The character encoding to use</param>
        /// <returns>A <see cref="string"/> containing all the data read from the <paramref name="stream"/></returns>
        /// <exception cref="ArgumentException"><paramref name="stream"/> does not support reading</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed</exception>
        public static string ReadToEndAsString(this Stream stream, Encoding encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamReader(stream, encoding).ReadToEnd();
        }

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
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.Position == stream.Length;
        }
    }
}
