using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.Web.Fluent;
using System;
using System.Net.Http;
using System.Text;

namespace Phnx.Web.Tests.Fluent
{
    public class FluentRequestBodyTests
    {
        public HttpRequestServiceMock Mock { get; }

        public FluentRequestBodyTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        private string GetContentType(FluentRequest request)
        {
            return request.Request.Content.Headers.ContentType.MediaType;
        }

        private string GetBodyOfRequest(FluentRequest request)
        {
            return request.Request.Content.ReadAsStringAsync().Result;
        }

        [Test]
        public void Json_WithNullEncoding_ThrowsArgumentNullException()
        {
            var body = new
            {
                q = "test"
            };

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.Json(body, null));
        }

        [Test]
        public void Json_WithData_AddsHeaderAndContent()
        {
            var body = new
            {
                q = "test"
            };
            var expected = JsonConvert.SerializeObject(body);

            var request = Mock.CreateRequest();

            request
                .WithBody()
                .Json(body);

            Assert.AreEqual(expected, GetBodyOfRequest(request));
            Assert.AreEqual("application/json", GetContentType(request));
        }

        [Test]
        public void PlainText_WithNullEncoding_ThrowsArgumentNullException()
        {
            var body = "test";

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.PlainText(body, null));
        }

        [Test]
        public void PlainText_WithData_AddsHeaderAndContent()
        {
            var expected = "test";

            var request = Mock.CreateRequest();

            request
                .WithBody()
                .PlainText(expected);

            Assert.AreEqual(expected, GetBodyOfRequest(request));
            Assert.AreEqual("text/plain", GetContentType(request));
        }

        [Test]
        public void Form_WithNullEncoding_ThrowsArgumentNullException()
        {
            var form = new
            {
                q = "test"
            };

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.Form(form, null));
        }

        [Test]
        public void Form_WithData_AddsHeaderAndContent()
        {
            var form = new
            {
                q = "test"
            };
            var expected = UrlSerializer.ToQueryString(form);

            var request = Mock.CreateRequest();

            request
                .WithBody()
                .Form(form);

            Assert.AreEqual(expected, GetBodyOfRequest(request));
            Assert.AreEqual("application/x-www-form-urlencoded", GetContentType(request));
        }

        [Test]
        public void Custom_WithNullHttpContent_ThrowsArgumentNullException()
        {
            StringContent content = null;

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.Custom(content));
        }

        [Test]
        public void Custom_WithHttpContent_SetsContent()
        {
            var expected = new StringContent("Test", Encoding.Unicode, "text/csv");

            var request = Mock.CreateRequest();

            request
                .WithBody()
                .Custom(expected);

            Assert.AreEqual(expected, request.Request.Content);
        }

        [Test]
        public void Custom_WithNullContentType_ThrowsArgumentNullException()
        {
            var body = "Sample";
            string contentType = null;

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.Custom(contentType, body));
        }

        [Test]
        public void Custom_WithNullEncoding_ThrowsArgumentNullException()
        {
            var body = "Sample";
            var contentType = "application/custom";

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<ArgumentNullException>(() => bodyRequest.Custom(contentType, body, null));
        }

        [Test]
        public void Custom_WithNullData_DoesNotThrow()
        {
            string body = null;
            var contentType = "application/custom";

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.DoesNotThrow(() => bodyRequest.Custom(contentType, body));
        }

        [Test]
        public void Custom_WithInvalidContentType_ThrowsFormatException()
        {
            string body = null;
            var contentType = "invalid-content-type";

            var request = Mock.CreateRequest();

            var bodyRequest = request.WithBody();

            Assert.Throws<FormatException>(() => bodyRequest.Custom(contentType, body));
        }

        [Test]
        public void Custom_WithData_AddsHeaderAndContent()
        {
            var expectedBody = "Sample";
            var expectedContentType = "application/custom";

            var request = Mock.CreateRequest();

            request
                .WithBody()
                .Custom(expectedContentType, expectedBody);

            Assert.AreEqual(expectedBody, GetBodyOfRequest(request));
            Assert.AreEqual(expectedContentType, GetContentType(request));
        }
    }
}
