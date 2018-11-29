using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using Phnx.AspNetCore.Rest.Services;
using Phnx.AspNetCore.Rest.Tests.Fakes;
using System;
using System.Net;

namespace Phnx.AspNetCore.Rest.Tests.Services
{
    public class ETagServiceTests
    {
        [Test]
        public void New_WithNullContextAccessor_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagService(null));
        }

        [Test]
        public void New_WithContextAccessor_SetsContextAccessorAndHeaders()
        {
            var contextAccessor = new FakeActionContextAccessor();

            var eTagService = new ETagService(contextAccessor);

            Assert.AreEqual(contextAccessor, eTagService.ActionContext);
            Assert.AreEqual(contextAccessor.ActionContext.HttpContext.Request.Headers, eTagService.RequestHeaders);
            Assert.AreEqual(contextAccessor.ActionContext.HttpContext.Response.Headers, eTagService.ResponseHeaders);
        }

        [Test]
        public void CheckIfNoneMatch_WithNullData_ThrowsArgumentNullException()
        {
            var contextAccessor = new FakeActionContextAccessor();

            var eTagService = new ETagService(contextAccessor);

            Assert.Throws<ArgumentNullException>(() => eTagService.CheckIfNoneMatch(null));
        }

        [Test]
        public void CheckIfNoneMatch_WithMismatchedEtag_ReturnsTrue()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            eTagService.RequestHeaders.Add("If-None-Match", new StringValues("b"));

            Assert.IsTrue(eTagService.CheckIfNoneMatch(resource));
        }

        [Test]
        public void CheckIfNoneMatch_WithMissingETag_ReturnsTrue()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            Assert.IsTrue(eTagService.CheckIfNoneMatch(resource));
        }

        [Test]
        public void CheckIfNoneMatch_WithMatchedETag_ReturnsFalse()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource(eTag);

            var eTagService = new ETagService(contextAccessor);
            eTagService.RequestHeaders.Add("If-None-Match", new StringValues(eTag));

            Assert.IsFalse(eTagService.CheckIfNoneMatch(resource));
        }

        [Test]
        public void CheckIfMatch_WithNullData_ThrowsArgumentNullException()
        {
            var contextAccessor = new FakeActionContextAccessor();

            var eTagService = new ETagService(contextAccessor);

            Assert.Throws<ArgumentNullException>(() => eTagService.CheckIfMatch(null));
        }

        [Test]
        public void CheckIfMatch_WithMismatchedEtag_ReturnsFalse()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            eTagService.RequestHeaders.Add("If-Match", new StringValues("b"));

            Assert.IsFalse(eTagService.CheckIfMatch(resource));
        }

        [Test]
        public void CheckIfMatch_WithMissingETag_ReturnsTrue()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            Assert.IsTrue(eTagService.CheckIfMatch(resource));
        }

        [Test]
        public void CheckIfMatch_WithMatchedETag_ReturnsTrue()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource(eTag);

            var eTagService = new ETagService(contextAccessor);
            eTagService.RequestHeaders.Add("If-Match", new StringValues(eTag));

            Assert.IsTrue(eTagService.CheckIfMatch(resource));
        }

        [Test]
        public void CreateMatchResponse_CreatesNotModified()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var matchResponse = eTagService.CreateMatchResponse();

            Assert.AreEqual((int)HttpStatusCode.NotModified, matchResponse.StatusCode);
        }

        [Test]
        public void CreateDoNotMatchResponse_CreatesPreconditionFailed()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var matchResponse = eTagService.CreateDoNotMatchResponse();

            Assert.AreEqual((int)HttpStatusCode.PreconditionFailed, matchResponse.StatusCode);
        }

        [Test]
        public void AddETagToResponse_WithNullData_ThrowsArgumentNullException()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            Assert.Throws<ArgumentNullException>(() => eTagService.AddETagToResponse(null));
        }

        [Test]
        public void AddETagToResponse_WithData_AddsETagToResponseHeaders()
        {
            string eTag = Guid.NewGuid().ToString();

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);
            var resource = new FakeResource(eTag);

            eTagService.AddETagToResponse(resource);

            Assert.IsTrue(eTagService.ResponseHeaders.ContainsKey("ETag"));
            Assert.AreEqual(eTag, eTagService.ResponseHeaders["ETag"]);
        }
    }
}
