using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try
{
    public class TryResultTests
    {
        [Test]
        public void Succeed_SetsSuccessToTrue()
        {
            var r = TryResult.Succeed();
            Assert.IsTrue(r.Success);
        }

        [Test]
        public void Succeed_SetsErrorMessageToNull()
        {
            var r = TryResult.Succeed();
            Assert.IsNull(r.ErrorMessage);
        }

        [Test]
        public void Fail_SetsSuccessToFalse()
        {
            var r = TryResult.Fail(string.Empty);
            Assert.IsFalse(r.Success);
        }

        [Test]
        public void Fail_WhenErrorMessageHasValue_SetsErrorMessage()
        {
            var err = "er";
            var r = TryResult.Fail(err);

            Assert.AreEqual(err, r.ErrorMessage);
        }

        [Test]
        public void Fail_WhenErrorMessageIsNull_SetsSuccessToFalse()
        {
            var r = TryResult.Fail(null);
            Assert.IsFalse(r.Success);
        }

        [Test]
        public void Deconstruct_WhenSuccess_Deconstructs()
        {
            var r = TryResult.Succeed();

            var (success, errMsg) = r;

            Assert.AreEqual(r.Success, success);
            Assert.AreEqual(r.ErrorMessage, errMsg);
        }

        [Test]
        public void Deconstruct_WhenFail_Deconstructs()
        {
            var r = TryResult.Fail("err");

            var (success, errMsg) = r;

            Assert.AreEqual(r.Success, success);
            Assert.AreEqual(r.ErrorMessage, errMsg);
        }

        [Test]
        public void CastFromString_SetsAsFailWithError()
        {
            var err = "err";
            TryResult r = err;

            Assert.IsFalse(r.Success);
            Assert.AreEqual("err", r.ErrorMessage);
        }

        [Test]
        public void CastFromTuple_WhenSuccess_SetsSuccessAndIgnoresError()
        {
            TryResult r = (true, "err");

            Assert.IsTrue(r.Success);
            Assert.IsNull(r.ErrorMessage);
        }

        [Test]
        public void CastFromTuple_WhenFail_SetsError()
        {
            var err = "err";
            TryResult r = (false, err);

            Assert.IsFalse(r.Success);
            Assert.AreEqual("err", r.ErrorMessage);
        }

        [Test]
        public void CastToTuple_WhenSuccess_SetsSuccessAndError()
        {
            var r = TryResult.Succeed();

            (bool, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.ErrorMessage, o.Item2);
        }

        [Test]
        public void CastToTuple_WhenFail_SetsSuccessAndError()
        {
            var r = TryResult.Fail("err");

            (bool, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.ErrorMessage, o.Item2);
        }

        [Test]
        public void ToString_WhenSuccess_DoesNotContainError()
        {
            var r = TryResult.Succeed();
            var s = r.ToString();

            Assert.IsFalse(s.Contains("error", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ToString_WhenFail_ContainsError()
        {
            var err = "argnull";
            var r = TryResult.Fail(err);

            var s = r.ToString();

            Assert.IsTrue(s.Contains(err));
        }
    }
}
