using NUnit.Framework;
using System;

namespace Phnx.Web.Tests.Services
{
    public class UrlSerializerTests
    {
        [Test]
        public void ToUrl_WithValidSegments_ReturnsUrl()
        {
            var segments = new[]
            {
                "asdf",
                "goog",
                "wo2"
            };

            string finalUrl = string.Join("/", segments);

            var formattedUrl = UrlSerializer.ToUrl(segments, true);

            Assert.AreEqual(finalUrl, formattedUrl);
        }

        [Test]
        public void ToUrl_WithInvalidSegments_ReturnsEscapedUrl()
        {
            var segments = new[]
            {
                "as d f",
                "g oog",
                "wo2"
            };

            string finalUrl = "as%20d%20f/g%20oog/wo2";

            var formattedUrl = UrlSerializer.ToUrl(segments, true);

            Assert.AreEqual(finalUrl, formattedUrl);
        }

        [Test]
        public void ToQueryString_WithNull_ReturnsEmptyString()
        {
            string expected = string.Empty;

            var result = UrlSerializer.ToQueryString(null);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToQueryString_WithSimpleObject_ReturnsQuery()
        {
            string queryString = "name=John%20Smith&dob=2000-01-01T00%3A00%3A00";

            var query = new
            {
                name = "John Smith",
                dob = new DateTime(2000, 1, 1)
            };

            var formattedUrl = UrlSerializer.ToQueryString(query);

            Assert.AreEqual(queryString, formattedUrl);
        }

        [Test]
        public void ToQueryString_WithObjectContainingCollection_ReturnsQuery()
        {
            string queryString = "names=" +
                "%5B" +
                    "%22John%20Smith%22%2C" +
                    "%22David%20Jones%22%2C" +
                    "%22Sam%20Smith%22" +
                "%5D" +
                "&dob=2000-01-01T00%3A00%3A00";

            var query = new
            {
                names = new[]
                {
                    "John Smith",
                    "David Jones",
                    "Sam Smith"
                },
                dob = new DateTime(2000, 1, 1)
            };

            var formattedUrl = UrlSerializer.ToQueryString(query);

            Assert.AreEqual(queryString, formattedUrl);
        }

        [Test]
        public void SerializeForUrl_WhenValueIsNull_ReturnsEmptyString()
        {
            var expected = string.Empty;

            var result = UrlSerializer.SerializeForUrl(null);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SerializeForUrl_WhenValueIsString_Serializes()
        {
            var expected = "test%20URL";

            var result = UrlSerializer.SerializeForUrl("test URL");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SerializeForUrl_WhenValueIsDateTime_Serializes()
        {
            var expected = "2001-01-01T00%3A00%3A00";

            var result = UrlSerializer.SerializeForUrl(new DateTime(2001, 1, 1, 0, 0, 0));

            Assert.AreEqual(expected, result);
        }
    }
}
