namespace Phnx.Data.Tests.Lazy.TestData
{
    internal class PersonRepository
    {
        public int TimesLoaded { get; private set; }

        public Person GetSingle(int id)
        {
            TimesLoaded++;

            return new Person
            {
                FirstName = "John",
                LastName = "Smith",
                Id = id
            };
        }
    }
}
