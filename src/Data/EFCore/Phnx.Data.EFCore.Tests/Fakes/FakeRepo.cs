using Phnx.Data.EFCore.Repositories;
using System.Linq;

namespace Phnx.Data.EFCore.Tests.Fakes
{
    public class FakeRepo : CrudDbSet<FakeDbContext, DataModel, int>
    {
        public FakeRepo(FakeDbContext context) : base(context, db => db.Fakes, true)
        {
        }

        public int CreatedCount { get; private set; }
        public int DeletedCount { get; private set; }
        public int GetAllCount { get; private set; }
        public int GetSingleCount { get; private set; }
        public int UpdatedCount { get; private set; }

        public override void Create(DataModel data)
        {
            CreatedCount++;
            base.Create(data);
        }

        public override void Delete(DataModel data)
        {
            DeletedCount++;
            base.Delete(data);
        }

        public override IQueryable<DataModel> Get()
        {
            GetAllCount++;
            return base.Get();
        }

        public override DataModel GetSingle(int id)
        {
            GetSingleCount++;
            return base.GetSingle(id);
        }

        public override void Update(DataModel data)
        {
            UpdatedCount++;
            base.Update(data);
        }
    }
}
