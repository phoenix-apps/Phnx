using MarkSFrancis.Console;
using MarkSFrancis.Drawing;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            var color = new Color
            {
                Red = 255
            };

            Console.WriteLine(color.ToHexColor());

            Console.ReadKey();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
            threads.Run();
        }
    }
}
