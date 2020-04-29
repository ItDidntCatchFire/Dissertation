using System;
using System.Threading.Tasks;

namespace Business.Logic {
	public class UserLogic : ILogic<Models.User, Guid> {
		private readonly Repository.IUserRepository<DataLogic.Models.UserDL, Guid> _userRepository;

		public UserLogic(Repository.IUserRepository<DataLogic.Models.UserDL, Guid> userRepository) {
			_userRepository = userRepository;
		}

		public async Task<Models.User> GetByIdAsync(Guid id) {
			var type = await _userRepository.GetByIdAsync(id);

			return new Models.User() {
				UserId = type.UserId,
				Role = (Models.User.Roles)type.Role
			};
		}

		public Task<Models.User> InsertAsync(Models.User type)
			=> throw new NotImplementedException();

		public Task DeleteAsync(Models.User type)
			=> throw new NotImplementedException();

		public Task UpdateAsync(Models.User type)
			=> throw new NotImplementedException();
	}
}