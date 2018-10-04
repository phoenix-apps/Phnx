using NUnit.Framework;
using Phnx.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Phnx.Data.Tests.Seeds
{
    public class SeedGroupAsyncTests
    {
        [Test]
        public void New_WithNullArray_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SeedGroupAsync(null));
        }

        [Test]
        public void New_WithNullEntryInArray_ThrowsArgumentException()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, null, action
            }.Select(a => (SeedAsync)a)
            .ToArray();

            Assert.Throws<ArgumentException>(() => new SeedGroupAsync(seeds));
        }

        [Test]
        public void New_WithValidArray_DoesNotThrow()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(a => (SeedAsync)a)
            .ToArray();

            Assert.DoesNotThrow(() => new SeedGroupAsync(seeds));
        }

        [Test]
        public void New_WithNullIEnumerable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SeedGroupAsync((IEnumerable<ISeedAsync>)null));
        }

        [Test]
        public void New_WithValidIEnumerable_DoesNotThrow()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(a => (SeedAsync)a);

            Assert.DoesNotThrow(() => new SeedGroupAsync(seeds));
        }

        [Test]
        public void New_WithNullEntryInIEnumerable_ThrowsArgumentException()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, null, action
            }.Select(a => (SeedAsync)a);

            Assert.Throws<ArgumentException>(() => new SeedGroupAsync(seeds));
        }

        [Test]
        public void Add_Null_ThrowsArgumentNullException()
        {
            var seedGroup = new SeedGroupAsync();
            ISeedAsync seed = null;

            Assert.Throws<ArgumentNullException>(() => seedGroup.Add(seed));
        }

        [Test]
        public void Add_NotNull_DoesNotThrow()
        {
            var seedGroup = new SeedGroupAsync();
            ISeedAsync seed = new SeedAsync(() => { });

            Assert.DoesNotThrow(() => seedGroup.Add(seed));
        }

        [Test]
        public void RunAsyncGroup_With3Entries_InParallel_ExecutesAll()
        {
            int runCount = 0;
            void action() =>
                Interlocked.Increment(ref runCount);

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(s => (SeedAsync)s);

            var SeedGroupAsync = new SeedGroupAsync(seeds);

            Assert.AreEqual(0, runCount);

            SeedGroupAsync.RunAsync(true).Wait();

            Assert.AreEqual(3, runCount);
        }

        [Test]
        public void RunAsyncGroup_With3Entries_InSeries_ExecutesAll()
        {
            int runCount = 0;
            void action() => ++runCount;

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(s => (SeedAsync)s);

            var SeedGroupAsync = new SeedGroupAsync(seeds);

            Assert.AreEqual(0, runCount);

            SeedGroupAsync.RunAsync(false).Wait();

            Assert.AreEqual(3, runCount);
        }

        [Test]
        public void RunGroup_With0Entries_InSeries_DoesNotThrow()
        {
            var SeedGroupAsync = new SeedGroupAsync();

            Assert.DoesNotThrow(() => SeedGroupAsync.RunAsync(false).Wait());
        }

        [Test]
        public void RunGroup_With0Entries_InParallel_DoesNotThrow()
        {
            var SeedGroupAsync = new SeedGroupAsync();

            Assert.DoesNotThrow(() => SeedGroupAsync.RunAsync(true).Wait());
        }
    }
}
