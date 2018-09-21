using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDatabaseTests
    {
        [Test]
        public void SubmittingEntry_ToTheCache_IncreasesCacheCountByOne()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();
            var person = people.GetSingle(1);

            database.AddOrUpdate(1, person);

            Assert.AreEqual(1, database.TotalItemsCachedCount);
            Assert.AreEqual(1, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(0, database.TableItemsCachedCount<Role>());
        }

        [Test]
        public void SubmittingCrossTypeEntries_ToTheCache_IncreasesCacheCountByTwo()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();
            var person = people.GetSingle(1);
            var roles = new RoleRepository();
            var role = roles.GetSingle(1);

            database.AddOrUpdate(1, person);
            database.AddOrUpdate(1, role);

            database.Get(1, people.GetSingle);

            Assert.AreEqual(2, database.TotalItemsCachedCount);

            Assert.AreEqual(1, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(1, database.TableItemsCachedCount<Role>());
        }
    }
}
