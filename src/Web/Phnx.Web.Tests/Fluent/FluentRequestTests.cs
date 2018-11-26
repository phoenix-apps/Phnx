using NUnit.Framework;
using Phnx.Web.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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
    }
}
