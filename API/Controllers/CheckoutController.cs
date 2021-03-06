﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class CheckoutController : Controller {

		private readonly Business.Logic.CheckoutLogic _checkoutLogic;

		public CheckoutController(Business.Logic.CheckoutLogic checkoutLogic) {
			_checkoutLogic = checkoutLogic;
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(List<Business.Models.Inventory> inventories) {
			try {
				var validation = new Business.Validation.ValidationResult();

				foreach (var inventory in inventories)
					validation.Reasons.AddRange(Business.Validation.Validator.ValidateModel(inventory).Reasons);

				if (validation.IsValid) {
					await _checkoutLogic.InsertListAsync(inventories);
					return Ok();
				}

				return BadRequest(validation.Reasons);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet]
		[Authorize(Roles = nameof(Business.Models.User.Roles.Owner))]
		public async Task<IActionResult> Purchase() {
			try {

				var items = await _checkoutLogic.ListAsync();
				return Ok(items);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}

		[HttpGet("Clear")]
		[Authorize(Roles = nameof(Business.Models.User.Roles.Owner))]
		public async Task<IActionResult> Clear() {
			try {

				await _checkoutLogic.Clear();
				return Ok();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Failure");
			}
		}
	}
}