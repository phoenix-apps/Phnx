using System;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;

namespace Phnx.AspNetCore.ETags.Tests.Services
{
    public class ETagRequestServiceTests
    {
        public ETagRequestService GetService(Mock<IETagService> mockETagService = null, string ifMatchHeader = null, string ifNoneMatchHeader = null)
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

            var eTagRequestService = new ETagRequestService(new FakeActionContextAccessor(), eTagService);

            if (ifMatchHeader != null)
            {
                eTagRequestService.RequestHeaders.Add("If-Match", new StringValues(ifMatchHeader));
            }
            if (ifNoneMatchHeader != null)
            {
                eTagRequestService.RequestHeaders.Add("If-None-Match", new StringValues(ifNoneMatchHeader));
            }

            return eTagRequestService;
        }

        [Test]
        public void New_WithNullActionContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagRequestService(null, new ETagService()));
        }

        [Test]
        public void New_WithNullETagService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ETagRequestService(new FakeActionContextAccessor(), null));
        }

        [Test]
        public void ShouldGetSingle_WhenETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldGet = service.ShouldGetSingle(new object());

            Assert.IsTrue(shouldGet);
        }

        [Test]
        public void ShouldGetSingle_WhenModelIsNullAndETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldGet = service.ShouldGetSingle(null);

            Assert.IsTrue(shouldGet);
        }

        [Test]
        public void ShouldGetSingle_WhenModelIsNullAndETagIsUsed_ThrowsArgumentNullException()
        {
            ETagRequestService service = GetService(ifNoneMatchHeader: "a");
            Assert.Throws<ArgumentNullException>(() => service.ShouldGetSingle(null));
        }

        [Test]
        public void ShouldGetSingle_WhenETagMatches_ReturnsFalse()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            ETagRequestService service = GetService(mockETagService, ifNoneMatchHeader: "a");
            var shouldGet = service.ShouldGetSingle(new object());

            Assert.IsFalse(shouldGet);
        }

        [Test]
        public void ShouldGetSingle_WhenETagDoesNotMatch_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            ETagRequestService service = GetService(mockETagService, ifNoneMatchHeader: "a");
            var shouldGet = service.ShouldGetSingle(new object());

            Assert.IsTrue(shouldGet);
        }

        [Test]
        public void ShouldUpdate_WhenETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldUpdate = service.ShouldUpdate(new object());

            Assert.IsTrue(shouldUpdate);
        }

        [Test]
        public void ShouldUpdate_WhenModelIsNullAndETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldUpdate = service.ShouldUpdate(null);

            Assert.IsTrue(shouldUpdate);
        }

        [Test]
        public void ShouldUpdate_WhenModelIsNullAndETagIsUsed_ThrowsArgumentNullException()
        {
            ETagRequestService service = GetService(ifMatchHeader: "a");
            Assert.Throws<ArgumentNullException>(() => service.ShouldUpdate(null));
        }

        [Test]
        public void ShouldUpdate_WhenETagMatches_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            ETagRequestService service = GetService(mockETagService, ifMatchHeader: "a");
            var shouldUpdate = service.ShouldUpdate(new object());

            Assert.IsTrue(shouldUpdate);
        }

        [Test]
        public void ShouldUpdate_WhenETagDoesNotMatch_ReturnsFalse()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            ETagRequestService service = GetService(mockETagService, ifMatchHeader: "a");
            var shouldUpdate = service.ShouldUpdate(new object());

            Assert.IsFalse(shouldUpdate);
        }

        [Test]
        public void ShouldDelete_WhenETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldDelete = service.ShouldDelete(new object());

            Assert.IsTrue(shouldDelete);
        }

        [Test]
        public void ShouldDelete_WhenModelIsNullAndETagNotUsed_ReturnsTrue()
        {
            ETagRequestService service = GetService();
            var shouldDelete = service.ShouldDelete(null);

            Assert.IsTrue(shouldDelete);
        }

        [Test]
        public void ShouldDelete_WhenModelIsNullAndETagIsUsed_ThrowsArgumentNullException()
        {
            ETagRequestService service = GetService(ifMatchHeader: "a");
            Assert.Throws<ArgumentNullException>(() => service.ShouldDelete(null));
        }

        [Test]
        public void ShouldDelete_WhenETagMatches_ReturnsTrue()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.Match);

            ETagRequestService service = GetService(mockETagService, ifMatchHeader: "a");
            var shouldDelete = service.ShouldDelete(new object());

            Assert.IsTrue(shouldDelete);
        }

        [Test]
        public void ShouldDelete_WhenETagDoesNotMatch_ReturnsFalse()
        {
            var mockETagService = new Mock<IETagService>();

            mockETagService.Setup(e => e.CheckETags(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(ETagMatchResult.DoNotMatch);

            ETagRequestService service = GetService(mockETagService, ifMatchHeader: "a");
            var shouldDelete = service.ShouldDelete(new object());

            Assert.IsFalse(shouldDelete);
        }
    }
}
