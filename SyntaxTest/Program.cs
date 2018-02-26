using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            int[] items = {1, 2, 3};

            Console.WriteLine(items.IndexOf(0));

            Console.ReadKey();
        }
    }
}
