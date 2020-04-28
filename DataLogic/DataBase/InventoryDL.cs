using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Repository;

namespace DataLogic.DataBase
{
    public class InventoryDL : Models.InventoryDL, IInventoryRepository<Models.InventoryDL, Guid>
    {
        
        public async Task<IEnumerable<Models.InventoryDL>> ListAsync()
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            List<Models.InventoryDL> retVal = null;
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Inventory_GetAll";
            cmd.CommandType = CommandType.StoredProcedure;

            using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (rdr.HasRows)
            {
                var inventoryIdOrd = rdr.GetOrdinal(nameof(InventoryId));
                var itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
                var quantityOrd = rdr.GetOrdinal(nameof(Quantity));
                var timeOrd = rdr.GetOrdinal(nameof(Time));
                var exportOrd = rdr.GetOrdinal(nameof(Export));
                var moniesOrd = rdr.GetOrdinal(nameof(Monies));

                retVal = new List<Models.InventoryDL>();
                while (await rdr.ReadAsync())
                {
                    retVal.Add(new InventoryDL()
                        {
                            InventoryId = rdr.GetGuid(inventoryIdOrd),
                            ItemId = rdr.GetGuid(itemIdOrd),
                            Quantity = rdr.GetInt16(quantityOrd),
                            Time = rdr.GetDateTime(timeOrd),
                            Export = rdr.GetBoolean(exportOrd),
                            Monies = rdr.GetDecimal(moniesOrd)
                        }
                    );
                }
            }
            
            rdr.Close();

            return retVal;
        }
        
        public async Task<IEnumerable<Models.InventoryDL>> GetByIdAsync(Guid pInventoryId)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            List<Models.InventoryDL> retVal = null;
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Inventory_GetById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(
                DataFactory.CreateParameterWithValue(cmd, DbType.Binary, ParameterDirection.Input, nameof(pInventoryId),
                    pInventoryId));

            using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (rdr.HasRows)
            {
                var inventoryIdOrd = rdr.GetOrdinal(nameof(InventoryId));
                var itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
                var quantityOrd = rdr.GetOrdinal(nameof(Quantity));
                var timeOrd = rdr.GetOrdinal(nameof(Time));
                var exportOrd = rdr.GetOrdinal(nameof(Export));
                var moniesOrd = rdr.GetOrdinal(nameof(Monies));
                
                retVal = new List<Models.InventoryDL>();
                while (await rdr.ReadAsync())
                {
                    retVal.Add(new InventoryDL()
                        {
                            InventoryId = rdr.GetGuid(inventoryIdOrd),
                            ItemId = rdr.GetGuid(itemIdOrd),
                            Quantity = rdr.GetInt16(quantityOrd),
                            Time = rdr.GetDateTime(timeOrd),
                            Export = rdr.GetBoolean(exportOrd),
                            Monies = rdr.GetDecimal(moniesOrd) 
                        }
                    );
                }
            }
            rdr.Close();

            return retVal;
        }

        public async Task<IEnumerable<Models.InventoryDL>> InsertAsync(IEnumerable<Models.InventoryDL> inventories)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            var t = await c.BeginTransactionAsync();
            try
            {
                foreach (var inventory in inventories)
                {
                    using var cmd = c.CreateCommand();
                    cmd.CommandText = "Inventory_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(
                        DataFactory.CreateParametersWithValue(cmd, 
                            new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                            {
                                (DbType.Binary, ParameterDirection.Input, "p" + nameof(inventory.InventoryId), inventory.InventoryId),
                                (DbType.Binary, ParameterDirection.Input,"p" +  nameof(inventory.ItemId), inventory.ItemId),
                                (DbType.Int16, ParameterDirection.Input,"p" +  nameof(inventory.Quantity), inventory.Quantity),
                                (DbType.DateTime, ParameterDirection.Input, "p" + nameof(inventory.Time), inventory.Time),
                                (DbType.Boolean, ParameterDirection.Input, "p" + nameof(inventory.Export), inventory.Export),
                                (DbType.Decimal, ParameterDirection.Input, "p" + nameof(inventory.Monies), inventory.Monies),
                            }));

                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
                
                t.Commit();
                return inventories;
            }
            catch (Exception e)
            {
                t.Rollback();
                throw;
            }
        }
        
        public Task DeleteAsync(IEnumerable<Models.InventoryDL> type)
            => throw new NotImplementedException();

        public Task UpdateAsync(IEnumerable<Models.InventoryDL> type)
            => throw new NotImplementedException();
    }
}












