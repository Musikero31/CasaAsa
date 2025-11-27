using CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Authentication
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, IList<string> roles);
    }
}