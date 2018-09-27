using MarkSFrancis.Data.EFCore.Repositories;

namespace MarkSFrancis.Data.EFCore.Tests.Fakes
{
    public class FakeRepo : CrudDataRepository<FakeDbContext, DataModel, int>
    {
        public FakeRepo(FakeDbContext context) : base(context, db => db.Fakes)
        {
        }
    }
}
