namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    static class RoleRepository
    {
        public static Role GetSingle(int id)
        {
            return new Role
            {
                Name = "Test",
                Id = id
            };
        }
    }
}
