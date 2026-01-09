using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component.Administration
{
    public interface IAdminComponent
    {
        Task CreateNewLockOrderDate(DateOnly newDate);
        Task<LockOrder> GetLatestLockOrder();
    }
}