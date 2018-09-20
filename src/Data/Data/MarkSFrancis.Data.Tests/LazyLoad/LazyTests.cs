using MarkSFrancis.Data.LazyLoad;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyTests
    {
        public LazyGetSet<object> GetOnlyLazy =>
            new LazyGetSet<object>(() => null);

        public LazyGetSet<object> GetSetLazy =>
            new LazyGetSet<object>(() => null, o => { });

        [Test]
        public void CreateLazyLoad_WithNullGetFunc_InGetOnly_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LazyGetSet<object>(null));
        }

        [Test]
        public void CreateLazyLoad_WithNullGetFunc_InGetSet_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LazyGetSet<object>(null, o => { }));
        }

        [Test]
        public void CreateLazyLoad_WithNullSetFunc_InGetSet_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LazyGetSet<object>(() => null, null));
        }

        [Test]
        public void LazyLoad_FirstLoad_CallsLoadEventsInOrder()
        {
            var lazy = GetSetLazy;

            int eventsCalledCount = 0;
            int valueCachedCallOrder = 0,
                valueGetCallOrder = 0,
                valueSetCallOrder = 0;

            lazy.ValueCached += (sender, value) => valueCachedCallOrder = ++eventsCalledCount;

            lazy.ValueGet += (sender, value) => valueGetCallOrder = ++eventsCalledCount;

            lazy.ValueSet += (sender, value) => valueSetCallOrder = ++eventsCalledCount;

            var val = lazy.Value;

            Assert.AreEqual(1, valueCachedCallOrder);
            Assert.AreEqual(2, valueGetCallOrder);
            Assert.AreEqual(0, valueSetCallOrder);

            lazy.Value = val;

            Assert.AreEqual(2, valueGetCallOrder);
            Assert.AreEqual(3, valueSetCallOrder);
            Assert.AreEqual(4, valueCachedCallOrder);
        }

        [Test]
        public void SetLazyValue_WithoutSetAction_ThrowsInvalidOperation()
        {
            var lazy = GetOnlyLazy;

            Assert.Throws<NotSupportedException>(() => lazy.Value = null);
        }

        [Test]
        public void SetLazyValue_WithSetAction_CallsSetAction()
        {
            bool wasCalled = false;

            var lazy = new LazyGetSet<object>(() => null, o => wasCalled = true)
            {
                Value = null
            };


            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void GetLazyValue_WithGetFunc_CallsGetFunc()
        {
            bool wasCalled = false;

            var lazy = new LazyGetSet<object>(() =>
            {
                wasCalled = true;
                return null;
            });

            var val = lazy.Value;

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void GetLazyValue_WithNull_LoadsValue()
        {
            var lazy = new LazyGetSet<object>(() => null);

            var val = lazy.Value;

            Assert.AreEqual(null, val);
        }

        [Test]
        public void SetLazyValue_WithNull_SetsCachedValue()
        {
            var lazy =
                new LazyGetSet<object>(() => null, o =>
                    { })
                {
                    Value = 17
                };


            Assert.AreEqual(17, lazy.Value);
        }

        [Test]
        public void GetSetGetGetLazyValue_WithDifferentValues_UpdatesAndUsesCache()
        {
            var firstVal = 167;
            var secondVal = "asdf";
            int timesGetCalled = 0;

            var lazy =
                new LazyGetSet<object>(() =>
                    {
                        ++timesGetCalled;
                        return firstVal;
                    },
                    o => { });

            Assert.AreEqual(firstVal, lazy.Value);
            Assert.AreEqual(1, timesGetCalled);

            lazy.Value = secondVal;
            Assert.AreEqual(1, timesGetCalled);

            Assert.AreEqual(secondVal, lazy.Value);
            Assert.AreEqual(1, timesGetCalled);

            Assert.AreEqual(secondVal, lazy.Value);
            Assert.AreEqual(1, timesGetCalled);
        }
    }
}
