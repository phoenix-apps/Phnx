using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDatabaseTests
    {
        [Test]
        public void AddingTable_WithNullLoader_ThrowsArgumentNullException()
        {
            var database = new LazyDatabase();

            Assert.Throws<ArgumentNullException>(() => database.TryAddTable<int, Person>(null));
        }

        [Test]
        public void SubmittingEntry_ToTheCache_IncreasesCacheCountByOne()
        {
            var database = Fake.Database(true);
            var people = new PersonRepository();
            var person = people.GetSingle(1);

            database.TryAddOrUpdate(1, person);

            Assert.AreEqual(1, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(0, database.TableItemsCachedCount<Role>());
        }

        [Test]
        public void SubmittingCrossTypeEntries_ToTheCache_IncreasesCacheCountByTwo()
        {
            var database = Fake.Database();
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
            var database = Fake.Database(out var people);

            database.Get(1, people.GetSingle);
            database.Get(1, people.GetSingle);

            Assert.AreEqual(1, people.TimesLoaded);
        }

        [Test]
        public void GetFromDatabaseWithMultipleTablesAndKeys_WithMultipleRequests_LoadsOnceFromDatabasePerKey()
        {
            var database = Fake.Database(out var people, out var roles);

            database.TryGet(1, out Person p);
            database.TryGet(1, out p);
            database.TryGet(2, out p);
            database.TryGet(2, out p);

            database.TryGet(1, out Role r);
            database.TryGet(2, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);
            database.TryGet(7, out r);

            Assert.AreEqual(2, people.TimesLoaded);
            Assert.AreEqual(3, roles.TimesLoaded);
        }

        [Test]
        public void GetFromDatabase_WhenLifetimeHasExpired_Reloads()
        {
            var database = new LazyDatabase(TimeSpan.Zero);
            var people = new PersonRepository();

            database.TryAddTable<int, Person>(people.GetSingle);

            database.TryGet(1, out Person p);
            database.TryGet(1, out p);
            database.Get(1, people.GetSingle);

            Assert.AreEqual(3, people.TimesLoaded);
        }

        [Test]
        public void GetFromDatabase_WhenLifetimeIsOverridenAndHasExpired_Reloads()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();

            database.TryAddTable<int, Person>(people.GetSingle, TimeSpan.Zero);

            database.TryGet(1, out Person p);
            database.TryGet(1, out p);
            database.Get(1, people.GetSingle);

            Assert.AreEqual(3, people.TimesLoaded);
        }

        [Test]
        public void GetFromDatabase_WhenLifetimeHasNotExpired_DoesNotReload()
        {
            var database = new LazyDatabase(TimeSpan.FromHours(100));
            var people = new PersonRepository();

            database.Get(1, people.GetSingle);
            database.TryGet(1, out Person p);
            database.TryGet(1, out p);

            Assert.AreEqual(1, people.TimesLoaded);
        }

        [Test]
        public void GetFromDatabase_WhenLifetimeIsOverridenAndHasNotExpired_DoesNotReload()
        {
            var database = new LazyDatabase(TimeSpan.Zero);
            var people = new PersonRepository();

            database.TryAddTable<int, Person>(people.GetSingle, null);

            database.TryGet(1, out Person p);
            database.TryGet(1, out p);
            database.Get(1, people.GetSingle);

            Assert.AreEqual(1, people.TimesLoaded);
        }

        [Test]
        public void LazyAddingTable_WhenThereAreNoTables_SetsTableCountToOne()
        {
            var database = new LazyDatabase();
            var people = new PersonRepository();

            database.Get(1, people.GetSingle);

            Assert.AreEqual(1, database.TotalTablesCount);
        }

        [Test]
        public void EagerAddingTwoTables_WhenThereAreNoTables_SetsTableCountToTwo()
        {
            var database = Fake.Database(true, true);

            Assert.AreEqual(2, database.TotalTablesCount);
        }

        [Test]
        public void TryGetCache_WithNoTableConfigured_ReturnsFalse()
        {
            var database = new LazyDatabase();

            var result = database.TryGetCache<int, Person>(out var people);

            Assert.IsFalse(result);
            Assert.IsNull(people);
        }

        [Test]
        public void TryGetCache_WithTableConfigured_GetsTable()
        {
            var database = Fake.Database(true);

            database.TryGet(1, out Person _);
            database.TryGet(2, out Person _);

            var result = database.TryGetCache<int, Person>(out var cachedPeople);

            Assert.IsTrue(result);
            Assert.IsNotNull(cachedPeople);
            Assert.AreEqual(2, cachedPeople.Count());
        }

        [Test]
        public void TryGet_WithNoTableConfigured_ReturnsFalse()
        {
            var database = new LazyDatabase();

            var result = database.TryGet<int, Person>(1, out var person);

            Assert.IsFalse(result);
            Assert.IsNull(person);
        }

        [Test]
        public void Get_WithNullGet_ThrowsArgumentNull()
        {
            var database = new LazyDatabase();

            Assert.Throws<ArgumentNullException>(() => database.Get<int, object>(1, null));
        }

        [Test]
        public void AddTable_WithNullGet_ThrowsArgumentNull()
        {
            var database = new LazyDatabase();

            Assert.Throws<ArgumentNullException>(() => database.TryAddTable<int, object>(null));
        }

        [Test]
        public void Add_WithNullGet_ThrowsArgumentNull()
        {
            var database = new LazyDatabase();

            Assert.Throws<ArgumentNullException>(() => database.AddOrUpdate<int, object>(1, null, null));
        }

        [Test]
        public void TryRemoveTable_WhenTableNotConfigured_ReturnsFalse()
        {
            var database = new LazyDatabase();

            var result = database.TryRemoveTable<Person>();

            Assert.IsFalse(result);
        }

        [Test]
        public void TryRemoveTable_WhenTableConfigured_ReducesTableCachedCountByOne()
        {
            var database = Fake.Database(true, true);

            database.TryRemoveTable<Person>();

            Assert.AreEqual(1, database.TotalTablesCount);
        }

        [Test]
        public void TryAddOrUpdate_WhenTableNotConfigured_ReturnsFalse()
        {
            var database = new LazyDatabase();

            var result = database.TryAddOrUpdate(1, new Person());

            Assert.IsFalse(result);
        }

        [Test]
        public void TryAddOrUpdate_WhenTableConfigured_ReturnsTrue()
        {
            var database = Fake.Database(true);

            var result = database.TryAddOrUpdate(1, new Person());

            Assert.IsTrue(result);
        }

        [Test]
        public void TryRemove_WhenTableNotConfigured_ReturnsFalse()
        {
            var database = new LazyDatabase();

            var result = database.TryRemove<int, Person>(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryRemove_WhenTableConfiguredAndEntryNotPresent_ReturnsFalse()
        {
            var database = Fake.Database(true);

            var result = database.TryRemove<int, Person>(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryRemove_WhenTableConfiguredAndEntryPresent_ReturnsTrue()
        {
            var database = Fake.Database(true);
            database.TryGet<int, Person>(1, out _);

            var result = database.TryRemove<int, Person>(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void Clear_WhenNothingCachedOrConfigured_DoesNothing()
        {
            var database = new LazyDatabase();

            database.Clear();

            Assert.AreEqual(0, database.TotalTablesCount);
        }

        [Test]
        public void Clear_WhenRecordsCached_ClearsAll()
        {
            var database = Fake.Database(true, true);
            database.TryGet<int, Person>(1, out _);
            database.TryGet<int, Person>(2, out _);
            database.TryGet<int, Role>(1, out _);
            database.TryGet<int, Role>(2, out _);

            database.Clear();

            Assert.AreEqual(0, database.TableItemsCachedCount<Person>());
            Assert.AreEqual(0, database.TableItemsCachedCount<Role>());
            Assert.AreEqual(0, database.TotalTablesCount);
        }
    }
}
