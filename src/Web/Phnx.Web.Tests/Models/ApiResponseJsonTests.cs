using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.Web.Models;
using System;
using System.Net.Http;

namespace Phnx.Web.Tests.Models
{
    public class ApiResponseJsonTests
    {
        [Test]
        public void GetBodyAsync_WithNullMessage_ThrowsInvalidOperationException()
        {
            var response = new ApiResponseJson<object>(null);

            Assert.ThrowsAsync<InvalidOperationException>(() => response.GetBodyAsync());
        }

        [Test]
        public void GetBodyAsync_WithNullMessageContent_ThrowsInvalidOperationException()
        {
            var response = new ApiResponseJson<object>(new HttpResponseMessage());

            Assert.ThrowsAsync<InvalidOperationException>(() => response.GetBodyAsync());
        }

        [Test]
        public void GetBodyAsync_WithValidContent_GetsContentAsType()
        {
            var expected = new JsonTestModel
            {
                FullName = "test",
                Id = "Sample",
                LastChange = new DateTime(2000, 1, 1, 0, 0, 0)
            };

            var body = JsonConvert.SerializeObject(expected);

            var response = new ApiResponseJson<JsonTestModel>(new HttpResponseMessage
            {
                Content = new StringContent(body)
            });

            var result = response.GetBodyAsync().Result;

            Assert.AreEqual(expected, result);
        }
    }
}
