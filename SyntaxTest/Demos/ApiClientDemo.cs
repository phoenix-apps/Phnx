using MarkSFrancis.Console;
using MarkSFrancis.Web.Fluent;
using SyntaxTest.Demos.Interfaces;
using System;
using System.Net.Http;

namespace SyntaxTest.Demos
{
    public class ApiClientDemo : IDemo
    {
        private readonly ConsoleIo _console;

        public ApiClientDemo(ConsoleIo console)
        {
            _console = console;
        }

        public void Run()
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

            _console.FontColor = ConsoleColor.Yellow;

            _console.WriteLine("Response: ");
            _console.WriteLine(result);

            _console.ResetColor();
        }
    }
}
