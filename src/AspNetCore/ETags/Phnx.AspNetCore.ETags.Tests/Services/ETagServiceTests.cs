using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using Phnx.AspNetCore.ETags.Models;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;
using Phnx.Serialization;
using System;
using System.Net;

namespace Phnx.AspNetCore.ETags.Tests.Services
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

            eTagService.RequestHeaders.Add("If-None-Match", new StringValues("\"b\""));

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, eTagService.CheckIfNoneMatch(resource));
        }

        [Test]
        public void CheckIfNoneMatch_WithMissingETag_ReturnsETagNotInRequest()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, eTagService.CheckIfNoneMatch(resource));
        }

        [Test]
        public void CheckIfNoneMatch_WithMatchedETag_ReturnsFalse()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource(eTag);

            var eTagService = new ETagService(contextAccessor);
            eTagService.RequestHeaders.Add("If-None-Match", new StringValues("\"" + eTag + "\""));

            Assert.AreEqual(ETagMatchResult.StrongMatch, eTagService.CheckIfNoneMatch(resource));
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

            eTagService.RequestHeaders.Add("If-Match", new StringValues("\"b\""));

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, eTagService.CheckIfMatch(resource));
        }

        [Test]
        public void CheckIfMatch_WithMissingETag_ReturnsETagNotInRequest()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource("a");

            var eTagService = new ETagService(contextAccessor);

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, eTagService.CheckIfMatch(resource));
        }

        [Test]
        public void CheckIfMatch_WithMatchedETag_ReturnsTrue()
        {
            string eTag = Guid.NewGuid().ToString();
            var contextAccessor = new FakeActionContextAccessor();
            var resource = new FakeResource(eTag);

            var eTagService = new ETagService(contextAccessor);
            eTagService.RequestHeaders.Add("If-Match", new StringValues("\"" + eTag + "\""));

            Assert.AreEqual(ETagMatchResult.StrongMatch, eTagService.CheckIfMatch(resource));
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

            Assert.Throws<ArgumentNullException>(() => eTagService.AddETagForModelToResponse(null));
        }

        [Test]
        public void AddETagToResponse_WithData_AddsETagToResponseHeaders()
        {
            string eTag = Guid.NewGuid().ToString();

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);
            var resource = new FakeResource(eTag);

            eTagService.AddETagForModelToResponse(resource);

            Assert.IsTrue(eTagService.ResponseHeaders.ContainsKey("ETag"));
            Assert.AreEqual("\"" + eTag + "\"", eTagService.ResponseHeaders["ETag"]);
        }

        [Test]
        public void TryGetStrongETagMember_WithModelThatDoesNotSupportETags_ReturnsFalse()
        {
            var data = new FakeDto();
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var result = eTagService.TryGetStrongETag(data, out var etag);

            Assert.IsFalse(result);
            Assert.IsNull(etag);
        }

        [Test]
        public void TryGetStrongETag_WithGetterThatThrows_ReturnsFalse()
        {
            var sampleModel = new BrokenFakeResource();

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var result = eTagService.TryGetStrongETag(sampleModel, out var etagLoaded);

            Assert.IsFalse(result);
            Assert.IsNull(etagLoaded);
        }

        [Test]
        public void TryGetStrongETag_WithModelThatDoesSupportETags_ReturnsTrueAndMember()
        {
            var eTag = Guid.NewGuid().ToString();
            var sampleModel = new FakeResource(eTag);

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var result = eTagService.TryGetStrongETag(sampleModel, out var etagLoaded);

            Assert.IsTrue(result);
            Assert.AreEqual("\"" + eTag + "\"", etagLoaded);
        }

        [Test]
        public void GenerateWeakETag_ForNullObject_ReturnsEmptyETag()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var result = eTagService.GetWeakETag(null);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void GenerateWeakETag_ForObject_ReturnsWPrefixedETag()
        {
            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var result = eTagService.GetWeakETag(new object());

            Assert.IsTrue(result.StartsWith("W/"));
        }

        [Test]
        public void GenerateWeakETag_ForMatchingObjects_ReturnsMatchingTags()
        {
            var testData = new FakeDto
            {
                Id = 17
            };

            var testDataCopy = testData.ShallowCopy();

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var tag = eTagService.GetWeakETag(testData);
            var tagForCopy = eTagService.GetWeakETag(testDataCopy);

            Assert.AreEqual(tag, tagForCopy);
        }

        [Test]
        public void GenerateWeakETag_ForDifferentObjects_RetursNonMatchingTags()
        {
            var testData = new FakeDto
            {
                Id = 17
            };
            var nonCopy = new FakeDto
            {
                Id = 16
            };

            var contextAccessor = new FakeActionContextAccessor();
            var eTagService = new ETagService(contextAccessor);

            var tag = eTagService.GetWeakETag(testData);
            var tagForCopy = eTagService.GetWeakETag(nonCopy);

            Assert.AreNotEqual(tag, tagForCopy);
        }
    }
}
