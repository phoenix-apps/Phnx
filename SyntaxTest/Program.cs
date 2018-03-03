using System.Threading;
using System.Threading.Tasks;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();
        // private static object SyncContext = new object();

        static void Main()
        {
            int val = 0, incCount = 100000000;
            Task thd1 = new Task(() => IncrementLots(ref val, incCount / 2));
            Task thd2 = new Task(() => IncrementLots(ref val, incCount / 2));
            thd1.Start();
            thd2.Start();

            Task.WaitAll(thd1, thd2);

            Console.WriteLine("Value of val: ");
            Console.WriteLine(val);
            Console.WriteLine();

            Console.WriteLine("Value should be: ");
            Console.WriteLine(incCount);

            Console.ReadKey();
        }

        private static void IncrementLots(ref int value, int timesToIncrement)
        {
            for (int incrementCount = 0; incrementCount < timesToIncrement; ++incrementCount)
            {
                // Interlocked.Increment(ref value);
                // lock (SyncContext)
                // {
                ++value;
                // }
            }
        }
    }
}
