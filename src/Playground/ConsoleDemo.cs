using Phnx.Console;
using System;
using System.Threading;

namespace Playground
{
    public static class ConsoleDemo
    {
        private static ConsoleHelper Console => Program.Console;

        public static void Run()
        {
            if (!Intro())
            {
                return;
            }

            TestUndoAndClear();
            DoMath();
        }

        private static bool Intro()
        {
            Console.WriteLine("Playground", ConsoleColor.Gray);
            Console.WriteLine(new string('_', System.Console.WindowWidth), ConsoleColor.Gray);

            return Console.YesNo("Start demo?");
        }

        private static void TestUndoAndClear()
        {
            var infoMessage = "Testing undo writeline, please wait...";
            Console.WriteLine(infoMessage);

            var fullLine = new string('_', System.Console.WindowWidth);
            Console.WriteLine(fullLine, ConsoleColor.Yellow);

            Wait(500);
            Console.UndoWriteLine(fullLine);
            Console.UndoWriteLine(infoMessage);

            infoMessage = "Testing clear current line, please wait...";
            Console.WriteLine(infoMessage);

            Console.Write(fullLine, ConsoleColor.Yellow);
            Wait(500);
            Console.UndoWrite(fullLine);
            Console.UndoWriteLine(infoMessage);
        }

        private static void DoMath()
        {
            Console.Write("Please enter an integer: ");
            var result = Console.GetInt();
            Console.NewLine();

            Console.WriteLine($"{result}² = {Math.Pow(result, 2)}");
            Console.NewLine();
            Console.NewLine();

            if (!Console.YesNo("Run progress bar?"))
            {
                return;
            }

            using (var progress = Console.ProgressBar(100, d => "Testing progress bar..."))
            {
                // Simulate faulting at 73%
                while (progress.Progress < 73)
                {
                    progress.Progress++;
                    Thread.Sleep(25);
                }
            }

            Console.WriteError("Testing progress bar error state is complete");
        }

        private static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
