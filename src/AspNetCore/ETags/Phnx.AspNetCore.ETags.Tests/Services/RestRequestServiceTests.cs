using Moq;
using NUnit.Framework;
using Phnx.AspNetCore.ETags.Models;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;
using System;

namespace Phnx.AspNetCore.ETags.Tests.Services
{
    public class RestRequestServiceTests
    {
        [Test]
        public void New_WithNullETagService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagRequestService<FakeResource>(null));
        }

        [Test]
        public void New_WithETagService_SetsETagService()
        {
            var mockETagService = new Mock<IETagService>();

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.AreEqual(mockETagService.Object, requestService.ETagService);
        }

        [Test]
        public void ShouldGetSingle_WhenETagNoneMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfNoneMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldGetSingle(new FakeResource("a")));
        }

        [Test]
        public void ShouldGetSingle_WhenNotETagNoneMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfNoneMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldGetSingle(new FakeResource("a")));
        }

        [Test]
        public void ShouldUpdate_WhenETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldUpdate(new FakeResource("a")));
        }

        [Test]
        public void ShouldUpdate_WhenNotETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldUpdate(new FakeResource("a")));
        }

        [Test]
        public void ShouldDelete_WhenETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsTrue(requestService.ShouldDelete(new FakeResource("a")));
        }

        [Test]
        public void ShouldDelete_WhenNotETagMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();
            mockETagService
                .Setup(m =>
                    m.CheckIfMatch(It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            var requestService = new ETagRequestService<FakeResource>(mockETagService.Object);

            Assert.IsFalse(requestService.ShouldDelete(new FakeResource("a")));
        }
    }
}
