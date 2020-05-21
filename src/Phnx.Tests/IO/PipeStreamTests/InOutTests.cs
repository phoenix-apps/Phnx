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
        public void ReadAndWriteWithInOut_ReadsAndWritesToPipe()
        {
            var expected = "abc";
            var pipe = new PipeStream();

            pipe.In.Write(expected);

            Assert.IsTrue(pipe.Length != 0);
            Assert.AreEqual(expected, pipe.Out.ReadToEnd());
        }
    }
}
