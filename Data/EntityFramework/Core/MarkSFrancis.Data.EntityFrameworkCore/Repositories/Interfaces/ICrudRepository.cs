using System.Linq;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EntityFrameworkCore.Repositories.Interfaces
{
    public interface ICrudRepository<T, in TKey>
    {
        void Create(T data);

        IQueryable<T> GetAll();

        T GetSingle(TKey id);

        void Update(T data);

        void Delete(T data);

        Task SaveChanges();
    }
}
