using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component
{
    public interface IAdminComponent
    {
        Task CreateNewLockOrderDate(DateOnly newDate);
        Task<LockOrder> GetLatestLockOrder();
    }
}