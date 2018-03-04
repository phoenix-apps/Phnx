using System.Threading;
using System.Threading.Tasks;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class ThreadsDemo
    {
        public ThreadsDemo(ConsoleIo console)
        {
            _console = console;
        }

        private readonly ConsoleIo _console;
        // private object _syncContext = new object();

        public void Run()
        {
            int val = 0, incCount = 100000000;
            Task thd1 = new Task(() => IncrementLots(ref val, incCount / 2));
            Task thd2 = new Task(() => IncrementLots(ref val, incCount / 2));
            thd1.Start();
            thd2.Start();

            Task.WaitAll(thd1, thd2);

            _console.WriteLine("Value of val: ");
            _console.WriteLine(val);
            _console.WriteLine();

            _console.WriteLine("Value should be: ");
            _console.WriteLine(incCount);
        }

        private void IncrementLots(ref int value, int timesToIncrement)
        {
            for (int incrementCount = 0; incrementCount < timesToIncrement; ++incrementCount)
            {
                // Interlocked.Increment(ref value);
                // lock (_syncContext)
                // {
                ++value;
                // }
            }
        }
    }
}
