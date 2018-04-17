using MarkSFrancis.Console;
using MarkSFrancis.Web.Fluent;
using System.IO;
using System.Net.Http;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
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

            Console.WriteLine("Response: ");
            Console.WriteLine(result);

            Console.WriteLine("Saving response...");
            File.WriteAllText(@"C:\Users\MarkFrancis\Documents\result.html", result);
            Console.WriteLine("Response saved");

            Console.GetString();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
            threads.Run();
        }
    }
}
