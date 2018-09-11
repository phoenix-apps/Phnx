namespace MarkSFrancis.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        static void Main(string[] args)
        {
            Console.WriteLine("Test...");

            var result = Console.GetInt("Why are you like this?");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }
    }
}
