using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.Web.Fluent;

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
    }
}
