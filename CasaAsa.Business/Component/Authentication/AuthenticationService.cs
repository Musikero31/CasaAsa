using CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace CasaAsa.Business.Component.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     RoleManager<ApplicationRole> roleManager,
                                     IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request)
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

            // Optionally assign default role
            if (await _roleManager.RoleExistsAsync("Customer"))
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Set token.
            var token = await _jwtTokenService.CreateTokenAsync(user, roles);

            var tokenResponse = new AuthenticationToken
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email ?? "",
                Roles = roles.ToList()
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

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
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
    }
}
