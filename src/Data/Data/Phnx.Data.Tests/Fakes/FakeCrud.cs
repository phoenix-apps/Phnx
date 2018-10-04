using Phnx.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Data.Tests.Fakes
{
    public class FakeCrud : ICrud<int, DataModel>
    {
        public int CreatedCount { get; private set; }
        public int DeletedCount { get; private set; }
        public int GetSingleCount { get; private set; }
        public int GetAllCount { get; private set; }
        public int UpdateCount { get; private set; }

        public DictionaryCrud<int, DataModel> Source { get; }

        public FakeCrud()
        {
            Source = new DictionaryCrud<int, DataModel>();
        }

        public void Create(DataModel data)
        {
            Source.Create(data);
            CreatedCount++;
        }

        public void Delete(DataModel data)
        {
            Source.Delete(data);
            DeletedCount++;
        }

        IQueryable<DataModel> ICrud<int, DataModel>.Get()
        {
            var result = ((ICrud<int, DataModel>)Source).Get();
            GetAllCount++;
            return result;
        }

        public IEnumerable<DataModel> Get()
        {
            var result = Source.Get();
            GetAllCount++;
            return result;
        }

        public DataModel GetSingle(int id)
        {
            var found = Source.GetSingle(id);
            GetSingleCount++;

            return found;
        }

        public void Update(DataModel data)
        {
            Source.Update(data);

            UpdateCount++;
        }
    }
}
