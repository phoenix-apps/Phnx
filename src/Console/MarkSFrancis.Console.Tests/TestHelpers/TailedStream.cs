using System;
using System.Collections.Generic;
using System.IO;

namespace MarkSFrancis.Console.Tests.TestHelpers
{
    public class TailedStream : Stream
    {
        private Queue<byte> data;
        private Queue<byte> flushBuffer;
        private readonly bool requireFlush;

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

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

        public override long Position
        {
            get => Length;
            set => throw new NotSupportedException($"Cannot seek in a {nameof(TailedStream)}");
        }

        public StreamWriter Head { get; }

        public StreamReader Tail { get; }

        /// <param name="requireFlush">Set to <see langword="true"/> when you want to use a buffer. This helps for testing <see cref="Stream.Flush"/> being called after write operations</param>
        public TailedStream(bool requireFlush = true)
        {
            data = new Queue<byte>();
            flushBuffer = new Queue<byte>();

            Head = new StreamWriter(this);
            Head.AutoFlush = !requireFlush;

            Tail = new StreamReader(this);
            this.requireFlush = requireFlush;
        }

        public override void Flush()
        {
            // Do nothing
            lock (flushBuffer)
            {
                lock (data)
                {
                    while (flushBuffer.Count > 0)
                    {
                        data.Enqueue(flushBuffer.Dequeue());
                    }
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int readCount = 0;

            lock (data)
            {
                for (; readCount < count && data.Count > 0; ++readCount)
                {
                    buffer[offset + readCount] = data.Dequeue();
                }
            }

            return readCount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException($"Cannot seek in a {nameof(TailedStream)}");
        }

        public override void SetLength(long value)
        {
            // Do nothing
        }

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

            for (int writeCount = 0; writeCount < count; ++writeCount)
            {
                var valueToWrite = buffer[offset + writeCount];

                if (requireFlush)
                {
                    lock (flushBuffer)
                    {
                        flushBuffer.Enqueue(valueToWrite);
                    }
                }
                else
                {
                    lock (data)
                    {
                        data.Enqueue(valueToWrite);
                    }
                }
            }
        }
    }
}
