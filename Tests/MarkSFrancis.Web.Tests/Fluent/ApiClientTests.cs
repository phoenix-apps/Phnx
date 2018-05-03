using MarkSFrancis.Web.Fluent;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace MarkSFrancis.Web.Tests.Fluent
{
    public class ApiClientTests
    {
        public ApiClient CreateTestApiClient(out HttpRequestServiceMock mock)
        {
            mock = new HttpRequestServiceMock();
            return new ApiClient(mock);
        }

        [Test]
        public void SendingAHttpRequest_WithoutContentOrQuery_SendsRequestToUrl()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("http://www.google.com", mock.Request.Url);
        }

        [Test]
        public void SendingAPatchRequest_WithoutContentOrQuery_SendsRequestAsPatch()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .Send("PATCH");

            task.Wait();

            Assert.AreEqual("PATCH", mock.Request.Method);
        }

        [Test]
        public void SendingARequest_WithQuery_SendsRequestWithQueryInUrl()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .WithQuery(new
                {
                    q = "test",
                    date = new DateTime(2001, 1, 1)
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("http://www.google.com?q=test&date=01%2F01%2F2001%2000%3A00%3A00", mock.Request.Url);
        }

        [Test]
        public void SendingARequest_WithJsonContent_SendsRequest()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .WithJsonBody(new
                {
                    q = "test"
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("{\"q\":\"test\"}", mock.Request.Content);
            Assert.AreEqual("application/json", mock.Request.ContentType);
        }

        [Test]
        public void SendingARequest_WithPlaintextContent_SendsRequest()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .WithPlainTextBody("test")
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("test", mock.Request.Content);
            Assert.AreEqual("text/plain", mock.Request.ContentType);
        }

        [Test]
        public void SendingARequest_WithFormContent_SendsRequest()
        {
            var apiRequestService = CreateTestApiClient(out var mock);

            var task = apiRequestService.CreateRequest("http://www.google.com")
                .WithUrlFormBody(new
                {
                    q = "test"
                })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("q=test", mock.Request.Content);
            Assert.AreEqual("application/x-www-form-urlencoded", mock.Request.ContentType);
        }
    }
}
