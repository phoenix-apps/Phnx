using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace Phnx.Web.Tests.Fluent
{
    public class FluentRequestBodyTests
    {
        public HttpRequestServiceMock Mock { get; }

        public FluentRequestBodyTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        [Test]
        public void SendingARequest_WithJsonContent_SendsRequest()
        {
            var request = Mock.CreateRequest();

            var task = request.UseUrl("http://www.google.com/")
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
            var request = Mock.CreateRequest();

            var task = request.UseUrl("http://www.google.com/")
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
            var request = Mock.CreateRequest();

            var task = request.UseUrl("http://www.google.com/")
                .WithBody()
                .Form(new
                {
                    q = "test"
                })
                .SetHeaders(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("a", null) })
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("q=test", Mock.Request.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("application/x-www-form-urlencoded", Mock.Request.Content.Headers.ContentType.MediaType);
        }
    }
}
