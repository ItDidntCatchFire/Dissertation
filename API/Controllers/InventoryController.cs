using Export;
using Import;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(Business.Models.User.Roles.Owner))]
	public class InventoryController : Controller,
		IController<IEnumerable<Business.Models.Inventory>, Guid>,
		IDataTransfer {
		private readonly Func<eExport, IExport> _export;
		private readonly Func<eImport, IImport> _import;
		private readonly Business.Logic.InventoryLogic _inventoryLogic;

		public InventoryController(Business.Logic.InventoryLogic inventoryLogic,
			Func<eExport, IExport> exportResolver,
			Func<eImport, IImport> importResolver) {
			_inventoryLogic = inventoryLogic;
			_export = exportResolver;
			_import = importResolver;
		}

		[HttpGet("{inventoryId}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid inventoryId) {
			try {
				if (inventoryId.ToString() != "")
					return Ok(await _inventoryLogic.GetByIdAsync(inventoryId));

				return BadRequest();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpPost]
		public async Task<IActionResult> InsertAsync([FromBody] IEnumerable<Business.Models.Inventory> inventories) {
			try {
				var validation = new Business.Validation.ValidationResult();

				foreach (var inventory in inventories)
					validation.Reasons.AddRange(Business.Validation.Validator.ValidateModel(inventory).Reasons);

				if (validation.IsValid)
					return Ok(await _inventoryLogic.InsertAsync(inventories));

				return BadRequest(validation.Reasons);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet("List")]
		public async Task<IActionResult> GetAllAsync() {
			try {
				return Ok(await getAll());
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet("Export")]
		public async Task<IActionResult> SaveFile([FromRoute] eExport exportType) {
			try {
				var inventories = (await getAll()).ToList();

				var exporter = _export(exportType);

				var result = new FileStreamResult(exporter.Convert(inventories), exporter.ContentType) {
					FileDownloadName = $"Inventory-{DateTime.Now:MM/dd/yyyy HH:mm:ss}{exporter.Extenstion}"
				};
				return result;
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}

		}

		[HttpPost("Import")]
		public async Task<IActionResult> ReadFile([FromBody] string content, [FromRoute] eImport importType) {
			try {
				var importer = _import(importType);
				var inventories = importer.Read<List<Business.Models.Inventory>>(content);
				await _inventoryLogic.InsertAsync(inventories);
				return Ok();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpPost("List")]
		public async Task<IActionResult> InsertInventoryListAsync([FromBody] IEnumerable<Business.Models.Inventory> inventories) {
			try {
				var validation = new Business.Validation.ValidationResult();

				foreach (var inventory in inventories)
					validation.Reasons.AddRange(Business.Validation.Validator.ValidateModel(inventory).Reasons);

				if (validation.IsValid)
					return Ok(await _inventoryLogic.InsertAsync(inventories));

				return BadRequest(validation.Reasons);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet("Stock")]
		public async Task<IActionResult> GetStock() {
			try {
				return Ok(await _inventoryLogic.GetStock());
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		private async Task<IEnumerable<Business.Models.Inventory>> getAll()
			=> (await _inventoryLogic.ListAsync());
	}
}