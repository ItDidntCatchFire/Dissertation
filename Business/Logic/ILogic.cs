using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Logic
{
    public interface ILogic<T, U>
    {
        //Lovely CRUD
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetByIdAsync(U id);
        Task<T> InsertAsync(T type);
        Task DeleteAsync(T type);
        Task UpdateAsync(T type);
    }
}