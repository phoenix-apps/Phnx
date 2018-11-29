using Moq;
using NUnit.Framework;
using Phnx.AspNetCore.Rest.Models;
using Phnx.AspNetCore.Rest.Services;
using Phnx.AspNetCore.Rest.Tests.Fakes;
using System;

namespace Phnx.AspNetCore.Rest.Tests.Services
{
    public class RestRequestServiceTests
    {
        [Test]
        public void New_WithNullETagService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RestRequestService<FakeResource>(null));
        }

        [Test]
        public void New_WithETagService_SetsETagService()
        {
            var mockETagService = new Mock<IETagService>();

            var requestService = new RestRequestService<FakeResource>(mockETagService.Object);

            Assert.AreEqual(mockETagService.Object, requestService.ETagService);
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
