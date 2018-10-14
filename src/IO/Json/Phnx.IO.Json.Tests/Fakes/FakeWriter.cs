using System.IO;
using System.Text;

namespace Phnx.IO.Json.Tests.Fakes
{
    public class FakeWriter : TextWriter
    {
        public FakeWriter()
        {
            IsOpen = true;
        }

        public bool IsOpen { get; private set; }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Close()
        {
            IsOpen = false;
        }
    }
}
