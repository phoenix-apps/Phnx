using MarkSFrancis.Console;
using MarkSFrancis.Web.Fluent;
using System.Net.Http;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            var client = new ApiClient();

            var task = client.CreateRequest("http://www.google.com")
                .Send(HttpMethod.Get);

            task.Wait();

            var result = task.Result;

            Console.WriteLine("Response: ");
            Console.WriteLine(result.Body);

            Console.GetString();
        }

        static void RunThreadsDemo()
        {
            ThreadsDemo threads = new ThreadsDemo(Console, ThreadsDemo.Mode.Interlocked);
            threads.Run();
        }
    }
}
