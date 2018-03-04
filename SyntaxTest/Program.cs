using MarkSFrancis.Console;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            new NameMapTest(Console).Run();

            Console.ReadKey();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console);
            threads.Run();
        }
    }
}
