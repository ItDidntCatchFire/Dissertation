using System;
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


        [HttpGet("{itemId}")]
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
        public async Task<IActionResult> InsertInventory([FromBody] Business.Models.Inventory inventory)
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
    }
}