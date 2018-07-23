using MarkSFrancis.Console;
using MarkSFrancis.Web.Services;
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
            var client = new HttpRequestService();

            var task = client.CreateRequest()
                .UseUrl(builder =>
                {
                    builder.Base("https://www.google.com/search")
                        .Query(new
                        {
                            q = "test"
                        });
                })
                .Send(HttpMethod.Get);

            task.Wait();

            var result = task.Result.GetBodyAsStringAsync().Result;

            _console.FontColor = ConsoleColor.Yellow;

            _console.WriteLine("Response: ");
            _console.WriteLine(result);

            _console.ResetColor();
        }
    }
}
