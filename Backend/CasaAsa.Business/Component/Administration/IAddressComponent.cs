using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.Business.Component.Administration
{
    public interface IAddressComponent
    {
        Task CreateAddressAsync(Address address, Guid userId);
        Task<Address> GetDefaultAddressByUser(Guid userId);
        Task UpdateAddressAsync(Address address);
        Task<List<Address>> GetAllAddressesByUserAsync(Guid userId);
        Task RemoveAddressAsync(int addressId, Guid userId);
    }
}