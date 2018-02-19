using MarkSFrancis.Console;
using MarkSFrancis.DotNetExtensions.Collections;
using MarkSFrancis.DotNetExtensions.IO;

namespace SyntaxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleIo consoleIo = new ConsoleIo();

            var test = new byte[] {1, 2, 3, 4, 5};

            byte[] data = test.ToMemoryStream().ReadToEnd();

            consoleIo.WriteCollection(test);
            consoleIo.WriteCollection(data);

            consoleIo.Pause();
        }
    }
}
