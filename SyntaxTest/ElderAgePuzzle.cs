using System;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class ElderAgePuzzle
    {
        private readonly ConsoleIo _console;

        public ElderAgePuzzle(ConsoleIo console)
        {
            _console = console;
        }

        public void Run()
        {

            _console.WriteLine(ElderAge1(8, 5, 1, 100)); // 5
            _console.WriteLine(ElderAge2(8, 5, 1, 100)); // 5
            _console.WriteLine(ElderAge1(5, 45, 3, 1000007)); // 4323
            _console.WriteLine(ElderAge2(5, 45, 3, 1000007)); // 4323

            _console.WriteLine(ElderAge2(28827050410L, 35165045587L, 7109602, 13719506));
        }

        static long ElderAge1(long n, long m, long k, long newp)
        {
            long sum = 0;

            for (long x = 0; x < m; x++)
                for (long y = 0; y < n; y++)
                    sum += Math.Max(0, (x ^ y) - k);

            return sum % newp;
        }

        static long ElderAge2(long n, long m, long k, long newp)
        {
            var sum = (((n * (n - 1)) / 2) - (k * (n - k))) * m;

            return sum % newp;
        }
    }
}
