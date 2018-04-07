namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    class Person : IPrimaryKeyDataModel<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
