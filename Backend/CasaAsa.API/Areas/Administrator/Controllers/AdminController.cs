using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component.Administration;
using CasaAsa.Business.Component.Administration.Authentication;
using CasaAsa.Business.Component.Configuration;
using CasaAsa.Business.Constants;
using CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminComponent _adminComponent;
        
        public AdminController(IAdminComponent adminComponent)
        {
            _adminComponent = adminComponent;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetLatestLockOrderDate()
        {
            return Ok(await _adminComponent.GetLatestLockOrderAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetNewLockOrderDate([FromBody] OrderLock model)
        {
            await _adminComponent.CreateNewLockOrderDateAsync(model.NewOrderLockDate);

            return Ok();
        }

        
    }
}
