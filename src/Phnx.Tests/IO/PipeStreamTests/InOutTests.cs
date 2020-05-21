using NUnit.Framework;

namespace Phnx.IO.Tests.PipeStreamTests
{
    public class InOutTests
    {
        [Test]
        public void In_UsesPipeAsBaseStream()
        {
            var pipe = new PipeStream();

            Assert.AreEqual(pipe, pipe.In.BaseStream);
        }

        [Test]
        public void Out_UsesPipeAsBaseStream()
        {
            var pipe = new PipeStream();

            Assert.AreEqual(pipe, pipe.Out.BaseStream);
        }

        [Test]
        public void WriteToIn_WritesToPipe()
        {
            var expected = new byte[] { 1, 26, 212, 61, 2 };
            var pipe = new PipeStream();

            pipe.In.Write(expected);

            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }

        [Test]
        public void ReadFromOut_ReadsFromPipe()
        {
            var expected = new byte[] { 1, 26, 212, 61, 2 };
            var pipe = new PipeStream();

            pipe.Out.BaseStream.Write(expected);

            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }
    }
}
