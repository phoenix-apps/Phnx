using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.Web.Services;
using System;
using System.Net.Http;

namespace Phnx.Web.Tests.Services
{
    public class IHttpRequestServiceExtensionsTests
    {
        public HttpRequestServiceMock Mock { get; }

        public IHttpRequestServiceExtensionsTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        [Test]
        public void SendAsyncWithHttpMethod_WithNullRequestService_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => IHttpRequestServiceExtensions.SendAsync<object>(null, "https://www.google.com", HttpMethod.Get));
        }

        [Test]
        public void SendAsyncWithMethod_WithNullRequestService_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => IHttpRequestServiceExtensions.SendAsync<object>(null, "https://www.google.com", "GET"));
        }

        [Test]
        public void SendAsync_WithNullUrl_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => Mock.SendAsync<object>(null, HttpMethod.Get));
        }

        [Test]
        public void SendAsync_WithNullHttpMethod_ThrowsArgumentNullException()
        {
            HttpMethod method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => Mock.SendAsync<object>("https://www.google.com", method));
        }

        [Test]
        public void SendAsync_WithNullMethod_ThrowsArgumentNullException()
        {
            string method = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => Mock.SendAsync<object>("https://www.google.com", method));
        }

        [Test]
        public void SendAsync_WithNullBody_LeavesContentEmpty()
        {
            Mock.SendAsync<object>("https://www.google.com", HttpMethod.Get, null).Wait();

            var lastSent = Mock.Request;

            Assert.IsNull(lastSent.Content);
        }

        [Test]
        public void SendAsync_WithBody_AssignsBody()
        {
            object data = new
            {
                Id = 125,
                Name = "John Smith"
            };
            string dataSerialized = JsonConvert.SerializeObject(data);

            Mock.SendAsync<object>("https://www.google.com", HttpMethod.Get, data).Wait();

            var lastSent = Mock.Request;

            Assert.IsNotNull(lastSent.Content);
            var contentRead = lastSent.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(dataSerialized, contentRead);
        }

        [Test]
        public void GetAsync_UsesVerb()
        {
            Mock.GetAsync<object>("https://www.google.com").Wait();

            Assert.AreEqual(HttpMethod.Get, Mock.Request.Method);
        }

        [Test]
        public void PostAsync_UsesVerb()
        {
            Mock.PostAsync<object>("https://www.google.com", null).Wait();

            Assert.AreEqual(HttpMethod.Post, Mock.Request.Method);
        }

        [Test]
        public void PutAsync_UsesVerb()
        {
            Mock.PutAsync<object>("https://www.google.com", null).Wait();

            Assert.AreEqual(HttpMethod.Put, Mock.Request.Method);
        }

        [Test]
        public void PatchAsync_UsesVerb()
        {
            Mock.PatchAsync<object>("https://www.google.com", null).Wait();

            Assert.AreEqual(new HttpMethod("patch"), Mock.Request.Method);
        }

        [Test]
        public void DeleteAsync_UsesVerb()
        {
            Mock.DeleteAsync<object>("https://www.google.com", null).Wait();

            Assert.AreEqual(HttpMethod.Delete, Mock.Request.Method);
        }
    }
}
