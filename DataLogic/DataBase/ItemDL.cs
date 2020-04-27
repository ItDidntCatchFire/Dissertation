using System;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Repository;

namespace DataLogic.DataBase
{
    public class ItemDL : Models.ItemDL, IItemRepository<Models.ItemDL, Guid>
    {
        public async Task<IEnumerable<Models.ItemDL>> ListAsync()
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            List<Models.ItemDL> retVal = null;
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Item_GetAll";
            cmd.CommandType = CommandType.StoredProcedure;

            using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (rdr.HasRows)
            {
                var itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
                var nameOrd = rdr.GetOrdinal(nameof(Name));
                var descriptionOrd = rdr.GetOrdinal(nameof(Description));
                var shelfLifeOrd = rdr.GetOrdinal(nameof(ShelfLife));
                var buyPriceOrd = rdr.GetOrdinal(nameof(BuyPrice));
                var sellPriceOrd = rdr.GetOrdinal(nameof(SellPrice));

                retVal = new List<Models.ItemDL>();
                while (await rdr.ReadAsync())
                {
                    retVal.Add(new ItemDL()
                        {
                            ItemId = rdr.GetGuid(itemIdOrd),
                            Name = rdr.GetString(nameOrd),
                            Description = rdr.GetString(descriptionOrd),
                            ShelfLife = rdr.GetInt16(shelfLifeOrd),
                            BuyPrice = rdr.GetDecimal(buyPriceOrd),
                            SellPrice = rdr.GetDecimal(sellPriceOrd)        
                        }
                    );
                }
            }
            
            rdr.Close();

            return retVal;
        }

        public async Task<IEnumerable<Models.ItemDL>> InsertListAsync(IEnumerable<Models.ItemDL> items)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            var t = await c.BeginTransactionAsync();
            try
            {
                foreach (var item in items)
                {
                    using var cmd = c.CreateCommand();
                    cmd.CommandText = "Item_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(
                        DataFactory.CreateParametersWithValue(cmd, 
                            new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                            {
                                (DbType.Binary, ParameterDirection.Input, "p" + nameof(item.ItemId), item.ItemId),
                                (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Name), item.Name),
                                (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Description), item.Description),
                                (DbType.Int16, ParameterDirection.Input, "p" + nameof(item.ShelfLife), item.ShelfLife),
                                (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.BuyPrice), item.BuyPrice),
                                (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.SellPrice), item.SellPrice),
                            }));

                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
                
                t.Commit();
                return items;
            }
            catch (Exception e)
            {
                t.Rollback();
                throw;
            }
        }

        public async Task<Models.ItemDL> GetByIdAsync(Guid pItemId)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            Models.ItemDL retVal = null;
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Item_GetById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(
                DataFactory.CreateParameterWithValue(cmd, DbType.Binary, ParameterDirection.Input, nameof(pItemId),
                    pItemId));

            using var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            if (rdr.HasRows)
            {
                var itemIdOrd = rdr.GetOrdinal(nameof(ItemId));
                var nameOrd = rdr.GetOrdinal(nameof(Name));
                var descriptionOrd = rdr.GetOrdinal(nameof(Description));
                var shelfLifeOrd = rdr.GetOrdinal(nameof(ShelfLife));
                var buyPriceOrd = rdr.GetOrdinal(nameof(BuyPrice));
                var sellPriceOrd = rdr.GetOrdinal(nameof(SellPrice));

                retVal = new Models.ItemDL();
                while (await rdr.ReadAsync())
                {
                    retVal.ItemId = rdr.GetGuid(itemIdOrd);
                    retVal.Name = rdr.GetString(nameOrd);
                    retVal.Description = rdr.GetString(descriptionOrd);
                    retVal.ShelfLife = rdr.GetInt16(shelfLifeOrd);
                    retVal.BuyPrice = rdr.GetDecimal(buyPriceOrd);
                    retVal.SellPrice = rdr.GetDecimal(sellPriceOrd);
                }
            }
            
            rdr.Close();

            return retVal;
        }

        public async Task<Models.ItemDL> InsertAsync(Models.ItemDL item)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Item_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(
                DataFactory.CreateParametersWithValue(cmd, 
                    new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                        {
                            (DbType.Binary, ParameterDirection.Input, "p" + nameof(item.ItemId), item.ItemId),    
                            (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Name), item.Name),
                            (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Description), item.Description),
                            (DbType.Int16, ParameterDirection.Input, "p" + nameof(item.ShelfLife), item.ShelfLife),
                            (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.BuyPrice), item.BuyPrice),
                            (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.SellPrice), item.SellPrice),
                        }));

            var result = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            return item;
        }

        public Task DeleteAsync(Models.ItemDL type)
            => throw new NotImplementedException();

        public async Task UpdateAsync(Models.ItemDL item)
        {
            using var c = new MySqlConnection(DataFactory.DBConnectionString);
            c.Open();
            
            using var cmd = c.CreateCommand();
            cmd.CommandText = "Item_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(
                DataFactory.CreateParametersWithValue(cmd, 
                    new List<(DbType dbType, ParameterDirection direction, string parameterName, object value)>
                    {
                        (DbType.Binary, ParameterDirection.Input, "p" + nameof(item.ItemId), item.ItemId),    
                        (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Name), item.Name),
                        (DbType.String, ParameterDirection.Input,"p" +  nameof(item.Description), item.Description),
                        (DbType.Int16, ParameterDirection.Input, "p" + nameof(item.ShelfLife), item.ShelfLife),
                        (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.BuyPrice), item.BuyPrice),
                        (DbType.Decimal, ParameterDirection.Input, "p" + nameof(item.SellPrice), item.SellPrice),
                    }));

            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}