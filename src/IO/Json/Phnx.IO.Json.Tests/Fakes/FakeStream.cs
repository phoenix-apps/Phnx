using System.IO;

namespace Phnx.IO.Json.Tests.Fakes
{
    public class FakeStream : TextReader
    {
        public FakeStream()
        {
            IsOpen = true;
        }

        public bool IsOpen { get; private set; }

        public override void Close()
        {
            IsOpen = false;
        }
    }
}
