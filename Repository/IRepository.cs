using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T, U> 
        where T : class
    {
        //Lovely CRUD
        Task<T> GetByIdAsync(U id);
        Task<T> InsertAsync(T type);
        Task DeleteAsync(T type);
        Task UpdateAsync(T type);
    }

    public interface IList<T>
    {
        Task<IEnumerable<T>> ListAsync();
    }
}