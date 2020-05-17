using NUnit.Framework;
using Phnx.Web.Models;
using System.Net;
using System.Net.Http;

namespace Phnx.Web.Tests.Models
{
    public class ApiResponseTests
    {
        [Test]
        public void New_WithNullMessage_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ApiResponse(null));
        }

        [Test]
        public void New_WithMessage_SetsMessage()
        {
            var expected = new HttpResponseMessage();

            var response = new ApiResponse(expected);

            Assert.AreEqual(expected, response.Message);
        }

        [Test]
        public void GetStatusCode_WhenMessageIsNull_ReturnsZero()
        {
            var expected = (HttpStatusCode)0;

            var response = new ApiResponse(null);

            Assert.AreEqual(expected, response.StatusCode);
        }

        [Test]
        public void GetStatusCode_WhenMessageIsNotNull_ReturnsStatusCode()
        {
            var expected = HttpStatusCode.NotFound;

            var response = new ApiResponse(new HttpResponseMessage(expected));

            Assert.AreEqual(expected, response.StatusCode);
        }

        [Test]
        public void GetIsSuccessStatus_WhenMessageIsNull_ReturnsFalse()
        {
            var response = new ApiResponse(null);

            Assert.AreEqual(false, response.IsSuccessStatusCode);
        }

        [Test]
        public void GetIsSuccessStatus_WhenStatusCodeIs404_ReturnsFalse()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.NotFound));

            Assert.AreEqual(false, response.IsSuccessStatusCode);
        }

        [Test]
        public void GetIsSuccessStatus_WhenStatusCodeIs201_ReturnsTrue()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.Created));

            Assert.AreEqual(true, response.IsSuccessStatusCode);
        }

        [Test]
        public void GetHeaders_WhenMessageIsNull_ReturnsNull()
        {
            var response = new ApiResponse(null);

            Assert.IsNull(response.Headers);
        }

        [Test]
        public void GetHeaders_WhenMessageIsNotNull_ReturnsHeaders()
        {
            var response = new ApiResponse(new HttpResponseMessage());

            Assert.IsNotNull(response.Headers);
        }

        [Test]
        public void GetBodyAsStringAsync_WhenMessageIsNull_ReturnsNull()
        {
            var response = new ApiResponse(null);

            var result = response.GetBodyAsStringAsync().Result;

            Assert.IsNull(result);
        }

        [Test]
        public void GetBodyAsStringAsync_WhenMessageIsNotNull_ReturnsBody()
        {
            var expected = "Sample";

            var response = new ApiResponse(new HttpResponseMessage
            {
                Content = new StringContent(expected)
            });

            var result = response.GetBodyAsStringAsync().Result;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThrowIfNotSuccessStatus_WhenMessageIsNull_Throws()
        {
            var response = new ApiResponse(null);

            Assert.Throws<ApiException>(() => response.ThrowIfNotSuccessStatus());
        }

        [Test]
        public void ThrowIfNotSuccessStatus_WhenStatusCodeIs404_Throws()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.NotFound));

            Assert.Throws<ApiException>(() => response.ThrowIfNotSuccessStatus());
        }

        [Test]
        public void ThrowIfNotSuccessStatus_WhenStatusCodeIs201_DoesNotThrow()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.Created));

            Assert.DoesNotThrow(() => response.ThrowIfNotSuccessStatus());
        }

        [Test]
        public void ThrowIfStatusCodeIsNot_WhenMessageIsNull_Throws()
        {
            var response = new ApiResponse(null);

            Assert.Throws<ApiException>(() => response.ThrowIfStatusCodeIsNot());
        }

        [Test]
        public void ThrowIfStatusCodeIsNot_WhenStatusCodeIsNotInList_Throws()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.NotFound));

            Assert.Throws<ApiException>(() => response.ThrowIfStatusCodeIsNot(HttpStatusCode.BadGateway));
        }

        [Test]
        public void ThrowIfStatusCodeIsNot_WhenStatusCodeIsInList_DoesNotThrow()
        {
            var response = new ApiResponse(new HttpResponseMessage(HttpStatusCode.NotFound));

            Assert.DoesNotThrow(() => response.ThrowIfStatusCodeIsNot(HttpStatusCode.NotFound));
        }
    }
}
