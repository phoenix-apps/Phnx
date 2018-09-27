using MarkSFrancis.Data.EFCore.Repositories;
using MarkSFrancis.Data.EFCore.Tests.Fakes;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarkSFrancis.Data.EFCore.Tests.Repositories
{
    public class CrudRepositoryTests
    {
        [Test]
        public void CreatingCrudRepository_WithNullContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CrudDataRepository<FakeDbContext, DataModel, int>(null, d => d.Fakes));
        }

        [Test]
        public void CreatingCrudRepository_WithNullDbSetAccessor_ThrowsArgumentNullException()
        {
            var dbContext = FakeGenerator.DbContext();
            Assert.Throws<ArgumentNullException>(() => new CrudDataRepository<FakeDbContext, DataModel, int>(dbContext, null));
        }

        [Test]
        public void CreatingCrudRepository_WithDbSetAccessorThatReturnsNull_ThrowsArgumentException()
        {
            var dbContext = FakeGenerator.DbContext();
            Assert.Throws<ArgumentException>(() => new CrudDataRepository<FakeDbContext, DataModel, int>(dbContext, db => null));
        }

        [Test]
        public void Add_AddsModel()
        {
            var repo = FakeGenerator.Repo(out var ctx);

            repo.Create(FakeGenerator.Data());

            ctx.SaveChangesAsync().Wait();

            Assert.AreEqual(1, repo.Get().Count());
        }

        [Test]
        public void Update_UpdatesModel()
        {
            var repo = FakeGenerator.Repo(out var ctx);

            var data = FakeGenerator.Data();
            data.FullName = "test1";
            repo.Create(data);
            ctx.SaveChangesAsync().Wait();

            var newName = "test2";
            data.FullName = newName;

            repo.Update(data);
            ctx.SaveChangesAsync().Wait();

            var savedData = repo.GetSingle(data.Id);

            Assert.IsNotNull(savedData);

            Assert.AreEqual(data.Id, savedData.Id);
            Assert.AreEqual(data.FullName, savedData.FullName);
        }

        [Test]
        public void Get_GetsAllInDb()
        {
            var repo = FakeGenerator.Repo(out var ctx);

            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());
            ctx.SaveChangesAsync().Wait();

            Assert.AreEqual(2, repo.Get().Count());
        }

        [Test]
        public void GetSingle_GetsMatchingId()
        {
            var repo = FakeGenerator.Repo(out var ctx);

            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());
            var fake = FakeGenerator.Data();
            repo.Create(fake);
            repo.Create(FakeGenerator.Data());
            repo.Create(FakeGenerator.Data());
            ctx.SaveChangesAsync().Wait();

            var found = repo.GetSingle(fake.Id);

            Assert.IsNotNull(found);
            Assert.AreEqual(fake.Id, found.Id);
            Assert.AreEqual(fake.FullName, found.FullName);
        }
    }
}
