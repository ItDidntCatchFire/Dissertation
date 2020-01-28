using System;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataLogic {
	public class ItemDL {
		public int ItemId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }


		public static async Task<int> InsertItem<TConnection>(TConnection con, ItemDL item)
			where TConnection : DbConnection, new() {

			int newItemId = -1;
			using (DbCommand cmd = con.CreateCommand()) {
				cmd.CommandText = "[dbo].[InsertItem]";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddRange(
					DataFactory.CreateParametersWithValue(cmd, new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)> {
						(DbType.Int32, ParameterDirection.Output, "ItemId", newItemId),
						(DbType.String, ParameterDirection.Input, "ItemJSON",  JsonConvert.SerializeObject(item))
						}
					)
				);

				await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
				return int.Parse(DataFactory.GetParameterFromName(cmd, nameof(ItemId)).Value.ToString());
			}
		}

		public static async Task<ItemDL> GetItemByItemId<TConnection>(TConnection con, int itemId)
			where TConnection : DbConnection, new() {

			var retVal = new ItemDL();
			using (var cmd = con.CreateCommand()) {
				cmd.CommandText = "[dbo].[GetItemByItemId]";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddRange(
					DataFactory.CreateParametersWithValue(cmd, new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)> {
							(DbType.Int32, ParameterDirection.Input, "ItemId", itemId)
						}
					)
					);


				using (var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false)) {

					if (rdr.HasRows) {
						int itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
						int nameOrd = rdr.GetOrdinal(nameof(Name));
						int descriptionOrd = rdr.GetOrdinal(nameof(Description));

						while (rdr.Read()) {
							retVal.ItemId = rdr.GetInt32(itemIdOrd);
							retVal.Name = rdr.GetString(nameOrd);
							retVal.Description = rdr.GetString(descriptionOrd);
						}

					}
					rdr.Close();
				}
			}
			return retVal;
		}
	}
}
