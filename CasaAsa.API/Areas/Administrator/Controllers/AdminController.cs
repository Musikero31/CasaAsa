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
using System.IdentityModel.Tokens.Jwt;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IAdminComponent _adminComponent;
        private readonly IMapper _mapper;
        private readonly IMailComponent _mailComponent;
        private readonly IHtmlParser _htmlParser;

        public AdminController(IAuthenticationService authService,
                               IAdminComponent adminComponent,
                               IMapper mapper,
                               IMailComponent mailComponent,
                               IHtmlParser htmlParser)
        {
            _authService = authService;
            _adminComponent = adminComponent;
            _mapper = mapper;
            _mailComponent = mailComponent;
            _htmlParser = htmlParser;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CustomerViewModel model)
        {
            var register = _mapper.Map<RegisterRequest>(model);
            var result = await _adminComponent.RegisterAsync(register);

            // Retrieve the template
            var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/Admin/Confirm?userId={result.TokenResponse.UserId}&token={result.TokenResponse.Token}";
            var mailParameters = new TemplateFields
            {
                FullName = result.FullName,
                Username = result.TokenResponse.Email,
                OtherParameters = new Dictionary<string, string>
                {
                    { "ExpirationTime", "24 hours" },
                    { "ConfirmationLink", confirmationLink }
                }
            };

            var email = await _htmlParser.ParseByReportTypeAsync(mailParameters, ApplicationSettingsKeys.CONFIRM_EMAIL_TEMPLATE);

            // Send the mail here
            var mail = new Mail
            {
                FromEmail = "casa.asa@sarapfoods.com",
                SenderName = "Casa Asa Admin",
                ReceiverName = result.FullName,
                ToEmail = result.TokenResponse.Email,
                Subject = "Confirm User",
                Body = email
            };

            _mailComponent.SendMail(mail);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(Guid userId, string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);

            if (!result)
            {
                return BadRequest("This confirmation link has expired. Please ask the admin to assist you and request for a new one.");
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] string username)
        {
            var result = await _authService.ResetPassword(username);

            var resetPasswordLink = $"{Request.Scheme}://{Request.Host}/api/Admin/NewPassword?userId={result.TokenResponse.UserId}&token={result.TokenResponse.Token}";

            var mailParameters = new TemplateFields
            {
                FullName = result.FullName,
                Username = result.TokenResponse.Email,
                OtherParameters = new Dictionary<string, string>
                {
                    { "ExpirationTime", "24 hours" },
                    { "ResetPasswordLink", resetPasswordLink }
                }
            };

            var email = await _htmlParser.ParseByReportTypeAsync(mailParameters,
                                                                 ApplicationSettingsKeys.RESET_PASSWORD_TEMPLATE);

            // Send the mail here
            var mail = new Mail
            {
                FromEmail = "casa.asa@sarapfoods.com",
                SenderName = "Casa Asa Admin",
                ReceiverName = result.FullName,
                ToEmail = result.TokenResponse.Email,
                Subject = "Confirm User",
                Body = email
            };

            _mailComponent.SendMail(mail);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NewPassword([FromBody] ResetPassword model)
        {
            var result = await _authService.ResetNewPassword(model.Username, model.ResetPasswordToken, model.NewPassword);

            if (!result)
            {
                return BadRequest("Please ask the admin to assist you and request for a new one.");
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (jti == null || exp == null)
            {
                return BadRequest();
            }

            await _authService.LogoutAsync(jti, exp);

            return Ok("Logged out successfully");
        }
    }
}
