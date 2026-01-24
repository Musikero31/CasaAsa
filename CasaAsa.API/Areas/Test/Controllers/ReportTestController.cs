using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportTestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ConfirmUserReport(int id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordReport(int id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestReceipt(int id)
        {
            return Ok();
        }
    }
}
