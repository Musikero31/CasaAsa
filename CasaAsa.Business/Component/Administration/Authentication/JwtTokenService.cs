using CasaAsa.Data.Database;
using CasaAsa.Data.Models;
using CasaAsa.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CasaAsa.Business.Component.Administration.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<RevokedToken> _tokenRepo;

        public JwtTokenService(IConfiguration configuration,
                               IRepository<RevokedToken> tokenRepo)
        {
            _configuration = configuration;
            _tokenRepo = tokenRepo;
        }

        public Task<string> CreateTokenAsync(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var signInKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(signInKey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(tokenStr);
        }

        public async Task<bool> IsTokenRevokedAsync(string jti)
        {
            var result = await _tokenRepo.FindAsync(x => x.Jti == jti);

            return result.Any();
        }

        public async Task RevokeByJtiAsync(string jti, DateTime expiryDate)
        {
            await _tokenRepo.AddAsync(new RevokedToken
            {
                Jti = jti,
                ExpiryDate = expiryDate
            });

            await _tokenRepo.SaveChangesAsync();
        }
    }
}
