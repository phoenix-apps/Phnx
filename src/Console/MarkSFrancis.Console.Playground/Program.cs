using System;
using System.Threading;

namespace MarkSFrancis.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        static void Main(string[] args)
        {
            Console.WriteInfo("Welcome to the playground application for the MarkSFrancis.Console library");

            if (Console.YesNo("Clear the intro message?"))
            {
                Console.UndoWriteLine(2);
            }
            else
            {
                Console.UndoWriteLine();
            }

            var infoMessage = "Testing undo writeline, please wait...";
            Console.WriteLine(infoMessage);

            var fullLine = new string('_', System.Console.BufferWidth);
            Console.WriteLine(fullLine);

            Thread.Sleep(500);
            Console.UndoWriteLine(infoMessage, fullLine);

            infoMessage = "Testing clear current line, please wait...";
            Console.WriteLine(infoMessage);

            Console.Write(fullLine);
            Thread.Sleep(500);
            Console.ClearCurrentLine(fullLine);
            Console.UndoWriteLine();

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

            if (!Console.YesNo("Continue?")) return;

            Console.WriteWarning("Falling back to legacy algorithmic modelling");
            for (int index = 0; index < 10; ++index)
            {
                Console.Write('.');
                Thread.Sleep(250);
            }

            Console.NewLine(2);

            Console.WriteLine($"{result}² = {Math.Pow(result, 2)}");
            Console.NewLine();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }
    }
}
