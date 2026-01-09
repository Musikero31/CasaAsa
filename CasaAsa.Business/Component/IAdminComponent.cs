using CasaAsa.Core.BusinessModels;
using CasaAsa.Core.BusinessModels.Authentication;

namespace CasaAsa.Business.Component
{
    public interface IAdminComponent
    {
        Task CreateNewLockOrderDateAsync(DateOnly newDate);
        Task<LockOrder> GetLatestLockOrderAsync();

        Task<AuthenticationResult> RegisterAsync(RegisterRequest register);
    }
}