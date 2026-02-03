using CasaAsa.Business.Component.Configuration;
using CasaAsa.Business.Constants;
using CasaAsa.Core.Configuration;
using CasaAsa.Core.Configuration.Template;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportTestController : ControllerBase
    {
        private readonly IHtmlParser _parser;
        private readonly IMailComponent _mailComponent;

        public ReportTestController(IHtmlParser parser, IMailComponent mailComponent)
        {
            _parser = parser;
            _mailComponent = mailComponent;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmUserReport(int id)
        {
            var templateFields = new TemplateFields
            {
                FullName = "Ryan Estandarte",
                Username = "ryan.aimel.estandarte@gmail.com",
                OtherParameters = new Dictionary<string, string>
                {
                    { "ExpirationTime", "3 days" },
                    { "ConfirmationLink", "#" }
                }
            };

            var result = await _parser.ParseByReportTypeAsync(templateFields, ApplicationSettingsKeys.CONFIRM_EMAIL_TEMPLATE);

            // Send the mail here
            var mail = new Mail
            {
                FromEmail = "casa.asa@sarapfoods.com",
                SenderName = "Casa Asa Admin",
                ReceiverName = templateFields.FullName,
                ToEmail = templateFields.Username,
                Subject = "Confirm User",
                Body = result
            };

            _mailComponent.SendMail(mail);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordReport(int id)
        {
            var templateFields = new TemplateFields
            {
                FullName = "Ryan Estandarte",
                Username = "ryan.aimel.estandarte@gmail.com"
            };

            var result = await _parser.ParseByReportTypeAsync(templateFields, ApplicationSettingsKeys.RESET_PASSWORD_TEMPLATE);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestReceipt(int id)
        {
            var templateFields = new TemplateFields
            {
                FullName = "Ryan Estandarte",
                Username = "ryan.aimel.estandarte@gmail.com"
            };

            var result = await _parser.ParseByReportTypeAsync(templateFields, ApplicationSettingsKeys.CUSTOMER_RECEIPT_TEMPLATE);

            return Ok(result);
        }
    }
}
