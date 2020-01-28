using DataLogic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Logic {
	public static class ItemLogic {

		public static async Task<Models.Item> GetItemByItemIdAsync(int itemId) {
			using (SqlConnection c = new SqlConnection(DataFactory.DBConnectionString)) {
				c.Open();
				var item = await ItemDL.GetItemByItemId(c, itemId).ConfigureAwait(false);
				c.Close();

				return new Models.Item() {
					ItemId = item.ItemId,
					Name = item.Name,
					Description = item.Description
				};
			}
		}

		public static async Task<int> InsertItemAsync(Models.Item item) {
			using (SqlConnection c = new SqlConnection(DataFactory.DBConnectionString)) {
				c.Open();

				ItemDL itemDL = new ItemDL() {
					ItemId = item.ItemId,
					Name = item.Name,
					Description = item.Description
				};

				var retVal = await ItemDL.InsertItem(c, itemDL);
				c.Close();
				return retVal;
			}
		}
	}
}
