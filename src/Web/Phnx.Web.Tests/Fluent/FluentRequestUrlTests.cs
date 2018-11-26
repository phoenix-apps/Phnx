using NUnit.Framework;
using System;
using System.Net.Http;

namespace Phnx.Web.Tests.Fluent
{
    public class FluentRequestUrlTests
    {
        public HttpRequestServiceMock Mock { get; }

        public FluentRequestUrlTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        [Test]
        public void SendingARequest_WithQuery_SendsRequestWithQueryInUrl()
        {
            var request = Mock.CreateRequest();

            var task = request
                .UseUrl(builder =>
                {
                    builder
                        .Base("http://www.google.com")
                        .Query(new
                        {
                            q = "test",
                            date = new DateTime(2001, 1, 1)
                        });
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("http://www.google.com/?q=test&date=2001-01-01T00%3A00%3A00", Mock.Request.RequestUri.AbsoluteUri);
        }
    }
}
