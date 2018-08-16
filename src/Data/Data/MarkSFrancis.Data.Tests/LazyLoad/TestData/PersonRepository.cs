namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    static class PersonRepository
    {
        public static Person GetSingle(int id)
        {
            return new Person
            {
                FirstName = "John",
                LastName = "Smith",
                Id = id
            };
        }
    }
}
