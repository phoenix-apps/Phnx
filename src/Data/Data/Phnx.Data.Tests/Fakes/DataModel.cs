namespace Phnx.Data.Tests.Fakes
{
    public class DataModel : IIdDataModel<int>
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
