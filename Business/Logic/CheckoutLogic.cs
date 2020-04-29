using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic {
	public class CheckoutLogic {

		private readonly Repository.ICheckoutRepository<DataLogic.Models.InventoryDL, Guid> _checkoutRepository;

		public CheckoutLogic(Repository.ICheckoutRepository<DataLogic.Models.InventoryDL, Guid> checkoutRepository) {
			_checkoutRepository = checkoutRepository;
		}

		public async Task<IEnumerable<Models.Inventory>> ListAsync() {
			var inventoryDLs = await _checkoutRepository.ListAsync();

			var retVal = new List<Models.Inventory>();

			foreach (var inventoryDL in inventoryDLs)
				retVal.Add(new Models.Inventory() {
					InventoryId = inventoryDL.InventoryId,
					ItemId = inventoryDL.ItemId,
					Time = inventoryDL.Time,
					Export = inventoryDL.Export,
					Quantity = inventoryDL.Quantity,
					Monies = inventoryDL.Monies
				});

			return retVal;
		}

		public async Task Clear() {
			await _checkoutRepository.ClearAsync();
		}


		public async Task InsertListAsync(IEnumerable<Models.Inventory> inventories) {
			var inventoryDLs = new List<DataLogic.Models.InventoryDL>();
			foreach (var inventory in inventories)
				inventoryDLs.Add(new DataLogic.Models.InventoryDL() {
					InventoryId = inventory.InventoryId,
					ItemId = inventory.ItemId,
					Time = inventory.Time,
					Export = inventory.Export,
					Quantity = inventory.Quantity,
					Monies = inventory.Monies
				});

			await _checkoutRepository.InsertListAsync(inventoryDLs);
		}
	}
}
