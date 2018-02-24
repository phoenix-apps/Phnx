using MarkSFrancis.Console;
using MarkSFrancis.Random.Generator;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            RandomULongGenerator rndULong = new RandomULongGenerator();

            for (var genTimes = 0; genTimes < 10; genTimes++)
            {
                var val = rndULong.Get(ulong.MinValue, ulong.MaxValue);

                Console.WriteLine(val);
            }

            Console.ReadKey();
        }
    }
}
