using MarkSFrancis.Data.LazyLoad;
using MarkSFrancis.Data.Tests.LazyLoad.TestData;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDictionaryTests
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

        private LazyDictionary<int, Person> CreateNoLifetimeDictionary(out PersonRepository people)
        {
            people = new PersonRepository();
            return new LazyDictionary<int, Person>(people.GetSingle);
        }

        private LazyDictionary<int, Person> CreateLifetimeDictionary(int lifetimeMilliseconds, out PersonRepository people)
        {
            people = new PersonRepository();
            return new LazyDictionary<int, Person>(people.GetSingle, TimeSpan.FromMilliseconds(lifetimeMilliseconds));
        }

        [Test]
        public void CreateDictionary_WhenGetIsNull_WithoutLifetime_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LazyDictionary<int, Person>(null));
        }

        [Test]
        public void CreateDictionary_WhenGetIsNull_WithLifetime_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LazyDictionary<int, Person>(null, TimeSpan.FromSeconds(0)));
        }

        [Test]
        public void AddingAnEntry_ThatIsNotCached_AddsToTheCache()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            Person sample = SamplePerson();

            cache.AddOrUpdate(sample.Id, sample);

            Assert.AreEqual(1, cache.CachedCount);
        }

        [Test]
        public void UpdatingAnEntry_ThatIsCached_ChangesCachedValue()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            string replacementName = "Davidson";

            Person sample = SamplePerson();

            var person = cache.Get(1);

            cache.AddOrUpdate(sample.Id, sample);

            sample.LastName = replacementName;
            cache.AddOrUpdate(sample.Id, sample);

            Assert.AreEqual(1, cache.CachedCount);
            Assert.AreEqual(replacementName, sample.LastName);
        }

        [Test]
        public void AddingTwoEntries_ThatAreNotCached_AddsToTheCache()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            Person sample = SamplePerson();

            cache.AddOrUpdate(sample.Id, sample);

            sample = SamplePerson(2);

            cache.AddOrUpdate(sample.Id, sample);

            Assert.AreEqual(2, cache.CachedCount);
        }

        [Test]
        public void GettingEntry_ThatIsCached_LoadsFromCache()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            var person = cache.Get(1);
            var person2 = cache.Get(1);

            Assert.AreEqual(person, person2);
            Assert.AreEqual(1, people.TimesLoaded);
        }

        [Test]
        public void RemovingAnEntry_ThatIsCached_RemovesFromTheCache()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            Person sample = SamplePerson();

            cache.AddOrUpdate(sample.Id, sample);

            cache.Remove(sample.Id);

            Assert.AreEqual(0, cache.CachedCount);
        }

        [Test]
        public void RemovingAnEntry_ThatIsNotCached_DoesNothing()
        {
            var cache = CreateNoLifetimeDictionary(out var people);

            Person sample = SamplePerson();

            cache.Remove(sample.Id);

            Assert.AreEqual(0, cache.CachedCount);
        }
    }
}
