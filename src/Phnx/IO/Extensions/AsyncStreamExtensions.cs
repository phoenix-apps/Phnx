using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Phnx.IO
{
    /// <summary>
    /// Extensions for <see cref="Stream"/> and <see cref="TextReader"/>
    /// </summary>
    public static class AsyncStreamExtensions
    {
        /// <summary>
        /// Writes <paramref name="textToWrite"/> to the <paramref name="stream"/>
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="textToWrite">The text to write to the stream</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support writing</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed</exception>
        public static async Task WriteAsync(this Stream stream, string textToWrite)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using var writer = new StreamWriter(stream);
            await writer.WriteAsync(textToWrite);
        }

        /// <summary>
        /// Writes <paramref name="textToWrite"/> to the <paramref name="stream"/>, in a specified encoding
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="textToWrite">The text to write to the stream</param>
        /// <param name="encoding">The encoding to use when converting <paramref name="textToWrite"/> to bytes</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support writing</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed</exception>
        public static async Task WriteAsync(this Stream stream, string textToWrite, Encoding encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using var writer = new StreamWriter(stream, encoding);
            await writer.WriteAsync(textToWrite);
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
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed</exception>
        public static Task<string> ReadToEndAsStringAsync(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamReader(stream).ReadToEndAsync();
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
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed</exception>
        public static Task<string> ReadToEndAsStringAsync(this Stream stream, Encoding encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamReader(stream, encoding).ReadToEndAsync();
        }

        /// <summary>
        /// Read to the end of the current stream, and return all the data read
        /// </summary>
        /// <param name="stream">The stream to read all data from</param>
        /// <returns>A <see cref="T:byte"/> containing all the data read from the <paramref name="stream"/></returns>
        /// <exception cref="ArgumentException">The sum of offset and count is larger than the buffer length</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed</exception>
        public static async Task<byte[]> ReadToEndAsync(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            byte[] data = new byte[stream.Length - stream.Position];

            await stream.ReadAsync(data, 0, data.Length);

            return data;
        }
    }
}
