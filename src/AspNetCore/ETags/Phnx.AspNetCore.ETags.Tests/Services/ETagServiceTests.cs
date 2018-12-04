using NUnit.Framework;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;
using System;

namespace Phnx.AspNetCore.ETags.Tests.Services
{
    public class ETagServiceTests
    {
        public ETagService ETagService { get; }

        public ETagServiceTests()
        {
            ETagService = new ETagService();
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreNull_ReturnsETagNotInRequest()
        {
            var result = ETagService.CheckETagsForModel(null, new object());

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWhitespace_ReturnsETagNotInRequest()
        {
            var result = ETagService.CheckETagsForModel("    ", null);

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, result);
        }

        [Test]
        public void CheckETagsForModel_WhenModelIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ETagService.CheckETagsForModel("asdf", null));
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAndModelIsNull_ReturnsETagNotInRequest()
        {
            var result = ETagService.CheckETagsForModel(null, null);

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWeakAndMatch_ReturnsWeakMatch()
        {
            var model = new FakeResource(Guid.NewGuid().ToString());
            var etag = ETagService.GetWeakETagForModel(model);

            var result = ETagService.CheckETagsForModel(etag, model);

            Assert.AreEqual(ETagMatchResult.WeakMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWeakAndDoNotMatch_ReturnsWeakDoNotMatch()
        {
            var model = new FakeResource(Guid.NewGuid().ToString());
            var etag = "W/\"" + Guid.NewGuid().ToString() + "\"";

            var result = ETagService.CheckETagsForModel(etag, model);

            Assert.AreEqual(ETagMatchResult.WeakDoNotMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongButNotSupported_ReturnsStrongDoNotMatch()
        {
            var model = new FakeDto();
            var etag = "\"" + Guid.NewGuid().ToString() + "\"";

            var result = ETagService.CheckETagsForModel(etag, model);

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongAndMatch_ReturnsStrongMatch()
        {
            var model = new FakeResource(Guid.NewGuid().ToString());
            ETagService.TryGetStrongETagForModel(model, out var etag);

            var result = ETagService.CheckETagsForModel(etag, model);

            Assert.AreEqual(ETagMatchResult.StrongMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongAndDoNotMatch_ReturnsStrongDoNotMatch()
        {
            var model = new FakeResource(Guid.NewGuid().ToString());
            var etag = "\"" + Guid.NewGuid().ToString() + "\"";

            var result = ETagService.CheckETagsForModel(etag, model);

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, result);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ETagService.TryGetStrongETagForModel(null, out _));
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelSupportsStrongTag_GetsStrongTag()
        {
            var version = Guid.NewGuid().ToString();
            var model = new FakeResource(version);
            var expected = "\"" + version + "\"";

            var result = ETagService.TryGetStrongETagForModel(model, out var etag);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, etag);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelConcurrencyIsBroken_ReturnsFalse()
        {
            var model = new BrokenFakeResource();

            var result = ETagService.TryGetStrongETagForModel(model, out var etag);

            Assert.IsFalse(result);
            Assert.IsNull(etag);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelDoesNotSupportStrongTag_ReturnsFalse()
        {
            var model = new FakeDto();

            var result = ETagService.TryGetStrongETagForModel(model, out var etag);

            Assert.IsFalse(result);
            Assert.IsNull(etag);
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ETagService.GetWeakETagForModel(null));
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNotNull_GetsWeakETag()
        {
            var resource = new FakeDto();
            var weakTag = ETagService.GetWeakETagForModel(resource);

            Assert.IsFalse(string.IsNullOrWhiteSpace(weakTag));
            Assert.IsTrue(weakTag.StartsWith("W/\""));
            Assert.IsTrue(weakTag.EndsWith("\""));
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNotNull_HasConsistentResult()
        {
            const int loopCount = 1000;

            var resource = new FakeDto();
            var weakTag = ETagService.GetWeakETagForModel(resource);

            for (int index = 0; index < loopCount; index++)
            {
                var result = ETagService.GetWeakETagForModel(resource);
                Assert.AreEqual(weakTag, result);
            }
        }
    }
}
