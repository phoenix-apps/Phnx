using System;
using System.Collections.Concurrent;
using System.IO;

namespace Phnx.IO
{
    /// <summary>
    /// Provides a piped stream with both input and output
    /// </summary>
    public class PipeStream : Stream
    {
        private ConcurrentQueue<byte> data;

        /// <summary>
        /// Whether this stream can be read (always <see langword="true"/>)
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Whether this stream can seek (always <see langword="false"/>)
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Whether this stream can be written to (always <see langword="true"/>)
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// The total amount of data waiting to be read
        /// </summary>
        public override long Length
        {
            get
            {
                lock (data)
                {
                    return data.Count;
                }
            }
        }

        /// <summary>
        /// The current position of this <see cref="PipeStream"/> (always <code>0</code>)
        /// </summary>
        public override long Position
        {
            get => 0;
            set => throw new NotSupportedException($"Cannot seek in a {nameof(PipeStream)}");
        }

        /// <summary>
        /// The input as a <see cref="StreamWriter"/>
        /// </summary>
        public StreamWriter In { get; }

        /// <summary>
        /// The output as a <see cref="StreamReader"/>
        /// </summary>
        public StreamReader Out { get; }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with no data
        /// </summary>
        public PipeStream()
        {
            data = new ConcurrentQueue<byte>();

            In = new StreamWriter(this)
            {
                AutoFlush = true
            };

            Out = new StreamReader(this);
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances <see cref="Out"/> by the number of bytes read
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream has been reached</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int readCount = 0;

            lock (data)
            {
                for (; readCount < count; ++readCount)
                {
                    if (!data.TryDequeue(out var dataEntry))
                    {
                        break;
                    }
                    else
                    {
                        buffer[offset + readCount] = dataEntry;
                    }
                }
            }

            return readCount;
        }

        /// <summary>
        /// Seeking is not supported in a <see cref="PipeStream"/>, 
        /// </summary>
        /// <exception cref="NotSupportedException">Cannot seek in a <see cref="PipeStream"/></exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException($"Cannot seek in a {nameof(PipeStream)}");
        }

        /// <summary>
        /// This does nothing in <see cref="PipeStream"/>
        /// </summary>
        public override void SetLength(long value)
        {
            // Do nothing
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from buffer to the current stream</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream</param>
        /// <param name="count">The number of bytes to be written to the current stream</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="offset"/> is less than zero</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            lock (data)
            {
                for (int writeCount = 0; writeCount < count; ++writeCount)
                {
                    var valueToWrite = buffer[offset + writeCount];

                    data.Enqueue(valueToWrite);
                }
            }
        }
    }
}
