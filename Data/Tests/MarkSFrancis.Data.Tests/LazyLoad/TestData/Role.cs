namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    class Role : IPrimaryKeyDataModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
