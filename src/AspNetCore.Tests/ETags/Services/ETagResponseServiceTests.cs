using System;
using System.Net;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;

namespace Phnx.AspNetCore.ETags.Tests.Services
{
    public class ETagResponseServiceTests
    {
        public ETagResponseService GetService(Mock<IETagService> mockETagService = null)
        {
            IETagService eTagService;
            if (mockETagService is null)
            {
                eTagService = new ETagService();
            }
            else
            {
                eTagService = mockETagService.Object;
            }

            var eTagResponse = new ETagResponseService(new FakeActionContextAccessor(), eTagService);

            return eTagResponse;
        }

        public string GetETagHeader(ETagResponseService service)
        {
            service.ResponseHeaders.TryGetValue("ETag", out StringValues val);

            return val.Count > 0 ? val[0] : null;
        }

        [Test]
        public void New_WithNullActionContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagResponseService(null, new ETagService()));
        }

        [Test]
        public void New_WithNullETagService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagResponseService(new FakeActionContextAccessor(), null));
        }

        [Test]
        public void CreateDataHasNotChangedResponse_ReturnsNotModifiedCode()
        {
            ETagResponseService service = GetService();

            Microsoft.AspNetCore.Mvc.StatusCodeResult response = service.CreateDataHasNotChangedResponse();

            Assert.AreEqual((int)HttpStatusCode.NotModified, response.StatusCode);
        }

        [Test]
        public void CreateDataHasChangedResponse_ReturnsPreconditionedFailedCode()
        {
            ETagResponseService service = GetService();

            Microsoft.AspNetCore.Mvc.StatusCodeResult response = service.CreateDataHasChangedResponse();

            Assert.AreEqual((int)HttpStatusCode.PreconditionFailed, response.StatusCode);
        }

        [Test]
        public void AddWeakETagForModel_ForNullModel_ThrowsArgumentNullException()
        {
            ETagResponseService service = GetService();

            Assert.Throws<ArgumentNullException>(() => service.AddWeakETagForModel(null));
        }

        [Test]
        public void AddWeakETagForModel_ForNonNullModel_AddsWeakTagFromETagService()
        {
            var expected = "a";
            var mockService = new Mock<IETagService>();
            mockService
                .Setup(e => e.GetWeakETag(It.IsAny<object>()))
                .Returns(expected);

            ETagResponseService service = GetService(mockService);

            service.AddWeakETagForModel(new object());

            var result = GetETagHeader(service);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TryAddStrongETagForModel_ForNullModel_ReturnsFalse()
        {
            ETagResponseService service = GetService();

            var added = service.TryAddStrongETagForModel(null);

            Assert.IsFalse(added);
        }

        [Test]
        public void TryAddStrongETagForModel_ForSupportedModel_ReturnsTrueAndAddsETagFromETagService()
        {
            var expected = "a";
            var mockService = new Mock<IETagService>();
            mockService
                .Setup(e => e.TryGetStrongETag(It.IsAny<object>(), out expected))
                .Returns(true);

            ETagResponseService service = GetService(mockService);

            var added = service.TryAddStrongETagForModel(new object());

            var result = GetETagHeader(service);

            Assert.IsTrue(added);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TryAddStrongETagForModel_ForUnsupportedModel_ReturnsFalseAndDoesNotAddHeader()
        {
            string expected = null;
            var mockService = new Mock<IETagService>();
            mockService
                .Setup(e => e.TryGetStrongETag(It.IsAny<object>(), out expected))
                .Returns(false);

            ETagResponseService service = GetService(mockService);

            var added = service.TryAddStrongETagForModel(new object());

            var result = GetETagHeader(service);

            Assert.IsFalse(added);
            Assert.AreEqual(0, service.ResponseHeaders.Count);
        }

        [Test]
        public void AddStrongestETagForModel_ForNullModel_ThrowsArgumentNullException()
        {
            ETagResponseService service = GetService();

            Assert.Throws<ArgumentNullException>(() => service.AddStrongestETagForModel(null));
        }

        [Test]
        public void AddStrongestETagForModel_WhenOnlyWeakIsSupported_AddsWeakTagFromETagService()
        {
            var expected = "a";
            var strongETag = "Tried adding strong etag";

            var mockService = new Mock<IETagService>();
            mockService
                .Setup(e => e.GetWeakETag(It.IsAny<object>()))
                .Returns(expected);
            mockService
                .Setup(e => e.TryGetStrongETag(It.IsAny<object>(), out strongETag))
                .Returns(false);

            ETagResponseService service = GetService(mockService);

            service.AddStrongestETagForModel(new object());

            var result = GetETagHeader(service);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddStrongestETagForModel_WhenStrongIsSupported_AddsStrongTagFromETagService()
        {
            var expected = "a";

            var mockService = new Mock<IETagService>();
            mockService
                .Setup(e => e.GetWeakETag(It.IsAny<object>()))
                .Returns("Tried added weak etag");
            mockService
                .Setup(e => e.TryGetStrongETag(It.IsAny<object>(), out expected))
                .Returns(true);

            ETagResponseService service = GetService(mockService);

            service.AddStrongestETagForModel(new object());

            var result = GetETagHeader(service);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddETag_WhenETagIsNull_ThrowsArgumentNulLException()
        {
            ETagResponseService service = GetService();

            Assert.Throws<ArgumentNullException>(() => service.AddETag(null));
        }

        [Test]
        public void AddETag_WhenETagIsNotNull_AddsETag()
        {
            string expected = string.Empty;
            ETagResponseService service = GetService();

            service.AddETag(expected);

            var etag = GetETagHeader(service);
            Assert.AreEqual(expected, etag);
        }
    }
}
