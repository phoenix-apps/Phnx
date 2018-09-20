using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDatabaseTests
    {
        [SetUp]
        public void Setup()
        {
            // NOTE: The LazyDatabase must be cleared before each test, and tests cannot be ran in parallel
            LazyDatabase.Clear();
        }

        [Test]
        public void SubmittingEntry_ToTheCache_IncreasesCacheCountByOne()
        {
            var people = new PersonRepository();
            var person = people.GetSingle(1);

            LazyDatabase.AddOrUpdate(1, person);

            Assert.AreEqual(1, LazyDatabase.TotalItemsCachedCount);

            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Person)));
        }

        [Test]
        public void SubmittingCrossTypeEntries_ToTheCache_IncreasesCacheCountByTwo()
        {
            var people = new PersonRepository();
            var person = people.GetSingle(1);
            var roles = new RoleRepository();
            var role = roles.GetSingle(1);

            LazyDatabase.AddOrUpdate(1, person);
            LazyDatabase.AddOrUpdate(1, role);

            LazyDatabase.Get(1, people.GetSingle);

            Assert.AreEqual(2, LazyDatabase.TotalItemsCachedCount);

            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Person)));
            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Role)));
        }
    }
}
