using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request);
    }
}