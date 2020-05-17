using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try
{
    public class TryResultTTests
    {
        [Test]
        public void Succeed_SetsSuccessToTrue()
        {
            var r = TryResult<int>.Succeed(1);

            Assert.IsTrue(r.Success);
        }

        [Test]
        public void Succeed_SetsResult()
        {
            var result = 1;
            var r = TryResult<int>.Succeed(result);

            Assert.AreEqual(result, r.Result);
        }

        [Test]
        public void Succeed_SetsErrorMessageToNull()
        {
            var r = TryResult<int>.Succeed(1);
            Assert.IsNull(r.ErrorMessage);
        }

        [Test]
        public void Fail_SetsSuccessToFalse()
        {
            var r = TryResult<int>.Fail(string.Empty);
            Assert.IsFalse(r.Success);
        }

        [Test]
        public void Fail_SetsResultToDefault()
        {
            var r = TryResult<int>.Fail(string.Empty);

            Assert.AreEqual(default(int), r.Result);
        }

        [Test]
        public void Fail_WhenErrorMessageHasValue_SetsErrorMessage()
        {
            var err = "er";
            var r = TryResult<int>.Fail(err);

            Assert.AreEqual(err, r.ErrorMessage);
        }

        [Test]
        public void Fail_WhenErrorMessageIsNull_SetsSuccessToFalse()
        {
            var r = TryResult<int>.Fail(null);
            Assert.IsFalse(r.Success);
        }

        [Test]
        public void DeconstructWithoutResult_WhenSuccess_Deconstructs()
        {
            var r = TryResult<int>.Succeed(1);

            var (success, errMsg) = r;

            Assert.AreEqual(r.Success, success);
            Assert.AreEqual(r.ErrorMessage, errMsg);
        }

        [Test]
        public void DeconstructWithResult_WhenSuccess_Deconstructs()
        {
            var r = TryResult<int>.Succeed(1);

            var (success, result, errMsg) = r;

            Assert.AreEqual(r.Success, success);
            Assert.AreEqual(r.Result, result);
            Assert.AreEqual(r.ErrorMessage, errMsg);
        }

        [Test]
        public void Deconstruct_WhenFail_Deconstructs()
        {
            var r = TryResult<int>.Fail("err");

            var (success, result, errMsg) = r;

            Assert.AreEqual(r.Success, success);
            Assert.AreEqual(default(int), result);
            Assert.AreEqual(r.ErrorMessage, errMsg);
        }

        [Test]
        public void CastFromString_SetsAsFailWithError()
        {
            var err = "err";
            TryResult<int> r = err;

            Assert.IsFalse(r.Success);
            Assert.AreEqual("err", r.ErrorMessage);
        }

        [Test]
        public void CastFromTuple_WhenSuccess_SetsSuccessAndIgnoresError()
        {
            TryResult<int> r = (true, 1, "err");

            Assert.IsTrue(r.Success);
            Assert.IsNull(r.ErrorMessage);
        }

        [Test]
        public void CastFromTuple_WhenFail_SetsError()
        {
            var err = "err";
            TryResult<int> r = (false, default, err);

            Assert.IsFalse(r.Success);
            Assert.AreEqual("err", r.ErrorMessage);
        }

        [Test]
        public void CastToTupleWithoutResult_WhenSuccess_SetsSuccessAndError()
        {
            var r = TryResult<int>.Succeed(1);

            (bool, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.ErrorMessage, o.Item2);
        }

        [Test]
        public void CastToTupleWithoutResult_WhenFail_SetsSuccessAndError()
        {
            var r = TryResult<int>.Fail("err");

            (bool, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.ErrorMessage, o.Item2);
        }

        [Test]
        public void CastToTupleWithResult_WhenSuccess_SetsSuccessResultAndError()
        {
            var r = TryResult<int>.Succeed(1);

            (bool, int, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.Result, o.Item2);
            Assert.AreEqual(r.ErrorMessage, o.Item3);
        }

        [Test]
        public void CastToTupleWithResult_WhenFail_SetsSuccessResultAndError()
        {
            var r = TryResult<int>.Fail("err");

            (bool, int, string) o = r;

            Assert.AreEqual(r.Success, o.Item1);
            Assert.AreEqual(r.Result, o.Item2);
            Assert.AreEqual(r.ErrorMessage, o.Item3);
        }

        [Test]
        public void ToString_WhenSuccess_DoesNotContainError()
        {
            var r = TryResult<int>.Succeed(1);
            var s = r.ToString();

            Assert.IsFalse(s.Contains("error", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ToString_WhenFail_ContainsError()
        {
            var err = "argnull";
            var r = TryResult<int>.Fail(err);

            var s = r.ToString();

            Assert.IsTrue(s.Contains(err));
        }
    }
}
