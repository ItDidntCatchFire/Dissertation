using System;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Repository;

namespace DataLogic.DataBase
{
    public class ItemDL : Models.ItemDL, IItemRepository<Models.ItemDL, Guid>
    {
        public Task<IEnumerable<Models.ItemDL>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.ItemDL>> InsertListAsync(IEnumerable<Models.ItemDL> types)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.ItemDL> GetByIdAsync(Guid id)
        {
            using var c = new SqlConnection(DataFactory.DBConnectionString);
            c.Open();

            var retVal = new ItemDL();
            using var cmd = c.CreateCommand();
            cmd.CommandText = "[dbo].[GetItemByItemId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(
                DataFactory.CreateParametersWithValue(cmd,
                    new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                    {
                        (DbType.String, ParameterDirection.Input, nameof(ItemId), id)
                    }
                )
            );

            using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (rdr.HasRows)
            {
                var itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
                var nameOrd = rdr.GetOrdinal(nameof(Name));
                var descriptionOrd = rdr.GetOrdinal(nameof(Description));

                while (rdr.Read())
                {
                    retVal.ItemId = rdr.GetGuid(itemIdOrd);
                    retVal.Name = rdr.GetString(nameOrd);
                    retVal.Description = rdr.GetString(descriptionOrd);
                }
            }

            rdr.Close();

            return retVal;
        }

        public async Task<Models.ItemDL> InsertAsync(Models.ItemDL type)
        {
            using var c = new SqlConnection(DataFactory.DBConnectionString);
            c.Open();
            using var cmd = c.CreateCommand();
            cmd.CommandText = "[dbo].[InsertItem]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(
                DataFactory.CreateParametersWithValue(cmd,
                    new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                    {
                        (DbType.String, ParameterDirection.Output, "ItemId", Guid.NewGuid().ToString()),
                        (DbType.String, ParameterDirection.Input, "ItemJSON", JsonConvert.SerializeObject(type))
                    }
                )
            );
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            return type;
        }

        public Task DeleteAsync(Models.ItemDL type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.ItemDL type)
        {
            throw new NotImplementedException();
        }
    }
}