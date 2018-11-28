using NUnit.Framework;
using Phnx.Web.Fluent;
using System;

namespace Phnx.Web.Tests.Fluent
{
    public class FluentRequestUrlTests
    {
        public HttpRequestServiceMock Mock { get; }

        public FluentRequestUrlTests()
        {
            Mock = new HttpRequestServiceMock();
        }

        private string GetUri(FluentRequest request)
        {
            return request.Request.RequestUri.AbsoluteUri;
        }

        [Test]
        public void Query_WithProperties_Serializes()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder
                        .Base("http://www.google.com")
                        .Query(new
                        {
                            q = "test",
                            date = new DateTime(2001, 1, 1)
                        });
                });

            Assert.AreEqual("http://www.google.com/?q=test&date=2001-01-01T00%3A00%3A00", GetUri(request));
        }

        [Test]
        public void Query_WithNull_SerializesWithoutQuery()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder
                        .Base("http://www.google.com")
                        .Query(null);
                });

            Assert.AreEqual("http://www.google.com/", GetUri(request));
        }

        [Test]
        public void Serialize_WithPathOnly_CreatesPathAsIfPathWasBase()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder
                        .Path(false, "https://path", "test");
                });

            Assert.AreEqual("https://path/test", GetUri(request));
        }

        [Test]
        public void Path_WithPathString_SetsPath()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder.Base("https://www.google.com");
                    builder.Path("api/people/id");
                });

            Assert.AreEqual("https://www.google.com/api/people/id", GetUri(request));
        }

        [Test]
        public void Path_WithPathObject_MapsPathOntoTemplate()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder
                    .Base("https://www.google.com")
                    .Path("api/people/{id}", new { id = 17 });
                });

            Assert.AreEqual("https://www.google.com/api/people/17", GetUri(request));
        }

        [Test]
        public void Build_WithBasePathAndQuery_CreatesValidPath()
        {
            var request = Mock.CreateRequest();

            request
                .UseUrl(builder =>
                {
                    builder
                    .Base("https://www.google.com")
                    .Path("api/people/{id}", new { id = 17 })
                    .Query(new
                    {
                        includeAddress = true
                    });
                });

            Assert.AreEqual("https://www.google.com/api/people/17?includeAddress=true", GetUri(request));
        }
    }
}
