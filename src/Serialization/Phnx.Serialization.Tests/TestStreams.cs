using System;
using System.IO;

namespace Phnx.Serialization.Tests
{
    public class TestStream : MemoryStream
    {
        public TestStream(bool canRead = true, bool canWrite = true)
        {
            CanRead = canRead;
            CanWrite = canWrite;
        }

        public TestStream(byte[] buffer, bool canRead = true, bool canWrite = true) : base(buffer)
        {
            CanRead = canRead;
            CanWrite = canWrite;
        }

        public override bool CanRead { get; }

        public override bool CanWrite { get; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead)
            {
                throw new InvalidOperationException("Cannot read");
            }

            return base.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite)
            {
                throw new InvalidOperationException("Cannot write");
            }

            base.Write(buffer, offset, count);
        }
    }
}
