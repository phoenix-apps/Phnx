using System.IO;
using MarkSFrancis.Console;
using MarkSFrancis.IO.Factory;
using MarkSFrancis.IO.Json.Streams;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            Console.ReadKey();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
            threads.Run();
        }
    }
}
