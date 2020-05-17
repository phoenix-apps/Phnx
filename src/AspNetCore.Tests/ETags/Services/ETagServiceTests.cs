using System;
using NUnit.Framework;
using Phnx.AspNetCore.ETags.Services;
using Phnx.AspNetCore.ETags.Tests.Fakes;

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
            ETagMatchResult result = ETagService.CheckETagsForModel(null, new object());

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWhitespace_ReturnsETagNotInRequest()
        {
            ETagMatchResult result = ETagService.CheckETagsForModel("    ", null);

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
            ETagMatchResult result = ETagService.CheckETagsForModel(null, null);

            Assert.AreEqual(ETagMatchResult.ETagNotInRequest, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWeakAndMatch_ReturnsWeakMatch()
        {
            var model = new FakeStrongResource(Guid.NewGuid().ToString());
            var eTag = ETagService.GetWeakETagForModel(model);

            ETagMatchResult result = ETagService.CheckETagsForModel(eTag, model);

            Assert.AreEqual(ETagMatchResult.WeakMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreWeakAndDoNotMatch_ReturnsWeakDoNotMatch()
        {
            var model = new FakeStrongResource(Guid.NewGuid().ToString());
            var eTag = "W/\"" + Guid.NewGuid().ToString() + "\"";

            ETagMatchResult result = ETagService.CheckETagsForModel(eTag, model);

            Assert.AreEqual(ETagMatchResult.WeakDoNotMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongButNotSupported_ReturnsStrongDoNotMatch()
        {
            var model = new FakeWeakResource();
            var eTag = "\"" + Guid.NewGuid().ToString() + "\"";

            ETagMatchResult result = ETagService.CheckETagsForModel(eTag, model);

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongAndMatch_ReturnsStrongMatch()
        {
            var model = new FakeStrongResource(Guid.NewGuid().ToString());
            ETagService.TryGetStrongETagForModel(model, out var eTag);

            ETagMatchResult result = ETagService.CheckETagsForModel(eTag, model);

            Assert.AreEqual(ETagMatchResult.StrongMatch, result);
        }

        [Test]
        public void CheckETagsForModel_WhenETagsAreStrongAndDoNotMatch_ReturnsStrongDoNotMatch()
        {
            var model = new FakeStrongResource(Guid.NewGuid().ToString());
            var eTag = "\"" + Guid.NewGuid().ToString() + "\"";

            ETagMatchResult result = ETagService.CheckETagsForModel(eTag, model);

            Assert.AreEqual(ETagMatchResult.StrongDoNotMatch, result);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelIsNull_ReturnsFalse()
        {
            var result = ETagService.TryGetStrongETagForModel(null, out _);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelSupportsStrongTag_GetsStrongTag()
        {
            var version = Guid.NewGuid().ToString();
            var model = new FakeStrongResource(version);
            var expected = "\"" + version + "\"";

            var result = ETagService.TryGetStrongETagForModel(model, out var eTag);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, eTag);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelConcurrencyIsBroken_ReturnsFalse()
        {
            var model = new BrokenFakeResource();

            var result = ETagService.TryGetStrongETagForModel(model, out var eTag);

            Assert.IsFalse(result);
            Assert.IsNull(eTag);
        }

        [Test]
        public void TryGetStrongETagForModel_WhenModelDoesNotSupportStrongTag_ReturnsFalse()
        {
            var model = new FakeWeakResource();

            var result = ETagService.TryGetStrongETagForModel(model, out var eTag);

            Assert.IsFalse(result);
            Assert.IsNull(eTag);
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ETagService.GetWeakETagForModel(null));
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNotNull_GetsWeakETag()
        {
            var resource = new FakeWeakResource();
            var weakTag = ETagService.GetWeakETagForModel(resource);

            Assert.IsFalse(string.IsNullOrWhiteSpace(weakTag));
            Assert.IsTrue(weakTag.StartsWith("W/\""));
            Assert.IsTrue(weakTag.EndsWith("\""));
        }

        [Test]
        public void TryGetWeakETagForModel_WhenModelIsNotNull_HasConsistentResult()
        {
            const int loopCount = 1000;

            var resource = new FakeWeakResource();
            var weakTag = ETagService.GetWeakETagForModel(resource);

            for (var index = 0; index < loopCount; index++)
            {
                var result = ETagService.GetWeakETagForModel(resource);
                Assert.AreEqual(weakTag, result);
            }
        }
    }
}
