using MarkSFrancis;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            var converter = ConverterHelpers.GetDefaultConverter<string, int>();

            int converted = converter("1");

            Console.WriteLine(converted);

            Console.ReadKey();
        }
    }
}
