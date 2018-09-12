namespace MarkSFrancis.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        static void Main(string[] args)
        {
            Console.WriteInfo($"Welcome to the playground application for the MarkSFrancis.Console library");

            var result = Console.GetInt("Please enter an integer:");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }
    }
}
