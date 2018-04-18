using MarkSFrancis.Console;
using MarkSFrancis.Web.Fluent;
using System;
using System.Net.Http;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            if (Console.YesNo($"Run {nameof(ApiClient)} demo?"))
            {
                var client = new ApiClient();

                var task = client.CreateRequest("https://www.google.com/search")
                    .WithQuery(new
                    {
                        q = "test"
                    })
                    .Send(HttpMethod.Get);

                task.Wait();

                var result = task.Result.Body;

                Console.FontColor = ConsoleColor.Yellow;

                Console.WriteLine("Response: ");
                Console.WriteLine(result);

                Console.ResetColor();
            }

            if (Console.YesNo($"Run {nameof(ThreadsDemo)}?"))
            {
                ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
                threads.Run();
            }

            Console.WriteLine("Waiting for keypress to close...");
            Console.ReadKey();
        }
    }
}
