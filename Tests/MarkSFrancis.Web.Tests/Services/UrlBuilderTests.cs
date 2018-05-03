using MarkSFrancis.Web.Services;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Web.Tests.Services
{
    public class UrlBuilderTests
    {
        [Test]
        public void RenderingAnUrl_WithoutSegments_ReturnsUrl()
        {
            string url = "http://www.test.com";

            var formattedUrl = UrlBuilder.ToUrl(url);

            Assert.AreEqual(url, formattedUrl);
        }

        [Test]
        public void RenderingAnUrl_WithValidSegments_ReturnsUrl()
        {
            string url = "http://www.test.com";
            var segments = new[]
            {
                "asdf",
                "goog",
                "wo2"
            };

            string finalUrl = url + "/" + string.Join("/", segments);

            var formattedUrl = UrlBuilder.ToUrl(url, segments);

            Assert.AreEqual(finalUrl, formattedUrl);
        }

        [Test]
        public void RenderingAnUrl_WithInvalidSegments_ReturnsEscapedUrl()
        {
            string url = "http://www.test.com";
            var segments = new[]
            {
                "as d f",
                "g oog",
                "wo2"
            };

            string finalUrl = url + "/as%20d%20f/g%20oog/wo2";

            var formattedUrl = UrlBuilder.ToUrl(url, segments);

            Assert.AreEqual(finalUrl, formattedUrl);
        }

        [Test]
        public void RenderingAQueryString_WithNull_ReturnsEmptyString()
        {
            string queryString = String.Empty;

            var formattedUrl = UrlBuilder.ToQueryString(null);

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

            var formattedUrl = UrlBuilder.ToQueryString(query);

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

            var formattedUrl = UrlBuilder.ToQueryString(query);

            Assert.AreEqual(queryString, formattedUrl);
        }

        [Test]
        public void SetQueryString_OnUrlWithoutQuery_AppendsQuery()
        {
            string url = "http://www.test.com";

            string queryString = "names=John%20Smith%2CDavid%20Jones%2CSam%20Smith&dob=01%2F01%2F2000%2000%3A00%3A00";

            string completeUrl = url + "?" + queryString;

            var formattedUrl = UrlBuilder.SetQueryString(url, queryString);

            Assert.AreEqual(completeUrl, formattedUrl);
        }

        [Test]
        public void SetQueryString_OnUrlWithQuery_ReplacesQuery()
        {
            string url = "http://www.test.com?test=asdf";

            string queryString = "names=John%20Smith%2CDavid%20Jones%2CSam%20Smith&dob=01%2F01%2F2000%2000%3A00%3A00";

            string completeUrl = "http://www.test.com" + "?" + queryString;

            var formattedUrl = UrlBuilder.SetQueryString(url, queryString);

            Assert.AreEqual(completeUrl, formattedUrl);
        }
    }
}
