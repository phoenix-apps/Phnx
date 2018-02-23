using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main(string[] args)
        {
            if (Console.YesNo("Would you like to continue?"))
            {
                Console.WriteLine("Selected Yes");
            }
            else
            {
                Console.WriteLine("Selected No");
            }

            Console.ReadKey();
        }
    }
}
