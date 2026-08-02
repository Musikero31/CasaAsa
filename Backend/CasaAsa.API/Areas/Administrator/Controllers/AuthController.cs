using AutoMapper;
using CasaAsa.API.Areas.Administrator.Data;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component.Administration.Authentication;
using CasaAsa.Business.Component.Configuration;
using CasaAsa.Business.Constants;
using CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;
        private readonly IMailComponent _mailComponent;
        private readonly IHtmlParser _htmlParser;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticationService authService,
                              IMapper mapper,
                              IMailComponent mailComponent,
                              IHtmlParser htmlParser,
                              IWebHostEnvironment webEnv,
                              IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _mailComponent = mailComponent;
            _htmlParser = htmlParser;
            _webEnv = webEnv;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CustomerViewModel model)
        {
            var register = _mapper.Map<RegisterRequest>(model);
            var result = await _authService.RegisterAsync(register);

            if (!result.Succeeded)
            {
                return BadRequest(new AuthenticationResponse
                {
                    Success = false,
                    Errors = result.Errors
                });
            }

            var response = PrepareLoginResponse(result);

            // Retrieve the template
            var baseUrl = _configuration["Frontend:BaseUrl"];

            var confirmationLink = $"{baseUrl}/confirm?userId={result.TokenResponse!.UserId}&token={result.TokenResponse.Token}";
            var mailParameters = new TemplateFields
            {
                FullName = result.FullName!,
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
                ReceiverName = result.FullName!,
                ToEmail = result.TokenResponse.Email,
                Subject = "Confirm User",
                Body = email
            };

            _mailComponent.SendMail(mail);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);
            var response = PrepareLoginResponse(result);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm([FromBody] ConfirmUserViewModel model)
        {
            if (model.UserId == Guid.Empty || string.IsNullOrEmpty(model.Token))
            {
                return BadRequest(new AuthenticationResponse
                {
                    Success = false,
                    Errors = new List<string> { "User Id or Token is empty" }
                });
            }

            var result = await _authService.ConfirmEmailAsync(model.UserId, model.Token);

            if (!result.success)
            {
                return BadRequest(new AuthenticationResponse
                {
                    Success = result.success,
                    Errors = new List<string>() { result.message}
                });
            }

            return Ok(new AuthenticationResponse
            {
                Success = result.success,
                Message = result.message
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] string username)
        {
            var result = await _authService.ResetPassword(username);

            var resetPasswordLink = $"{Request.Scheme}://{Request.Host}/api/Admin/ChangePassword?userId={result.TokenResponse!.UserId}&token={result.TokenResponse.Token}";

            var mailParameters = new TemplateFields
            {
                FullName = result.FullName!,
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
                ReceiverName = result.FullName!,
                ToEmail = result.TokenResponse.Email,
                Subject = "Confirm User",
                Body = email
            };

            _mailComponent.SendMail(mail);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var result = await _authService.ChangeNewPassword(model.Username, model.ResetPasswordToken, model.NewPassword);

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

            Response.Cookies.Delete("accessToken");
            await _authService.LogoutAsync(jti, exp);

            return Ok("Logged out successfully");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var fullName = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
            var username = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            var roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

            Guid userId = default;

            var subjectClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                   ?? User.FindFirst(JwtRegisteredClaimNames.Sub);

            if (subjectClaim != null && Guid.TryParse(subjectClaim.Value, out Guid parsedUserId))
            {
                userId = parsedUserId;
            }

            return Ok(new AuthenticationResponse
            {
                Success = true,
                UserId = userId,
                FullName = fullName,
                Username = username,
                Roles = roles,
            });
        }

        private AuthenticationResponse PrepareLoginResponse(AuthenticationResult result)
        {
            if (result.Succeeded)
            {
                Response.Cookies.Append("accessToken", result.TokenResponse!.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = _webEnv.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }

            return new AuthenticationResponse
            {
                Success = result.Succeeded,
                UserId = result.TokenResponse?.UserId,
                Username = result.TokenResponse?.Email,
                FullName = result.FullName,
                Roles = result.TokenResponse?.Roles,
                Errors = result.Errors,
            };
        }

    }
}
