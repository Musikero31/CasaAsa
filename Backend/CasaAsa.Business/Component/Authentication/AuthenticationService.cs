using CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text;

namespace CasaAsa.Business.Component.Administration.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAddressComponent _addressComponent;


        public AuthenticationService(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     RoleManager<ApplicationRole> roleManager,
                                     IJwtTokenService jwtTokenService,
                                     ILogger<AuthenticationService> logger,
                                     IAddressComponent addressComponent)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
            _addressComponent = addressComponent;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterRequest register)
        {
            var result = await RegisterUserAsync(register);

            if (result.TokenResponse == null)
            {
                return result;
            }

            foreach (var address in register.Addresses)
            {
                if (address.ContactIsSameAsUser)
                {
                    address.ContactPerson = register.FirstName + " " + register.LastName;
                    address.ContactNumber = register.PhoneNumber;
                }

                await _addressComponent.CreateAddressAsync(address, result.TokenResponse.UserId);
            }

            _logger.LogInformation($"User {register.Email} has been registered.");

            return result;
        }
        private async Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing != null)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = ["Email already registered."]
                };
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(ex => ex.Description).ToList(),
                };
            }

            if (await _roleManager.RoleExistsAsync("Customer"))
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            // Set token.
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var tokenResponse = new AuthenticationToken
            {
                Token = encodedToken,
                UserId = user.Id,
                Email = user.Email ?? ""
            };

            return new AuthenticationResult
            {
                TokenResponse = tokenResponse,
                Succeeded = true,
                Errors = [],
                FullName = user.FullName,
            };
        }

        public async Task<AuthenticationResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = ["Invalid credentials."]
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: true);
            if (!signInResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = ["Invalid credentials."]
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _jwtTokenService.CreateTokenAsync(user, roles);

            var response = new AuthenticationToken
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email ?? "",
                Roles = roles.ToList()
            };

            return new AuthenticationResult
            {
                Succeeded = true,
                TokenResponse = response,
                Errors = [],
                FullName = user.FullName,
            };
        }

        public async Task<(bool success, string message)> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                _logger.LogError("User not found. UserId: {userId}", userId.ToString());
                return (success: false, message: "User not found.");
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogError("Confirmation error: {ErrorDescriptions}", errorDescriptions);

                return (success: false, message: $"Confirmation error: {errorDescriptions}");
            }

            _logger.LogInformation("User {UserName} is confirmed.", user.UserName);

            return (success: true, message: "Welcome to Casa Asa. You can now login.");
        }

        public async Task<(bool success, string message)> ChangeNewPassword(Guid userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                _logger.LogError("User not found. UserId: {userId}", userId.ToString());
                return (success: false, message: "User not found.");
            }

            // Check if the new password is the same as the one in the table
            var isSamePassword = await _userManager.CheckPasswordAsync(user, newPassword);

            if (isSamePassword)
            {
                _logger.LogError("New password is the same as the current password.");
                return (success: false, message: "New password is the same as the current password.");
            }

            string decodedToken;

            try
            {
                decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "The password reset link is invalid or has expired.");

                return (success: false, message: "The password reset link is invalid or has expired.");
            }

            // Update access failed count and access failed count
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogError("Change password error: {ErrorDescriptions}", errorDescriptions);

                return (success: false, message: $"Change password errors {errorDescriptions}");
            }

            _logger.LogInformation("User {Username} has changed password.", user.UserName);

            return (success: result.Succeeded, message: $"User {user.UserName} has changed password.");
        }

        public async Task<AuthenticationResult> ForgotPassword(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                _logger.LogWarning("Password reset requested for unknown username.");

                return new AuthenticationResult
                {
                    Succeeded = true,
                    Errors = []
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            var tokenResponse = new AuthenticationToken
            {
                Token = encodedToken,
                UserId = user.Id,
                Email = user.Email ?? ""
            };

            return new AuthenticationResult
            {
                TokenResponse = tokenResponse,
                Succeeded = true,
                Errors = [],
                FullName = user.FullName
            };
        }

        public async Task LogoutAsync(string jti, string exp)
        {
            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;

            await _jwtTokenService.RevokeByJtiAsync(jti, expiryDate);
        }
    }
}
