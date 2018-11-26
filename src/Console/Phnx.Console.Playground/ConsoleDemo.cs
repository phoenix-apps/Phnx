using System;
using System.Threading;

namespace Phnx.Console.Playground
{
    public static class ConsoleDemo
    {
        private static ConsoleHelper Console = new ConsoleHelper();

        public static void Run()
        {
            Intro();
            TestUndoAndClear();
            if (!DoMath()) return;
        }

        private static void Intro()
        {
            Console.WriteInfo("Welcome to the playground application for the Phnx.Console library");

            if (Console.YesNo("Clear the intro message?"))
            {
                Console.UndoWriteLine(2);
            }
            else
            {
                Console.UndoWriteLine();
            }
        }

        private static void TestUndoAndClear()
        {
            var infoMessage = "Testing undo writeline, please wait...";
            Console.WriteLine(infoMessage);

            var fullLine = new string('_', System.Console.BufferWidth);
            Console.WriteLine(fullLine);

            Wait(500);
            Console.UndoWriteLine(infoMessage, fullLine);

            infoMessage = "Testing clear current line, please wait...";
            Console.WriteLine(infoMessage);

            Console.Write(fullLine);
            Wait(500);
            Console.ClearCurrentLine(fullLine);
            Console.UndoWriteLine();
        }

        private static bool DoMath()
        {
            var result = Console.GetInt("Please enter an integer to square:");

            using (var progress = Console.ProgressBar(100, d => "Powering up math cells...", true))
            {
                // Simulate faulting at 73%
                for (int timer = 1; timer < 74; ++timer)
                {
                    progress.Progress = timer;
                    Thread.Sleep(25);
                }
            }

            Console.WriteError("Critical failure with math cells: Temperature above safe levels, emergency abort");

            if (!Console.YesNo("Continue?")) return false;

            Console.WriteWarning("Falling back to legacy algorithmic modelling");
            SlowlyWriteText("..........");

            Console.NewLine(2);

            Console.WriteLine($"{result}² = {Math.Pow(result, 2)}");
            Console.NewLine();

            return true;
        }

        private static void SlowlyWriteText(string text)
        {
            foreach (var letter in text)
            {
                Console.Write(letter);
                Wait(250);
            }
        }

        private static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
