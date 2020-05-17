using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.Web.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Phnx.Web.Tests.Fluent
{
    public class FluentRequestTests
    {
        public HttpRequestServiceMock Mock { get; }

        public FluentRequestTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        [Test]
        public void Send_WithUrl_SendsRequestToUrl()
        {
            var request = Mock.CreateRequest();

            var task = request.UseUrl("http://www.google.com/")
                .Send(HttpMethod.Get);

            task.Wait();

            Assert.AreEqual("http://www.google.com/", Mock.Request.RequestUri.ToString());
        }

        [Test]
        public void Send_AsPatch_SendsRequestAsPatch()
        {
            var request = Mock.CreateRequest();

            var task = request.UseUrl("http://www.google.com/")
                .Send("PATCH");

            task.Wait();

            Assert.AreEqual("PATCH", Mock.Request.Method.ToString());
        }

        [Test]
        public void UseUrl_WithNullUrl_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            string url = null;
            Assert.Throws<ArgumentNullException>(() => request.UseUrl(url));
        }

        [Test]
        public void UseUrl_WithNullUrlBuilder_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            Action<FluentRequestUrl> urlBuilder = null;
            Assert.Throws<ArgumentNullException>(() => request.UseUrl(urlBuilder));
        }

        [Test]
        public void SetHeaders_WithNullHeaders_ClearsHeaders()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            HttpRequestHeaders headers = null;
            request.SetHeaders(headers);

            Assert.AreEqual(0, request.Request.Headers.Count());
        }

        [Test]
        public void SetHeaders_WithHeaders_SetsHeaders()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            var headers = new Dictionary<string, IEnumerable<string>>
            {
                { "test2", new[] { "testValue", "testValue2" } }
            };

            request.SetHeaders(headers);

            Assert.AreEqual(1, request.Request.Headers.Count());
        }

        [Test]
        public void SetHeadersIEnumerable_WithNullHeaders_ClearsHeaders()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            IEnumerable<KeyValuePair<string, string>> headers = null;
            request.SetHeaders(headers);

            Assert.AreEqual(0, request.Request.Headers.Count());
        }

        [Test]
        public void SetHeadersIEnumerable_WithHeaders_SetsHeaders()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            var headers = new Dictionary<string, string>
            {
                { "item1", "value1" },
                { "item2", "value2" }
            };

            request.SetHeaders(headers);

            Assert.AreEqual(2, request.Request.Headers.Count());
        }

        [Test]
        public void AppendHeadersIEnumerable_WithNullHeaders_DoesNothing()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            IEnumerable<KeyValuePair<string, string>> headers = null;
            request.AppendHeaders(headers);

            Assert.AreEqual(1, request.Request.Headers.Count());
        }

        [Test]
        public void AppendHeadersIEnumerable_WithHeaders_AppendsHeaders()
        {
            var request = Mock.CreateRequest();

            request.AppendHeader("test", "test");

            var headers = new Dictionary<string, string>
            {
                { "item1", "value1" },
                { "item2", "value2" }
            };

            request.AppendHeaders(headers);

            Assert.AreEqual(3, request.Request.Headers.Count());
        }

        [Test]
        public void AppendHeaderIEnumerable_WithNullKey_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            Assert.Throws<ArgumentNullException>(() => request.AppendHeader(null, new[] { "test" }));
        }

        [Test]
        public void AppendHeaderIEnumerable_WithWhiteSpaceKey_ThrowsArgumentException()
        {
            var request = Mock.CreateRequest();

            Assert.Throws<ArgumentException>(() => request.AppendHeader("    ", new[] { "test" }));
        }

        [Test]
        public void AppendHeaderIEnumerable_WithNullIEnumerableValue_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            IEnumerable<string> value = null;
            Assert.Throws<ArgumentNullException>(() => request.AppendHeader("test", value));
        }

        [Test]
        public void AppendHeader_WithNullKey_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            Assert.Throws<ArgumentNullException>(() => request.AppendHeader(null, "test"));
        }

        [Test]
        public void AppendHeader_WithWhiteSpaceKey_ThrowsArgumentException()
        {
            var request = Mock.CreateRequest();

            Assert.Throws<ArgumentException>(() => request.AppendHeader("    ", "test"));
        }

        [Test]
        public void AppendHeader_WithNullValue_AddsHeader()
        {
            var request = Mock.CreateRequest();

            string value = null;
            request.AppendHeader("test", value);

            Assert.AreEqual(1, request.Request.Headers.Count());
        }

        [Test]
        public void Send_WithNullStringMethod_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            string method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => request.Send(method));
        }

        [Test]
        public void Send_WithWhitespaceStringMethod_ThrowsArgumentException()
        {
            var request = Mock.CreateRequest();

            Assert.ThrowsAsync<ArgumentException>(() => request.Send("    "));
        }

        [Test]
        public void Send_WithNullMethod_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            HttpMethod method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => request.Send(method));
        }

        [Test]
        public void SendWithJsonResponse_WithNullStringMethod_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            string method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => request.SendWithJsonResponse<object>(method));
        }

        [Test]
        public void SendWithJsonResponse_WithWhitespaceStringMethod_ThrowsArgumentException()
        {
            var request = Mock.CreateRequest();

            Assert.ThrowsAsync<ArgumentException>(() => request.SendWithJsonResponse<object>("    "));
        }

        [Test]
        public void SendWithJsonResponse_WithNullMethod_ThrowsArgumentNullException()
        {
            var request = Mock.CreateRequest();

            HttpMethod method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => request.SendWithJsonResponse<object>(method));
        }

        [Test]
        public void SendWithJsonResponse_WithValidResponse_GetsResponseAsJson()
        {
            // Arrange
            var expected = new[]
            {
                "item1",
                "item2",
                "item67"
            };
            string expectedAsJson = JsonConvert.SerializeObject(expected);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedAsJson, Encoding.UTF8, ContentType.Application.Json)
            };

            var request = new HttpRequestServiceMock(response);

            // Act
            var result = request.CreateRequest().SendWithJsonResponse<IEnumerable<string>>("GET").Result;

            var resultBody = result.GetBodyAsync().Result;

            // Assert
            CollectionAssert.AreEqual(expected, resultBody);
        }

        [Test]
        public void ToString_WithNewRequest_ReadsAllPropertiesAsNull()
        {
            string expected = "URL: null; Content: null; Custom Headers: {Count=0}";

            var request = Mock.CreateRequest();

            var description = request.ToString();

            Assert.AreEqual(expected, description);
        }

        [Test]
        public void ToString_WithAllValuesFilled_ReadsAllProperties()
        {
            string expected = "URL: https://www.google.com/; Content: text/plain; Custom Headers: {Count=1}";

            var request = Mock.CreateRequest()
                .AppendHeader("test", "test")
                .WithBody()
                    .PlainText("Test Text")
                .UseUrl("https://www.google.com/");

            var description = request.ToString();

            Assert.AreEqual(expected, description);
        }
    }
}
