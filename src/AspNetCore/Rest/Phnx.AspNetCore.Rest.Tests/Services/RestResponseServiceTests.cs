using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Phnx.AspNetCore.Rest.Models;
using Phnx.AspNetCore.Rest.Services;
using Phnx.AspNetCore.Rest.Tests.Fakes;
using System;
using System.Net;

namespace Phnx.AspNetCore.Rest.Tests.Services
{
    public class RestResponseServiceTests
    {
        private RestResponseFactory<FakeResource, FakeDto> CreateFake(Mock<IETagService> eTagService = null)
        {
            var mapper = new FakeResourceMap();
            if (eTagService is null)
            {
                eTagService = new Mock<IETagService>();
            }

            return new RestResponseFactory<FakeResource, FakeDto>(mapper, eTagService.Object);
        }

        [Test]
        public void New_WithNullMapper_ThrowsArgumentNullException()
        {
            var eTagService = new Mock<IETagService>();

            Assert.Throws<ArgumentNullException>(() => new RestResponseFactory<FakeResource, object>(null, eTagService.Object));
        }

        [Test]
        public void New_WithNullETagService_ThrowsArgumentNullException()
        {
            var mapper = new FakeResourceMap();

            Assert.Throws<ArgumentNullException>(() => new RestResponseFactory<FakeResource, object>(mapper, null));
        }

        [Test]
        public void New_NonNullMapperAndETagService_SetsETagServiceAndMapper()
        {
            var mapper = new FakeResourceMap();
            var eTagService = new Mock<IETagService>();

            var responseService = new RestResponseFactory<FakeResource, object>(mapper, eTagService.Object);

            Assert.AreEqual(eTagService.Object, responseService.ETagService);
            Assert.AreEqual(mapper, responseService.Mapper);
        }

        [Test]
        public void NotFound_WithNullTypeNameAndIdentifier_DoesNotThrow()
        {
            var service = CreateFake();
            Assert.DoesNotThrow(() => service.NotFound(null, null));
        }

        [Test]
        public void NotFound_WithTypeNameAndIdentifier_DoesNotThrow()
        {
            var service = CreateFake();
            Assert.DoesNotThrow(() => service.NotFound("type", "id"));
        }

        [Test]
        public void DataHasNotChanged_ReturnsNotModified()
        {
            var eTagService = new Mock<IETagService>();
            eTagService.Setup(e =>
                e.CreateMatchResponse())
            .Returns(() =>
                new StatusCodeResult((int)HttpStatusCode.NotModified));

            var service = CreateFake(eTagService);

            var result = service.DataHasNotChanged();

            Assert.AreEqual((int)HttpStatusCode.NotModified, result.StatusCode);
        }

        [Test]
        public void ShouldGetSingle_WhenETagNoneMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfNoneMatch(It.IsAny<IResourceDataModel>()))
                .Returns(true);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldGetSingle(new FakeResource("a")));
        }

        [Test]
        public void ShouldGetSingle_WhenNotETagNoneMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfNoneMatch(It.IsAny<IResourceDataModel>()))
                .Returns(false);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldGetSingle(new FakeResource("a")));
        }

        [Test]
        public void ShouldUpdate_WhenETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<IResourceDataModel>()))
                .Returns(true);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldUpdate(new FakeResource("a")));
        }

        [Test]
        public void ShouldUpdate_WhenNotETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<IResourceDataModel>()))
                .Returns(false);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldUpdate(new FakeResource("a")));
        }

        [Test]
        public void ShouldDelete_WhenETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<IResourceDataModel>()))
                .Returns(true);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldDelete(new FakeResource("a")));
        }

        [Test]
        public void ShouldDelete_WhenNotETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<IResourceDataModel>()))
                .Returns(false);

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldDelete(new FakeResource("a")));
        }
    }
}
