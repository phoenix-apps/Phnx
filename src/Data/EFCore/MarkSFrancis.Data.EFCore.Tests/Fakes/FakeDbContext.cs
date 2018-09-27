using Microsoft.EntityFrameworkCore;

namespace MarkSFrancis.Data.EFCore.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        public FakeDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<DataModel> Fakes { get; set; }
    }
}
