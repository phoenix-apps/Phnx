namespace Phnx.Console.Playground
{
    class Program
    {
        public static ConsoleHelper Console = new ConsoleHelper();

        static void Main(string[] args)
        {
            var prop = typeof(Program).GetProperty(nameof(Sample));
            var prop2 = typeof(Program).GetProperty(nameof(Sample2));

            var isAuto = Test.IsAuto(prop);
            var isAuto2 = Test.IsAuto(prop2);

            ConsoleDemo.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
        }

        public int Sample { get; set; }

        public int Sample2 => 0;
    }
}
