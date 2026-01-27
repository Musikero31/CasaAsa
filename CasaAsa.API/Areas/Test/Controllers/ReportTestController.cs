using CasaAsa.Business.Component.Configuration;
using CasaAsa.Business.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportTestController : ControllerBase
    {
        private readonly IHtmlParser _parser;

        public ReportTestController(IHtmlParser parser)
        {
            _parser = parser;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmUserReport(int id)
        {
            var result = await _parser.ParseByReportType(ApplicationSettingsKeys.CONFIRM_EMAIL_TEMPLATE);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordReport(int id)
        {
            var result = await _parser.ParseByReportType(ApplicationSettingsKeys.RESET_PASSWORD_TEMPLATE);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestReceipt(int id)
        {
            var result = await _parser.ParseByReportType(ApplicationSettingsKeys.CUSTOMER_RECEIPT_TEMPLATE);

            return Ok(result);
        }
    }
}
