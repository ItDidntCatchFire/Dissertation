using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly Business.Logic.ItemLogic _itemLogic;

        public ItemsController(Business.Logic.ItemLogic itemLogic)
        {
            _itemLogic = itemLogic;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemByItemId([FromRoute] Guid itemId)
        {
            try
            {
                if (itemId != Guid.Empty)
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
                var validation = Business.Validation.Validator.ValidateModel(item);

                if (validation.IsValid)
                {
                    return Ok(await _itemLogic.InsertAsync(item));
                }
                
                return BadRequest(validation.Reasons);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }
        
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateItem([FromBody] Business.Models.Item item)
        {
            try
            {
                var validation = Business.Validation.Validator.ValidateModel(item);

                if (validation.IsValid)
                {
                    await _itemLogic.UpdateAsync(item);
                    return Ok();
                }
                
                return BadRequest(validation.Reasons);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }
        
        [HttpGet("List")]
        public async Task<IActionResult> ItemsGetAll()
        {
            try
            {
                return Ok(await _itemLogic.ListAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }
    }
}