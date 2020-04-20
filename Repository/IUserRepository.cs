using System;

namespace Repository
{
    public interface IUserRepository : IRepository<DataLogic.Models.UserDL, Guid>
    {
        
    }
}