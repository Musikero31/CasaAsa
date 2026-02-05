using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component.Administration.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request);
        Task<bool> ConfirmEmailAsync(Guid userId, string token);
        Task<bool> ResetNewPassword(string username, string token, string newPassword);
        Task<AuthenticationResult> ResetPassword(string username);
    }
}