using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyGetSetTests
    {
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
        public void FirstLoad_CallsLoadEventsInOrder()
        {
            var lazy = new LazyGetSetTester<object>(null, null).LazyGetSet;

            int eventsCalledCount = 0,
                valueCachedCallOrder = 0,
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
            var lazy = new LazyGetSetTester<object>();

            Assert.Throws<NotSupportedException>(() => lazy.LazyGetSet.Value = null);
        }

        [Test]
        public void SetLazyValue_WithSetAction_CallsSetAction()
        {
            var lazy = new LazyGetSetTester<object>(null, null);
            lazy.LazyGetSet.Value = null;

            Assert.GreaterOrEqual(1, lazy.SetCount);
        }

        [Test]
        public void GetLazyValue_WithGetFunc_CallsGetFunc()
        {
            var lazy = new LazyGetSetTester<object>();

            _ = lazy.LazyGetSet.Value;

            Assert.GreaterOrEqual(1, lazy.GetCount);
        }

        [Test]
        public void GetLazyValue_WithNull_LoadsValue()
        {
            var lazy = new LazyGetSetTester<object>(() => null);

            var val = lazy.LazyGetSet.Value;

            Assert.AreEqual(null, val);
        }

        [Test]
        public void SetLazyValue_WithNull_SetsCachedValue()
        {
            var lazy = new LazyGetSetTester<object>(null, o => { });

            lazy.LazyGetSet.Value = null;

            Assert.AreEqual(null, lazy.LazyGetSet.Value);
        }

        [Test]
        public void GetSetLazy_WithDifferentValues_UpdatesAndUsesCache()
        {
            var firstVal = 167;
            var secondVal = "asdf";

            var lazy = new LazyGetSetTester<object>(() => firstVal, null);

            Assert.AreEqual(firstVal, lazy.LazyGetSet.Value);
            Assert.AreEqual(1, lazy.GetCount);

            lazy.LazyGetSet.Value = secondVal;
            Assert.AreEqual(1, lazy.GetCount);

            Assert.AreEqual(secondVal, lazy.LazyGetSet.Value);
            Assert.AreEqual(1, lazy.GetCount);
        }

        [Test]
        public void ClearCache_WhenItWasPreviouslyCached_SetsIsCachedToFalseAndReloadsOnNextRequest()
        {
            var lazy = new LazyGetSetTester<object>();

            _ = lazy.LazyGetSet.Value;

            Assert.IsTrue(lazy.LazyGetSet.IsCached);
            Assert.AreEqual(1, lazy.GetCount);

            lazy.LazyGetSet.ClearCache();

            Assert.IsFalse(lazy.LazyGetSet.IsCached);

            _ = lazy.LazyGetSet.Value;

            Assert.IsTrue(lazy.LazyGetSet.IsCached);
            Assert.AreEqual(2, lazy.GetCount);
        }
    }
}
