using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly Business.Logic.UserLogic _userLogic;
        
        public UserController(Business.Logic.UserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody]Guid userId)
        {
            try
            {
                if (userId.ToString() != "")
                    return Ok(await _userLogic.GetByIdAsync(userId));

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