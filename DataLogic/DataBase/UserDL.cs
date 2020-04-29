using MySql.Data.MySqlClient;
using Repository;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DataLogic.DataBase {
	public class UserDL : Models.UserDL, IUserRepository<Models.UserDL, Guid> {
		public async Task<Models.UserDL> GetByIdAsync(Guid pUserId) {
			using var c = new MySqlConnection(DataFactory.DBConnectionString);
			c.Open();

			Models.UserDL retVal = null;
			using var cmd = c.CreateCommand();
			cmd.CommandText = "User_GetById";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(
				DataFactory.CreateParameterWithValue(cmd, DbType.Binary, ParameterDirection.Input, nameof(pUserId),
					pUserId));

			using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
			if (rdr.HasRows) {
				var userIdOrd = rdr.GetOrdinal(nameof(UserId));
				var roleOrd = rdr.GetOrdinal(nameof(Role));

				retVal = new Models.UserDL();
				while (await rdr.ReadAsync()) {
					retVal.UserId = rdr.GetGuid(userIdOrd);
					retVal.Role = rdr.GetInt16(roleOrd);
				}
			}

			rdr.Close();

			return retVal;
		}

		public Task<Models.UserDL> InsertAsync(Models.UserDL type)
			=> throw new NotImplementedException();

		public Task DeleteAsync(Models.UserDL type)
			=> throw new NotImplementedException();

		public Task UpdateAsync(Models.UserDL type)
			=> throw new NotImplementedException();
	}
}