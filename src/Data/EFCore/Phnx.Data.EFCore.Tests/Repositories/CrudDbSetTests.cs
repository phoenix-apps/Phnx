using NUnit.Framework;
using Phnx.Data.EFCore.Repositories;
using Phnx.Data.EFCore.Tests.Fakes;
using System;
using System.Linq;

namespace Phnx.Data.EFCore.Tests.Repositories
{
    public class CrudDbSetTests
    {
        [Test]
        public void New_WithNullContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CrudDbSet<FakeDbContext, DataModel, int>(null, d => d.Fakes, false));
        }

        [Test]
        public void New_WithNullDbSetAccessor_ThrowsArgumentNullException()
        {
            var dbContext = FakeGenerator.DbContext();
            Assert.Throws<ArgumentNullException>(() => new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, null, false));
        }

        [Test]
        public void New_WithDbSetAccessorThatReturnsNull_ThrowsArgumentException()
        {
            var dbContext = FakeGenerator.DbContext();
            Assert.Throws<ArgumentException>(() => new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => null, false));
        }

        [Test]
        public void Add_WithAutosaveEnabled_Autosaves()
        {
            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, true);

            var dataToSave = FakeGenerator.Data(1);
            repo.Create(dataToSave);

            var savedData = repo.GetSingle(1);

            Assert.AreEqual(dataToSave.FullName, savedData.FullName);
        }

        [Test]
        public void Add_WithAutosaveDisabled_DoesNotSave()
        {
            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, false);

            var dataToSave = FakeGenerator.Data(1);
            repo.Create(dataToSave);

            var savedData = repo.GetSingle(1);

            Assert.IsNull(savedData);
        }

        [Test]
        public void Add_AddsModel()
        {
            var repo = FakeGenerator.Repo();

            repo.Create(FakeGenerator.Data());

            Assert.AreEqual(1, repo.Get().Count());
        }

        [Test]
        public void Update_WithAutosaveEnabled_Autosaves()
        {
            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, true);

            var dataToSave = FakeGenerator.Data(1);
            repo.Create(dataToSave);
            dataToSave.FullName = FakeGenerator.FullName(false);

            repo.Update(dataToSave);

            var savedData = repo.GetSingle(1);
            Assert.AreEqual(dataToSave.FullName, savedData.FullName);
        }

        [Test]
        public void Update_WithAutosaveDisabled_DoesNotSave()
        {
            // This test, and the Delete without autosave test requires change tracking to be disabled.
            // Change tracking being disabled helps to ensure that the Get method returns the in-memory database, not what EF has tracked

            var dbContext = FakeGenerator.DbContext();
            var repo = new CrudDbSet<FakeDbContext, DataModel, int>(dbContext, db => db.Fakes, true);

            var dataToSave = FakeGenerator.Data(1);
            string originalName = dataToSave.FullName;
            repo.Create(dataToSave);

            dataToSave.FullName = FakeGenerator.FullName(false);
            repo.AutoSave = false;

            repo.Update(dataToSave);

            var savedData = repo.GetSingle(1);

            Assert.AreEqual(originalName, savedData.FullName);
        }

        [Test]
        public void Update_UpdatesModel()
        {
            var repo = FakeGenerator.Repo();

            var data = FakeGenerator.Data();
            data.FullName = "test1";
            repo.Create(data);

            var newName = "test2";
            data.FullName = newName;

            repo.Update(data);

            var savedData = repo.GetSingle(data.Id);

            Assert.IsNotNull(savedData);

            Assert.AreEqual(data.Id, savedData.Id);
            Assert.AreEqual(data.FullName, savedData.FullName);
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
