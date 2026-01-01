using CasaAsa.Core.BusinessModels;
using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.Business.Component
{
    public interface IAdminComponent
    {
        Task CreateNewLockOrderDate(DateOnly newDate);
        Task<LockOrder> GetLatestLockOrder();
    }
}