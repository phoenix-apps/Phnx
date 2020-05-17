using Phnx.Data.Lazy;
using System;

namespace Phnx.Data.Tests.Fakes
{
    public static class FakeGenerator
    {
        private static readonly Random _rnd = new Random();

        private static readonly string[] _firstNames = new string[]
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

        private static readonly string[] _surnames = new string[]
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

            var firstName = _firstNames[_rnd.Next(0, _firstNames.Length)];

            var surname = _surnames[_rnd.Next(0, _surnames.Length)];

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
    }
}
