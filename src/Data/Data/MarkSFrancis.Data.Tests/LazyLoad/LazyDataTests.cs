using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDataTests
    {
        private static Person SamplePerson(int id = 1)
        {
            return new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            };
        }

        private LazyData<int, Person> CreateTable()
        {
            return new LazyData<int, Person>(PersonRepository.GetSingle);
        }

        [Test]
        public void AddingAnEntry_ThatIsNotCached_AddsToTheCache()
        {
            LazyData<int, Person> people = new LazyData<int, Person>(PersonRepository.GetSingle);

            Person sample = SamplePerson();

            people.AddOrUpdate(sample);

            Assert.AreEqual(1, people.CachedCount);
        }

        [Test]
        public void UpdatingAnEntry_ThatIsCached_ChangesCachedValue()
        {
            LazyData<int, Person> people = new LazyData<int, Person>(PersonRepository.GetSingle);

            string replacementName = "Davidson";

            Person sample = SamplePerson();

            var person = people.Get(1);

            people.AddOrUpdate(sample);

            sample.LastName = replacementName;
            people.AddOrUpdate(sample);

            Assert.AreEqual(1, people.CachedCount);
            Assert.AreEqual(replacementName, sample.LastName);
        }

        [Test]
        public void AddingTwoEntries_ThatAreNotCached_AddsToTheCache()
        {
            var people = CreateTable();

            Person sample = SamplePerson();

            people.AddOrUpdate(sample);

            sample = SamplePerson(2);

            people.AddOrUpdate(sample);

            Assert.AreEqual(2, people.CachedCount);
        }

        [Test]
        public void RemovingAnEntry_ThatIsCached_RemovesFromTheCache()
        {
            LazyData<int, Person> people = new LazyData<int, Person>(PersonRepository.GetSingle);

            Person sample = SamplePerson();

            people.AddOrUpdate(sample);

            people.Remove(sample.Id);

            Assert.AreEqual(0, people.CachedCount);
        }

        [Test]
        public void RemovingAnEntry_ThatIsNotCached_DoesNothing()
        {
            LazyData<int, Person> people = new LazyData<int, Person>(PersonRepository.GetSingle);

            Person sample = SamplePerson();

            people.Remove(sample.Id);

            Assert.AreEqual(0, people.CachedCount);
        }
    }
}
