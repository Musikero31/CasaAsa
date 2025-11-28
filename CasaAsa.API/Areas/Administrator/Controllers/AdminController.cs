using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminComponent _component;

        public AdminController(IAdminComponent component)
        {
            _component = component;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestLockOrderDate()
        {
            return Ok(await _component.GetLatestLockOrder());
        }

        [HttpPost]
        public async Task<IActionResult> SetNewLockOrderDate([FromBody] DateOnly newLockOrderDate)
        {
            await _component.CreateNewLockOrderDate(newLockOrderDate);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CustomerViewModel model)
        {

            return Ok();
        }
    }
}
