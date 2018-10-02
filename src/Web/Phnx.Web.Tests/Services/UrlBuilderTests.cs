using Phnx.Web.Services;
using NUnit.Framework;
using System;

namespace Phnx.Web.Tests.Services
{
    public class UrlBuilderTests
    {
        [Test]
        public void RenderingAnUrl_WithValidSegments_ReturnsUrl()
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
        public void RenderingAnUrl_WithInvalidSegments_ReturnsEscapedUrl()
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
        public void RenderingAQueryString_WithNull_ReturnsEmptyString()
        {
            string queryString = String.Empty;

            var formattedUrl = UrlSerializer.ToQueryString(null);

            Assert.AreEqual(queryString, formattedUrl);
        }

        [Test]
        public void RenderingAQueryString_WithSimpleObject_ReturnsQuery()
        {
            string queryString = "name=John%20Smith&dob=01%2F01%2F2000%2000%3A00%3A00";

            var query = new
            {
                name = "John Smith",
                dob = new DateTime(2000, 1, 1)
            };

            var formattedUrl = UrlSerializer.ToQueryString(query);

            Assert.AreEqual(queryString, formattedUrl);
        }

        [Test]
        public void RenderingAQueryString_WithObjectContainingCollection_ReturnsQuery()
        {
            string queryString = "names=John%20Smith%2CDavid%20Jones%2CSam%20Smith&dob=01%2F01%2F2000%2000%3A00%3A00";

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
    }
}
