using System;
using System.Threading;

namespace Phnx.Console.Playground
{
    public static class ConsoleDemo
    {
        private static readonly ConsoleHelper _console = new ConsoleHelper();

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
            _console.WriteLineInColor("Playground", ConsoleColor.Gray);
            _console.WriteLineInColor(new string('_', System.Console.WindowWidth), ConsoleColor.Gray);

            return _console.YesNo("Start demo?");
        }

        private static void TestUndoAndClear()
        {
            var infoMessage = "Testing undo writeline, please wait...";
            _console.WriteLine(infoMessage);

            var fullLine = new string('_', System.Console.WindowWidth);
            _console.WriteLineInColor(fullLine, ConsoleColor.Yellow);

            Wait(500);
            _console.UndoWriteLine(fullLine);
            _console.UndoWriteLine(infoMessage);

            infoMessage = "Testing clear current line, please wait...";
            _console.WriteLine(infoMessage);

            _console.WriteInColor(fullLine, ConsoleColor.Yellow);
            Wait(500);
            _console.UndoWrite(fullLine);
            _console.UndoWriteLine(infoMessage);
        }

        private static void DoMath()
        {
            var result = _console.GetInt("Please enter an integer:");
            _console.NewLine();

            _console.WriteLine($"{result}² = {Math.Pow(result, 2)}");
            _console.NewLine();
            _console.NewLine();

            if (!_console.YesNo("Run progress bar?"))
            {
                return;
            }

            using (var progress = _console.ProgressBar(100, d => "Testing progress bar..."))
            {
                // Simulate faulting at 73%
                while (progress.Progress < 73)
                {
                    progress.Progress++;
                    Thread.Sleep(25);
                }
            }

            _console.WriteError("Testing progress bar error state is complete");
        }

        private static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
