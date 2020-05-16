namespace Phnx.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        public static void Main()
        {
            ConsoleDemo.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }
    }
}
