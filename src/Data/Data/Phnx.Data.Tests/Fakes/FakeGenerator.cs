using Phnx.Data.LazyLoad;
using Phnx.Data.Repositories;
using System;

namespace Phnx.Data.Tests.Fakes
{
    public static class FakeGenerator
    {
        private static Random rnd = new Random();

        private static string[] FirstNames = new string[]
        {
            "Oliver",
            "George",
            "Harry",
            "Jack",
            "Jacob",
            "Noah",
            "Olivia",
            "Amelia",
            "Emily",
            "Isla",
            "Ava"
        };

        private static string[] Surnames = new string[]
        {
            "Smith",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "Davis",
            "Miller",
            "Wilson",
            "Moore",
            "Taylor"
        };

        public static string FullName(bool looksReal = true)
        {
            if (!looksReal)
            {
                return Guid.NewGuid().ToString();
            }

            var firstName = FirstNames[rnd.Next(0, FirstNames.Length)];

            var surname = Surnames[rnd.Next(0, Surnames.Length)];

            return firstName + " " + surname;
        }

        public static DataModel Data(int id = 0)
        {
            return new DataModel
            {
                FullName = FullName(),
                Id = id
            };
        }

        public static LazyDatabase Cache()
        {
            return new LazyDatabase();
        }

        public static DictionaryCrud<int, DataModel> DictionaryCrud()
        {
            return new DictionaryCrud<int, DataModel>();
        }

        public static CachedCrud<int, DataModel> CachedCrud(out LazyDatabase database, out FakeCrud repo)
        {
            repo = new FakeCrud();
            database = Cache();

            return new CachedCrud<int, DataModel>(database, repo);
        }
    }
}
