using NUnit.Framework;
using Phnx.Data.Repositories;
using Phnx.Data.Tests.Fakes;
using System;
using System.Linq;

namespace Phnx.Data.Tests.Repositories
{
    public class CachedCrudTests
    {
        [Test]
        public void New_WithNullCache_ThrowsArgumentNullException()
        {
            var source = FakeGenerator.DictionaryCrud();

            Assert.Throws<ArgumentNullException>(() => new CachedCrud<int, DataModel>(null, source));
        }

        [Test]
        public void New_WithNullCrud_ThrowsArgumentNullException()
        {
            var cache = FakeGenerator.Cache();

            Assert.Throws<ArgumentNullException>(() => new CachedCrud<int, DataModel>(cache, null));
        }

        [Test]
        public void Add_AddsModelToCacheAndUpdatesSource()
        {
            var repo = FakeGenerator.CachedCrud(out var cache, out var data);

            repo.Create(FakeGenerator.Data());

            Assert.AreEqual(1, data.CreatedCount);
            Assert.AreEqual(1, cache.TableItemsCachedCount<DataModel>());
        }

        [Test]
        public void Update_UpdatesModelInCache()
        {
            var repo = FakeGenerator.CachedCrud(out var cache, out var data);

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
        public void Get_GetsAllInSource()
        {
            var repo = FakeGenerator.CachedCrud(out _, out var data);

            // Simulate pre-existing data
            data.Source.Create(FakeGenerator.Data(1));
            data.Source.Create(FakeGenerator.Data(2));
            data.Source.Create(FakeGenerator.Data(3));
            data.Source.Create(FakeGenerator.Data(4));

            Assert.AreEqual(4, repo.Get().Count());
        }

        [Test]
        public void GetSingle_GetsFromSourceThenCache()
        {
            var repo = FakeGenerator.CachedCrud(out _, out var data);

            data.Source.Create(FakeGenerator.Data(1));

            repo.GetSingle(1);
            repo.GetSingle(1);

            Assert.AreEqual(1, data.GetSingleCount);
        }

        [Test]
        public void Delete_RemovesFromCacheAndSource()
        {
            var repo = FakeGenerator.CachedCrud(out _, out var data);

            data.Source.Create(FakeGenerator.Data(1));

            repo.GetSingle(1);
            repo.Delete(FakeGenerator.Data(1));

            Assert.AreEqual(1, data.GetSingleCount);
            Assert.AreEqual(1, data.DeletedCount);
            Assert.AreEqual(0, repo.Get().Count());
        }
    }
}
