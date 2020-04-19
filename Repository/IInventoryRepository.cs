using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IInventoryRepository : IRepository<DataLogic.Models.InventoryDL, Guid>
    {
        Task<Guid> InsertListAsync(IEnumerable<DataLogic.Models.InventoryDL> types);
    }
}