using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.Business.Component
{
    public interface IAddressComponent
    {
        Task CreateAddressAsync(Address address, Guid userId);
    }
}