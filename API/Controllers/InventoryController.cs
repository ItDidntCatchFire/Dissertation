using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> InsertInventoryAsync([FromBody] Business.Models.Inventory inventory)
        {
            try
            {
                if (inventory.IsValid())
                    return Ok(await _inventoryLogic.InsertAsync(inventory));

                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }
        
        [HttpPost ("List")]
        public async Task<IActionResult> InsertInventoryListAsync([FromBody] IEnumerable<Business.Models.Inventory> inventories)
        {
            try
            {
                foreach (var inventory in inventories)
                    if (!inventory.IsValid())
                        return BadRequest();

                return Ok(await _inventoryLogic.InsertListAsync(inventories));
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
                return Ok(await  _inventoryLogic.GetListAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                                return StatusCode(500, "Failure");
            }
        }
        
    }
}