using NUnit.Framework;
using Phnx.Data.EFCore.Repositories;
using Phnx.Data.EFCore.Tests.Fakes;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Tests.Repositories
{
    public class CachedCrudTests
    {
        [Test]
        public void New_WithNullCache_ThrowsArgumentNullException()
        {
            var repo = FakeGenerator.Repo();

            Assert.Throws<ArgumentNullException>(() => new CachedCrud<DataModel, int>(null, repo));
        }

        [Test]
        public void New_WithNullCrud_ThrowsArgumentNullException()
        {
            var cache = FakeGenerator.Cache();

            Assert.Throws<ArgumentNullException>(() => new CachedCrud<DataModel, int>(cache, null));
        }

        [Test]
        public void Add_AddsModelToCacheAndUpdatesDatabase()
        {
            var repo = FakeGenerator.CacheRepo(out var data, out var cache);

            repo.Create(FakeGenerator.Data());

            Assert.AreEqual(1, data.CreatedCount);
            Assert.AreEqual(1, cache.TableItemsCachedCount<DataModel>());
        }

        [Test]
        public void Update_UpdatesModelInCache()
        {
            var repo = FakeGenerator.CacheRepo(out var data, out var cache);

            var originalModel = FakeGenerator.Data();
            originalModel.FullName = "test1";
            repo.Create(originalModel);

            var newName = "test2";
            originalModel.FullName = newName;

            repo.Update(originalModel);

            var reloadedModel = repo.GetSingle(originalModel.Id);

            Assert.IsNotNull(reloadedModel);

            Assert.AreEqual(originalModel.Id, reloadedModel.Id);
            Assert.AreEqual(originalModel.FullName, reloadedModel.FullName);

            Assert.AreEqual(0, data.GetSingleCount);
        }

        [Test]
        public void Get_GetsAllInDb()
        {
            var repo = FakeGenerator.Repo();

            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());

            Assert.AreEqual(2, repo.Get().Count());
        }

        [Test]
        public void GetSingle_GetsMatchingId()
        {
            var repo = FakeGenerator.Repo();

            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());
            var fake = FakeGenerator.Data();
            repo.Create(fake);
            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());

            var found = repo.GetSingle(fake.Id);

            Assert.IsNotNull(found);
            Assert.AreEqual(fake.Id, found.Id);
            Assert.AreEqual(fake.FullName, found.FullName);
        }

        [Test]
        public void Delete_WithAutosaveEnabled_Autosaves()
        {
            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, true);

            var dataToSave = FakeGenerator.Data(1);
            repo.Create(dataToSave);

            repo.Delete(dataToSave);

            var savedData = repo.GetSingle(1);
            Assert.IsNull(savedData);
        }

        [Test]
        public void Delete_WithAutosaveDisabled_DoesNotSave()
        {
            // This test, and the Update without autosave test requires change tracking to be disabled.
            // Change tracking being disabled helps to ensure that the Get method returns the in-memory database, not what EF has tracked

            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, true);

            var dataToSave = FakeGenerator.Data(1);
            repo.Create(dataToSave);

            repo.AutoSave = false;

            repo.Delete(dataToSave);

            var savedData = repo.GetSingle(1);
            Assert.NotNull(savedData);
        }
    }
}
