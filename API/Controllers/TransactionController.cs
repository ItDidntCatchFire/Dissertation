using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(Business.Models.User.Roles.Owner))]
	public class TransactionController : Controller {

		private readonly Business.Logic.TransacationLogic _transacationLogic;

		public TransactionController(Business.Logic.TransacationLogic transacationLogic) {
			_transacationLogic = transacationLogic;
		}

		[HttpGet("Cost")]
		public async Task<IActionResult> GetCost([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo) {
			try {
				return Ok(await _transacationLogic.CalculateCost(dateFrom, dateTo));
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet("Revenue")]
		public async Task<IActionResult> GetRevenue([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo) {
			try {
				return Ok(await _transacationLogic.CalculateRevenue(dateFrom, dateTo));
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}
	}
}