using System.IO;

namespace Phnx.IO.Json.Tests.Fakes
{
    public class FakeReader : TextReader
    {
        public FakeReader()
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
