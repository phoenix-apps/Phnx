using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Phnx.Web.Tests
{
    public class ApiExceptionTests
    {
        private HttpResponseHeaders CreateHeaders()
        {
            var response = new HttpResponseMessage();

            return response.Headers;
        }

        [Test]
        public void New_WithNullEverything_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ApiException(null, default(HttpStatusCode), null, null));
        }

        [Test]
        public void New_WithNullEverything_DoesNotThrowWhenAccessingPropertiesOrMethods()
        {
            var newError = new ApiException(null, default(HttpStatusCode), null, null);

            Assert.DoesNotThrow(() => _ = newError.ApiUrl);
            Assert.DoesNotThrow(() => _ = newError.StatusCode);
            Assert.DoesNotThrow(() => _ = newError.Body);
            Assert.DoesNotThrow(() => _ = newError.Headers);

            Assert.DoesNotThrow(() => _ = newError.Message);
        }

        [Test]
        public void Message_ContainsUrl()
        {
            string url = "test";
            var error = new ApiException(url, HttpStatusCode.Accepted, CreateHeaders(), "sample body");

            Assert.IsTrue(error.Message.Contains(url));
        }

        [Test]
        public void Message_ContainsStatusCode()
        {
            var status = HttpStatusCode.Accepted;
            var error = new ApiException("test", status, CreateHeaders(), "sample body");

            Assert.IsTrue(error.Message.Contains(status.ToString()));
            Assert.IsTrue(error.Message.Contains(((int)status).ToString()));
        }

        [Test]
        public void Message_ContainsHeaders()
        {
            string key1 = "valKey";
            string val1 = "valVal";
            string key2 = "key1";
            string val2 = "value1";

            var headers = CreateHeaders();
            headers.Add(key1, val1);
            headers.Add(key2, val2);

            var error = new ApiException("test", HttpStatusCode.Accepted, headers, "sample body");

            Assert.IsTrue(error.Message.Contains(key1));
            Assert.IsTrue(error.Message.Contains(val1));

            Assert.IsTrue(error.Message.Contains(key2));
            Assert.IsTrue(error.Message.Contains(val2));
        }

        [Test]
        public void Message_ContainsBody()
        {
            string body = "sample body";
            var error = new ApiException("test", HttpStatusCode.Accepted, CreateHeaders(), "sample body");

            Assert.IsTrue(error.Message.Contains(body));
        }
    }
}
