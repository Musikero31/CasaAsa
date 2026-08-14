using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component.Administration.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<AuthenticationResult> ConfirmEmailAsync(Guid userId, string token);
        Task<AuthenticationResult> ChangeNewPassword(Guid userId, string token, string newPassword);
        Task<AuthenticationResult> ForgotPassword(string username);
        Task LogoutAsync(string jti, string exp);
        Task<AuthenticationResult> RegisterAsync(RegisterRequest register);
    }
}