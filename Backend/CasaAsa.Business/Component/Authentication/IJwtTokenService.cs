using CasaAsa.Data.Entities;

namespace CasaAsa.Business.Component.Administration.Authentication
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, IList<string> roles);
        Task RevokeByJtiAsync(string jti, DateTime expiryDate);
        Task<bool> IsTokenRevokedAsync(string jti);
    }
}