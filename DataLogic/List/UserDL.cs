using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLogic.List {
	internal static partial class ListStore {
		public static List<Models.UserDL> Users = new List<Models.UserDL>();
	}

	public class UserDL : IUserRepository<Models.UserDL, Guid> {
		public async Task<Models.UserDL> GetByIdAsync(Guid id)
			=> ListStore.Users.FirstOrDefault(x => x.UserId == id);

		public Task<Models.UserDL> InsertAsync(Models.UserDL type)
		=> throw new NotImplementedException();

		public Task DeleteAsync(Models.UserDL type)
			=> throw new NotImplementedException();

		public Task UpdateAsync(Models.UserDL type)
			=> throw new NotImplementedException();
	}
}