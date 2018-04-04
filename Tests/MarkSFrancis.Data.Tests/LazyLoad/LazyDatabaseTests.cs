using MarkSFrancis.Data.LazyLoad;

namespace MarkSFrancis.Data.Tests.LazyLoad
{
    public class LazyDatabaseTests
    {
        class Person : IPrimaryKeyDataModel<int>
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

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

        class Role : IPrimaryKeyDataModel<int>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

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

        void asdf()
        {
            var loadedPerson = LazyDatabase.Get(1, PersonRepository.GetSingle);

            var loadedRole = LazyDatabase.Get(1, RoleRepository.GetSingle);

            LazyDatabase.AddOrUpdate(loadedPerson.Id, loadedPerson);
        }
    }
}
