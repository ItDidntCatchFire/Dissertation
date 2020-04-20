using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T, U>
    {
        //Lovely CRUD
        Task<T> GetByIdAsync(U id);
        Task<T> InsertAsync(T type);
        Task DeleteAsync(T type);
        Task UpdateAsync(T type);
    }
}