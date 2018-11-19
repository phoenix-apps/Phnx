using System.IO;

namespace Phnx.Security.Tests.TestHelpers
{
    public class OneWayStream : Stream
    {
        public OneWayStream(bool output)
        {
            BaseStream = new MemoryStream();
            CanRead = output;
            CanWrite = !output;
        }

        public override bool CanRead { get; }

        public override bool CanSeek => BaseStream.CanSeek;

        public override bool CanWrite { get; }

        public override long Length => BaseStream.Position;

        public override long Position { get => BaseStream.Position; set => BaseStream.Position = value; }

        public MemoryStream BaseStream { get; }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return BaseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            BaseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            BaseStream.Write(buffer, offset, count);
        }
    }
}
