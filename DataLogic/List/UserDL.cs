using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Repository;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        public static List<Models.UserDL> users = new List<Models.UserDL>();
    }
    
    public class UserDL : Models.UserDL, IUserRepository
    {
        public async Task<Models.UserDL> GetByIdAsync(Guid id)
            => ListStore.users.FirstOrDefault(x => x.UserId == id);

        public Task<Models.UserDL> InsertAsync(Models.UserDL type)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Models.UserDL type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.UserDL type)
        {
            throw new NotImplementedException();
        }
    }
}