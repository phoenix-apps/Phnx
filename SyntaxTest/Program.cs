using MarkSFrancis;
using MarkSFrancis.Console;
using SyntaxTest.Demos;
using SyntaxTest.Demos.Interfaces;

namespace SyntaxTest
{
    class Program
    {
        private static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            Console.Title = "Syntax Test for " + nameof(MarkSFrancis);

            do
            {
                IDemo selectedDemo = GetSelectedDemo();

                Console.WriteLine("Starting demo...");

                selectedDemo.Run();

                Console.WriteLine("Demo finished");

            } while (Console.YesNo("Run another demo?"));
        }

        private static IDemo GetSelectedDemo()
        {
            var selectedDemo = Console.GetSelection(new[]
            {
                "Api Demo",
                "Threads Demo"
            }, "Select a demo to run");

            switch (selectedDemo)
            {
                case 1:
                    return new ApiClientDemo(Console);
                case 2:
                    return new ThreadsDemo(Console, ThreadsDemo.Mode.Synced);
                default:
                    throw ErrorFactory.Default.ArgumentOutOfRange(nameof(selectedDemo));
            }
        }
    }
}
