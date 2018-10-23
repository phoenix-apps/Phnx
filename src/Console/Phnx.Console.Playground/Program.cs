namespace Phnx.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        static void Main(string[] args)
        {
            ConsoleDemo.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }
    }
}
