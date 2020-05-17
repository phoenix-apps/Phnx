using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions
{
    public class AsyncExtensionsTests
    {
        [Test]
        public void StartingAsyncMethod_WithoutContext_RunsMethod()
        {
            int? result = null;
            Action methodStub = () => result = 0;

            var task = methodStub.InvokeAsync(false);

            task.Wait();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingAsyncMethod_WithContext_RunsMethod()
        {
            int? result = null;
            Action methodStub = () => result = 0;

            var task = methodStub.InvokeAsync(true);

            task.Wait();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingAsyncFunc_WithoutContext_RunsMethod()
        {
            int? result = null;
            Func<int?> methodStub = () => 0;

            var task = methodStub.InvokeAsync(false);

            task.Wait();
            result = task.Result;
            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingAsyncFunc_WithContext_RunsMethod()
        {
            int? result = null;
            Func<int?> methodStub = () => 0;

            var task = methodStub.InvokeAsync(true);

            task.Wait();
            result = task.Result;
            Assert.AreEqual(0, result);
        }
    }
}
