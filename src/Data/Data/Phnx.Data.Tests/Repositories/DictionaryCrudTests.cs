using NUnit.Framework;
using Phnx.Data.Repositories;
using Phnx.Data.Tests.Fakes;
using System;
using System.Collections.Generic;

namespace Phnx.Data.Tests.Repositories
{
    public class DictionaryCrudTests
    {
        [Test]
        public void New_WithNullSource_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DictionaryCrud<int, DataModel>(null));
        }

        [Test]
        public void New_WithSource_SetsSource()
        {
            var source = new Dictionary<int, DataModel>();
            var crud = new DictionaryCrud<int, DataModel>(source);

            Assert.AreEqual(source, crud.Source);
        }

        [Test]
        public void New_WithoutSource_CreatesEmptySource()
        {
            var crud = new DictionaryCrud<int, DataModel>();

            Assert.IsNotNull(crud.Source);
            Assert.AreEqual(0, crud.Source.Count);
        }

        [Test]
        public void Add_AddsToSource()
        {
            var crud = FakeGenerator.DictionaryCrud();

            var entry = FakeGenerator.Data(1);
            crud.Create(entry);

            Assert.AreEqual(entry, crud.Source[entry.Id]);
        }

        [Test]
        public void GetAll_GetsAllEntries()
        {
            var crud = FakeGenerator.DictionaryCrud();

            var collection = new List<DataModel>
            {
                FakeGenerator.Data(1),
                FakeGenerator.Data(2),
                FakeGenerator.Data(3),
                FakeGenerator.Data(4)
            };

            foreach (var entry in collection)
            {
                crud.Source.Add(entry.Id, entry);
            }

            CollectionAssert.AreEqual(collection, crud);
        }

        [Test]
        public void GetAllIQueryable_GetsAllEntries()
        {
            var crud = FakeGenerator.DictionaryCrud();

            var collection = new List<DataModel>
            {
                FakeGenerator.Data(1),
                FakeGenerator.Data(2),
                FakeGenerator.Data(3),
                FakeGenerator.Data(4)
            };

            foreach (var entry in collection)
            {
                crud.Source.Add(entry.Id, entry);
            }

            CollectionAssert.AreEqual(collection, ((ICrud<int, DataModel>)crud).Get());
        }

        [Test]
        public void GetSingle_GetsFromSource()
        {
            var crud = FakeGenerator.DictionaryCrud();
            var fake = FakeGenerator.Data(1);

            crud.Source.Add(fake.Id, fake);

            var found = crud.GetSingle(1);
            Assert.AreEqual(fake, found);
        }

        [Test]
        public void Update_UpdatesSource()
        {
            var crud = FakeGenerator.DictionaryCrud();
            var fake = FakeGenerator.Data(1);

            crud.Source.Add(fake.Id, fake);

            var newFake = FakeGenerator.Data(1);
            crud.Update(newFake);

            var found = crud.Source[1];
            Assert.AreEqual(newFake, found);
        }

        [Test]
        public void Delete_ByModel_DeletesFromSource()
        {
            var crud = FakeGenerator.DictionaryCrud();
            var fake = FakeGenerator.Data(1);

            crud.Source.Add(fake.Id, fake);

            crud.Delete(fake);

            Assert.AreEqual(0, crud.Source.Count);
        }

        [Test]
        public void Delete_ByKey_DeletesFromSource()
        {
            var crud = FakeGenerator.DictionaryCrud();
            var fake = FakeGenerator.Data(1);

            crud.Source.Add(fake.Id, fake);

            crud.Delete(fake.Id);

            Assert.AreEqual(0, crud.Source.Count);
        }
    }
}
