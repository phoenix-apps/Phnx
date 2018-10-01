using MarkSFrancis.Data.LazyLoad;

namespace MarkSFrancis.Data.Tests.LazyLoad.TestData
{
    public static class Fake
    {
        public static LazyDatabase Database(bool includePeople = false, bool includeRoles = false)
        {
            var database = new LazyDatabase();

            if (includePeople)
            {
                var people = new PersonRepository();
                database.TryAddTable<int, Person>(people.GetSingle);
            }

            if (includeRoles)
            {
                var roles = new RoleRepository();
                database.TryAddTable<int, Role>(roles.GetSingle);
            }

            return database;
        }

        internal static LazyDatabase Database(out PersonRepository people)
        {
            var database = Database();

            people = new PersonRepository();
            database.TryAddTable<int, Person>(people.GetSingle);

            return database;
        }

        internal static LazyDatabase Database(out PersonRepository people, out RoleRepository roles)
        {
            var database = Database(out people);

            roles = new RoleRepository();
            database.TryAddTable<int, Role>(roles.GetSingle);

            return database;
        }
    }
}
