using System;
using System.Threading.Tasks;

namespace Repository
{
    public interface IItemRepository : IRepository<DataLogic.Models.ItemDL, Guid>
    {
    }
}