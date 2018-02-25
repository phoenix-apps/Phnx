using MarkSFrancis;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            var converter = ConverterHelpers.GetDefaultConverter<asdf2, asdf>();

            var converted = converter(new asdf2());

            Console.WriteLine(converted.val);

            Console.ReadKey();
        }
    }

    class asdf
    {
        public string val { get; set; }
    }

    class asdf2 : asdf
    {
        public asdf2()
        {
            val = "a";
        }
    }
}
