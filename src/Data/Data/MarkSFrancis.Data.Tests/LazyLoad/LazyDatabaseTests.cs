using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDatabaseTests
    {
        // NOTE: The LazyDatabase must be cleared before each test, and tests cannot be ran in parallel

        [Test]
        public void SubmittingEntry_ToTheCache_IncreasesCacheCountByOne()
        {
            LazyDatabase.Clear();

            var person = PersonRepository.GetSingle(1);

            LazyDatabase.AddOrUpdate(1, person);

            Assert.AreEqual(1, LazyDatabase.TotalItemsCachedCount);

            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Person)));
        }

        [Test]
        public void SubmittingCrossTypeEntries_ToTheCache_IncreasesCacheCountByTwo()
        {
            LazyDatabase.Clear();

            var person = PersonRepository.GetSingle(1);
            var role = RoleRepository.GetSingle(1);

            LazyDatabase.AddOrUpdate(1, person);
            LazyDatabase.AddOrUpdate(1, role);

            LazyDatabase.Get(1, PersonRepository.GetSingle);

            Assert.AreEqual(2, LazyDatabase.TotalItemsCachedCount);

            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Person)));
            Assert.AreEqual(1, LazyDatabase.TableItemsCachedCount(typeof(Role)));
        }
    }
}
