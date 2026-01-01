using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component;
using CasaAsa.Business.Component.Authentication;
using CasaAsa.Core.BusinessModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IAdminComponent _adminComponent;

        public AdminController(IAuthenticationService authService, IAdminComponent adminComponent)
        {
            _authService = authService;
            _adminComponent = adminComponent;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestLockOrderDate()
        {
            return Ok(await _adminComponent.GetLatestLockOrder());
        }

        [HttpPost]
        public async Task<IActionResult> SetNewLockOrderDate([FromBody] DateOnly newLockOrderDate)
        {
            await _adminComponent.CreateNewLockOrderDate(newLockOrderDate);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CustomerViewModel model)
        {
            var result = await _authService.RegisterAsync(new RegisterRequest());

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);

            return Ok(result);
        }
    }
}
