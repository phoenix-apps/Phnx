using NUnit.Framework;
using Phnx.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Data.Tests.Seeds
{
    public class SeedGroupTests
    {
        [Test]
        public void New_WithNullArray_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SeedGroup(null));
        }

        [Test]
        public void New_WithNullEntryInArray_ThrowsArgumentException()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, null, action
            }.Select(a => (Seed)a)
            .ToArray();

            Assert.Throws<ArgumentException>(() => new SeedGroup(seeds));
        }

        [Test]
        public void New_WithValidArray_DoesNotThrow()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(a => (Seed)a)
            .ToArray();

            Assert.DoesNotThrow(() => new SeedGroup(seeds));
        }

        [Test]
        public void New_WithNullIEnumerable_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SeedGroup((IEnumerable<ISeed>)null));
        }

        [Test]
        public void New_WithValidIEnumerable_DoesNotThrow()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(a => (Seed)a);

            Assert.DoesNotThrow(() => new SeedGroup(seeds));
        }

        [Test]
        public void New_WithNullEntryInIEnumerable_ThrowsArgumentException()
        {
            void action() { }

            var seeds = new List<Action>
            {
                action, null, action
            }.Select(a => (Seed)a);

            Assert.Throws<ArgumentException>(() => new SeedGroup(seeds));
        }

        [Test]
        public void Add_Null_ThrowsArgumentNullException()
        {
            var seedGroup = new SeedGroup();
            ISeed seed = null;

            Assert.Throws<ArgumentNullException>(() => seedGroup.Add(seed));
        }

        [Test]
        public void Add_NotNull_DoesNotThrow()
        {
            var seedGroup = new SeedGroup();
            ISeed seed = new Seed(() => { });

            Assert.DoesNotThrow(() => seedGroup.Add(seed));
        }

        [Test]
        public void RunGroup_With3Entries_ExecutesAll()
        {
            int runCount = 0;
            void action() => ++runCount;

            var seeds = new List<Action>
            {
                action, action, action
            }.Select(s => (Seed)s);

            var seedGroup = new SeedGroup(seeds);

            Assert.AreEqual(0, runCount);

            seedGroup.Run();

            Assert.AreEqual(3, runCount);
        }

        [Test]
        public void RunGroup_With0Entries_DoesNotThrow()
        {
            var seedGroup = new SeedGroup();

            Assert.DoesNotThrow(() => seedGroup.Run());
        }
    }
}
