namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    internal class RoleRepository
    {
        public int TimesCalled { get; set; }

        public Role GetSingle(int id)
        {
            ++TimesCalled;

            return new Role
            {
                Name = "Test",
                Id = id
            };
        }
    }
}
