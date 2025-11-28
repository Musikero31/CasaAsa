using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(Login request);
        Task<AuthenticationResult> RegisterAsync(RegisterRequest request);
    }
}