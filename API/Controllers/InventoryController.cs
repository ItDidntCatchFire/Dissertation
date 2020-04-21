using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly Business.Logic.InventoryLogic _inventoryLogic;

        public InventoryController(Business.Logic.InventoryLogic inventoryLogic)
        {
            _inventoryLogic = inventoryLogic;
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
                var retVal = new List<Business.Models.Inventory>();
                
                foreach (var inventory in await _inventoryLogic.ListAsync())
                    retVal.Add(new Business.Models.Inventory()
                    {
                        InventoryId = inventory.InventoryId,
                        ItemId = inventory.ItemId,
                        Time = inventory.Time,
                        Export = inventory.Export,
                        Quantity = inventory.Quantity,
                        Monies = inventory.Monies
                    });

                return Ok(retVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }
    }
}