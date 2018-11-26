using NUnit.Framework;
using System;
using System.Net.Http;

namespace Phnx.Web.Tests.Fluent
{
    public class ApiClientTests
    {
        public HttpRequestServiceMock Mock { get; }

        public ApiClientTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        [Test]
        public void SendingAHttpRequest_WithoutContentOrQuery_SendsRequestToUrl()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService.UseUrl("http://www.google.com/")
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("http://www.google.com/", Mock.Request.RequestUri.ToString());
        }

        [Test]
        public void SendingAPatchRequest_WithoutContentOrQuery_SendsRequestAsPatch()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService.UseUrl("http://www.google.com/")
                .Send("PATCH");

            task.Wait();

            Assert.AreEqual("PATCH", Mock.Request.Method.ToString());
        }

        [Test]
        public void SendingARequest_WithQuery_SendsRequestWithQueryInUrl()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService
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

            Assert.AreEqual("http://www.google.com/?q=test&date=01%2F01%2F2001%2000%3A00%3A00", Mock.Request.RequestUri.AbsoluteUri);
        }

        [Test]
        public void SendingARequest_WithJsonContent_SendsRequest()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService.UseUrl("http://www.google.com/")
                .WithBody()
                .Json(new
                {
                    q = "test"
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("{\"q\":\"test\"}", Mock.Request.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("application/json", Mock.Request.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void SendingARequest_WithPlaintextContent_SendsRequest()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService.UseUrl("http://www.google.com/")
                .WithBody()
                .PlainText("test")
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("test", Mock.Request.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("text/plain", Mock.Request.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void SendingARequest_WithFormContent_SendsRequest()
        {
            var apiRequestService = Mock.CreateRequest();

            var task = apiRequestService.UseUrl("http://www.google.com/")
                .WithBody()
                .Form(new
                {
                    q = "test"
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("q=test", Mock.Request.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("application/x-www-form-urlencoded", Mock.Request.Content.Headers.ContentType.MediaType);
        }
    }
}
