namespace Phnx.Data.Tests.Lazy.TestData
{
    internal class RoleRepository
    {
        public int TimesLoaded { get; set; }

        public Role GetSingle(int id)
        {
            ++TimesLoaded;

            return new Role
            {
                Name = "Test",
                Id = id
            };
        }
    }
}
