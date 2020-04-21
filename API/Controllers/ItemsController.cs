using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly Business.Logic.ItemLogic _itemLogic;
        private readonly Func<Export.ExportEnum,Export.IExport> _export;
        
        public ItemsController(Business.Logic.ItemLogic itemLogic, Func<Export.ExportEnum,Export.IExport>  servicesResolver)
        {
            _itemLogic = itemLogic;
            _export = servicesResolver;
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
        [Authorize(Roles=nameof(Business.Models.User.Roles.Owner))]
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
        [Authorize(Roles=nameof(Business.Models.User.Roles.Owner))]
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
        [Authorize(Roles=nameof(Business.Models.User.Roles.Owner))]
        public async Task<IActionResult> ItemsGetAll()
        {
            try
            {
                return Ok(await getAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Failure");
            }
        }

        private async Task<List<Business.Models.Item>> getAll()
            => (await _itemLogic.ListAsync()).ToList();
        
        //[Authorize(Roles=nameof(Business.Models.User.Roles.Owner))]
        [HttpGet("Export{type}")]
        public async Task<IActionResult> SaveFile([FromRoute]Export.ExportEnum type)
        {
            var items = await getAll();

            var service = _export(type);
            var result = new FileStreamResult(service.Write(items), service.ContentType)
            {
                FileDownloadName = $"Items-{DateTime.Now:MM/dd/yyyy HH:mm:ss}{service.Extenstion}"
            };
            return result;
        }
    }
}