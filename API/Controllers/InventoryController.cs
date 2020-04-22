using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles=nameof(Business.Models.User.Roles.Owner))]
    public class InventoryController : Controller
    {
        private readonly Business.Logic.InventoryLogic _inventoryLogic;
        private readonly Func<Export.ExportEnum,Export.IExport> _export;
        
        public InventoryController(Business.Logic.InventoryLogic inventoryLogic, Func<Export.ExportEnum,Export.IExport>  servicesResolver)
        {
            _inventoryLogic = inventoryLogic;
            _export = servicesResolver;
        }

        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetInventoryByInventoryId([FromRoute] Guid inventoryId)
        {
            try
            {
                if (inventoryId.ToString() != "")
                    return Ok(await _inventoryLogic.GetByIdAsync(inventoryId));

                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertInventoryAsync([FromBody] IEnumerable<Business.Models.Inventory> inventories)
        {
            try
            {
                var validation = new Business.Validation.ValidationResult();
                
                foreach (var inventory in inventories)
                    validation.Reasons.AddRange(Business.Validation.Validator.ValidateModel(inventory).Reasons);

                if (validation.IsValid)
                    return Ok(await _inventoryLogic.InsertAsync(inventories));

                return BadRequest(validation.Reasons);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        [HttpPost("List")]
        public async Task<IActionResult> InsertInventoryListAsync(
            [FromBody] IEnumerable<Business.Models.Inventory> inventories)
        {
            try
            {
                var validation = new Business.Validation.ValidationResult();
                
                foreach (var inventory in inventories)
                    validation.Reasons.AddRange(Business.Validation.Validator.ValidateModel(inventory).Reasons);

                if (validation.IsValid)
                    return Ok(await _inventoryLogic.InsertAsync(inventories));

                return BadRequest(validation.Reasons);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> InventoryGetAll()
        {
            try
            {
                return Ok(await getAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        private async Task<IEnumerable<Business.Models.Inventory>> getAll()
            => (await _inventoryLogic.ListAsync());

        [HttpGet("Export{type}")]
        public async Task<IActionResult> SaveFile([FromRoute]Export.ExportEnum type)
        {
            var inventories = await getAll();

            var service = _export(type);
            
            var result = new FileStreamResult(service.Write(inventories), service.ContentType)
            {
                FileDownloadName = $"Inventory-{DateTime.Now:MM/dd/yyyy HH:mm:ss}{service.Extenstion}"
            };
            return result;
        }
    }
}