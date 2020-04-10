using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly Business.Logic.ItemLogic _itemLogic;

        public ItemController(Business.Logic.ItemLogic itemLogic)
        {
            _itemLogic = itemLogic;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemByItemId([FromRoute] Guid itemId)
        {
            try
            {
                if (itemId.ToString() != "")
                    return Ok(await _itemLogic.GetByIdAsync(itemId));

                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertItem([FromBody] Business.Models.Item item)
        {
            try
            {
                if (item.IsValid())
                    return Ok(await _itemLogic.InsertAsync(item));

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