using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phnx.IO
{
    /// <summary>
    /// Provides a piped stream with both input and output, where the output is "pulled" when the reader is ready
    /// </summary>
    public class PipeStream : Stream
    {
        /// <summary>
        /// The backing data store for the stream
        /// </summary>
        private Queue<byte> data;

        /// <summary>
        /// Whether this stream can be read (always <see langword="true"/>)
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Whether this stream can be written to (always <see langword="true"/>)
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Whether this stream can seek (always <see langword="false"/>)
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Represents an event for data being written to a <see cref="PipeStream"/>
        /// </summary>
        /// <param name="sender">The stream that was written to</param>
        public delegate void DataWrittenEvent(PipeStream sender);

        /// <summary>
        /// The event called when data is written
        /// </summary>
        public event DataWrittenEvent DataWritten;

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
            data = new Queue<byte>();

            In = new StreamWriter(this)
            {
                AutoFlush = true
            };

            Out = new StreamReader(this);
        }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with some initial data
        /// </summary>
        /// <param name="initialData">The data to initialise the <see cref="PipeStream"/> with</param>
        public PipeStream(string initialData) :
            this()
        {
            In.Write(initialData);
        }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with some initial data
        /// </summary>
        /// <param name="initialData">The data to initialise the <see cref="PipeStream"/> with</param>
        /// <exception cref="ArgumentNullException"><paramref name="initialData"/> is <see langword="null"/></exception>
        public PipeStream(byte[] initialData) :
            this()
        {
            if (initialData is null)
            {
                throw new ArgumentNullException(nameof(initialData));
            }

            Write(initialData, 0, initialData.Length);
        }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with some initial data loaded from a buffer
        /// </summary>
        /// <param name="buffer">The data to initialise the <see cref="PipeStream"/> with</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream</param>
        /// <param name="count">The number of bytes to be written to the current stream</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> or <paramref name="offset"/> is less than zero</exception>
        public PipeStream(byte[] buffer, int offset, int count) :
            this()
        {
            Write(buffer, offset, count);
        }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with custom text encoding
        /// </summary>
        /// <param name="encoding">The text encoding to use</param>
        public PipeStream(Encoding encoding)
        {
            data = new Queue<byte>();

            In = new StreamWriter(this, encoding)
            {
                AutoFlush = true
            };

            Out = new StreamReader(this, encoding);
        }

        /// <summary>
        /// Create a new <see cref="PipeStream"/> with some initial data in custom text encoding
        /// </summary>
        /// <param name="initialData">The data to initialise the <see cref="PipeStream"/> with</param>
        /// <param name="encoding">The text encoding to use</param>
        public PipeStream(string initialData, Encoding encoding) :
            this(encoding)
        {
            In.Write(initialData);
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
                for (; readCount < count && data.Count > 0; ++readCount)
                {
                    var dataEntry = data.Dequeue();

                    buffer[offset + readCount] = dataEntry;
                }
            }

            return readCount;
        }

        /// <summary>
        /// Seeking is not supported in a <see cref="PipeStream"/>
        /// </summary>
        /// <exception cref="NotSupportedException">Cannot seek in a <see cref="PipeStream"/></exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException($"Cannot seek in a {nameof(PipeStream)}");
        }

        /// <summary>
        /// Setting the length is not supported in a <see cref="PipeStream"/>
        /// </summary>
        /// <exception cref="NotSupportedException">Cannot set the length of a <see cref="PipeStream"/></exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException($"Cannot set the length of a {nameof(PipeStream)}");
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from buffer to the current stream</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream</param>
        /// <param name="count">The number of bytes to be written to the current stream</param>
        /// <exception cref="ArgumentException">The sum of offset and count is greater than the buffer length</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="count"/> or <paramref name="offset"/> is less than zero</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(offset));
            }
            if (count < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(count));
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentException($"{nameof(offset)} and {nameof(count)} combined were greater than the number of elements in {nameof(buffer)}");
            }

            lock (data)
            {
                for (int writeCount = 0; writeCount < count && offset + writeCount < buffer.Length; ++writeCount)
                {
                    var valueToWrite = buffer[offset + writeCount];

                    data.Enqueue(valueToWrite);
                }
            }

            DataWritten?.Invoke(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="PipeStream"/> and optionally releases the managed resources
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            // TODO Add the idea of being disposed

            if (data != null)
            {
                lock (data)
                {
                    data.Clear();
                }
            }

            base.Dispose(disposing);
        }
    }
}
