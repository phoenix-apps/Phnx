using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            new ElderAgePuzzle(Console).Run();
            // RunThreadsDemo();

            Console.ReadKey();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
            threads.Run();
        }
    }
}
