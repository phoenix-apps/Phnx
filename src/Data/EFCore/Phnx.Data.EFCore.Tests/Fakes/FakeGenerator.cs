using Microsoft.EntityFrameworkCore;
using System;

namespace Phnx.Data.EFCore.Tests.Fakes
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

        public static string FullName()
        {
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

        public static FakeDbContext DbContext()
        {
            var options = new DbContextOptionsBuilder<FakeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FakeDbContext(options);
        }

        public static FakeRepo Repo(out FakeDbContext ctx)
        {
            ctx = DbContext();
            return new FakeRepo(ctx);
        }
    }
}
