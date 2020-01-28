using Business.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : Controller {

		[HttpGet("{itemId}")]
		public async Task<IActionResult> GetItemByItemId([FromRoute] int itemId) {
			try {
				if (itemId > 0)
					return Ok(await ItemLogic.GetItemByItemIdAsync(itemId));
				else
					return BadRequest();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}

		}

		[HttpPost]
		public async Task<IActionResult> InsertItem([FromBody] Business.Models.Item item) {
			try {
				if (item.IsValid())
					return Ok(await ItemLogic.InsertItemAsync(item));
				else
					return BadRequest();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}

		}
	}
}