using NUnit.Framework;
using Phnx.Web.Services;
using System;
using System.Net.Http;

namespace Phnx.Web.Tests.Services
{
    public class HttpRequestServiceTests
    {
        private HttpClient CreateFakeClient(out HttpMessageHandlerFake handler)
        {
            handler = new HttpMessageHandlerFake();
            return new HttpClient(handler);
        }

        private HttpRequestService CreateFakeService(out HttpMessageHandlerFake handler)
        {
            var client = CreateFakeClient(out handler);
            return new HttpRequestService(client);
        }

        [Test]
        public void New_WithNoArgs_AssignsHttpClient()
        {
            var newService = new HttpRequestService();

            Assert.IsNotNull(newService.HttpClient);
        }

        [Test]
        public void New_WithCustomHttpClient_UsesCustomHttpClient()
        {
            var client = new HttpClient();

            var newService = new HttpRequestService(client);

            Assert.AreEqual(client, newService.HttpClient);
        }

        [Test]
        public void New_WithNullClient_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestService(null));
        }

        [Test]
        public void CreateRequest_CreatesNonNullRequest()
        {
            var newService = new HttpRequestService();

            var request = newService.CreateRequest();

            Assert.IsNotNull(request);
        }

        [Test]
        public void SendAsync_WithNullMessage_ThrowsArgumentNullException()
        {
            var newService = CreateFakeService(out _);

            Assert.ThrowsAsync<ArgumentNullException>(() => newService.SendAsync(null));
        }

        [Test]
        public void SendAsync_WithMessage_SendsMessage()
        {
            var newService = CreateFakeService(out var handler);

            var message = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");

            newService.SendAsync(message).Wait();

            Assert.AreEqual(message, handler.LastMessageSent);
        }

        [Test]
        public void SendAsyncCustomClient_WithNullMessage_ThrowsArgumentNullException()
        {
            var newService = CreateFakeService(out _);

            Assert.ThrowsAsync<ArgumentNullException>(() => newService.SendAsync(null, CreateFakeClient(out _)));
        }

        [Test]
        public void SendAsyncCustomClient_WithNullClient_ThrowsArgumentNullException()
        {
            var newService = CreateFakeService(out _);

            var message = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");

            Assert.ThrowsAsync<ArgumentNullException>(() => newService.SendAsync(message, null));
        }

        [Test]
        public void SendAsyncCustomClient_WithMessage_SendsMessageUsingClient()
        {
            var newService = CreateFakeService(out _);

            var message = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
            var client = CreateFakeClient(out var handler);

            newService.SendAsync(message, client);

            Assert.AreEqual(message, handler.LastMessageSent);
        }
    }
}
