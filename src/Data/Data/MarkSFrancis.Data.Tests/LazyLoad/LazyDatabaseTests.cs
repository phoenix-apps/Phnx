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

            database.TryAddTable<int, Person>(people.GetSingle);
            database.TryAddOrUpdate(1, person);

            Assert.AreEqual(1, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(0, database.TableItemsCachedCount<Role>());
        }

        [Test]
        public void SubmittingCrossTypeEntries_ToTheCache_IncreasesCacheCountByTwo()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();
            var roles = new RoleRepository();
            var person = people.GetSingle(1);
            var role = roles.GetSingle(1);

            database.AddOrUpdate(1, person, people.GetSingle);
            database.AddOrUpdate(1, role, roles.GetSingle);

            Assert.AreEqual(1, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(1, database.TableItemsCachedCount<Role>());
        }

        [Test]
        public void GetFromDatabaseWith1Table_Twice_LoadsOnceFromDatabaseOnceFromCache()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();

            database.Get(1, people.GetSingle);
            database.Get(1, people.GetSingle);

            Assert.AreEqual(1, people.TimesLoaded);
        }

        [Test]
        public void GetFromDatabaseWithMultipleTablesAndKeys_WithMultipleRequests_LoadsOnceFromDatabasePerKey()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();
            var roles = new RoleRepository();

            database.TryAddTable<int, Person>(people.GetSingle);
            database.TryAddTable<int, Role>(roles.GetSingle);

            database.TryGet<int, Person>(1, out _);
            database.TryGet<int, Person>(1, out _);
            database.TryGet<int, Person>(2, out _);
            database.TryGet<int, Person>(2, out _);
            database.TryGet<int, Role>(1, out _);
            database.TryGet<int, Role>(2, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);
            database.TryGet<int, Role>(7, out _);

            Assert.AreEqual(2, people.TimesLoaded);
            Assert.AreEqual(3, roles.TimesLoaded);
        }
    }
}
