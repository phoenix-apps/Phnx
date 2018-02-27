using System;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Console;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            List<int> items = new List<int>{1, 2, 3};

            int ComparerFunc(int a, int b)
            {
                if (a % 2 == 0)
                {
                    if (b % 2 == 0)
                    {
                        return a.CompareTo(b);
                    }
                    return -1;
                }
                if (b % 2 == 0)
                {
                    return 1;
                }
                return a.CompareTo(b);
            }

            var asdf = Comparer<int>.Create(ComparerFunc);

            items = items.OrderBy(me => me, asdf).ToList();

            var insertAt = ~items.BinarySearchBy(10, asdf);

            items.Insert(insertAt, 10);

            Console.WriteCollection(items);

            Console.ReadKey();
        }
    }
}
