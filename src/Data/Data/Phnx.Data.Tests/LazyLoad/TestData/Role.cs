namespace Phnx.Data.Tests.LazyLoad.TestData
{
    internal class Role : IIdDataModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
