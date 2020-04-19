using System.Collections.Generic;
using System.Threading.Tasks;
using DataLogic.Models;

namespace Repository
{
    public interface IRepository<T, U>
    {
        //Lovely CRUD
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetByIdAsync(U id);
        Task<T> InsertAsync(T type);
        Task DeleteAsync(T type);
        Task UpdateAsync(T type);
    }
}