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

		[HttpGet]
		public async Task<IActionResult> GetCost(DateTime dateFrom, DateTime dateTo) {
			try {
				return Ok(await _transacationLogic.CalculateCost(dateFrom, dateTo));
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetRevenue(DateTime dateFrom, DateTime dateTo) {
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