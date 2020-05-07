using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Logic {
	public class TransacationLogic {

		private readonly Repository.IInventoryRepository<DataLogic.Models.InventoryDL, Guid> _inventoryRepository;

		public TransacationLogic(Repository.IInventoryRepository<DataLogic.Models.InventoryDL, Guid> inventoryRepository) {
			_inventoryRepository = inventoryRepository;
		}

		public async Task<decimal> CalculateRevenue(DateTime dateFrom, DateTime dateTo) {
			var inventoryDLs = await _inventoryRepository.ListAsync();
			var totalRevenue = inventoryDLs.Where(x => x.Export == true && x.Time >= dateFrom && x.Time <= dateTo).Sum(x => (x.Monies * x.Quantity));

			return totalRevenue;
		}

		public async Task<decimal> CalculateCost(DateTime dateFrom, DateTime dateTo) {
			var inventoryDLs = await _inventoryRepository.ListAsync();

			var totalCost = inventoryDLs.Where(x => x.Export == false && x.Time >= dateFrom && x.Time <= dateTo).Sum(x => (x.Monies * x.Quantity));

			return totalCost;
		}
	}
}