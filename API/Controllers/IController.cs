using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers {
	public interface IController<T, U> {
		Task<IActionResult> GetByIdAsync([FromRoute] U Id);
		Task<IActionResult> InsertAsync([FromBody] T type);
		Task<IActionResult> GetAllAsync();
	}

	public interface IDataTransfer {
		Task<IActionResult> SaveFile(eExport exportType);
		Task<IActionResult> ReadFile(string content, eImport importType);
	}
}