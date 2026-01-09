using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component;
using CasaAsa.Business.Component.Authentication;
using CasaAsa.Core.BusinessModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IAdminComponent _adminComponent;
        private readonly IMapper _mapper;

        public AdminController(IAuthenticationService authService,
                               IAdminComponent adminComponent,
                               IMapper mapper)
        {
            _authService = authService;
            _adminComponent = adminComponent;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestLockOrderDate()
        {
            return Ok(await _adminComponent.GetLatestLockOrderAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SetNewLockOrderDate([FromBody] DateOnly newLockOrderDate)
        {
            await _adminComponent.CreateNewLockOrderDateAsync(newLockOrderDate);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CustomerViewModel model)
        {
            try
            {
                var register = _mapper.Map<RegisterRequest>(model);
                var result = await _adminComponent.RegisterAsync(register);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
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
