using AutoMapper;
using CasaAsa.Data.Repository;
using CoreModel = CasaAsa.Core.BusinessModels.UserProfile;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Administration
{
    public class AddressComponent : IAddressComponent
    {
        private readonly IRepository<DataModel.Address> _addressRepository;
        private readonly IMapper _mapper;

        public AddressComponent(IRepository<DataModel.Address> addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task CreateAddressAsync(CoreModel.Address address, Guid userId)
        {
            var dataAddress = _mapper.Map<DataModel.Address>(address);
            dataAddress.UserId = userId;
            await _addressRepository.AddAsync(dataAddress);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task<List<CoreModel.Address>> GetAllAddressesByUserAsync(Guid userId)
        {
            var dataAddresses = await _addressRepository
                .FindAsync(x => x.UserId == userId);

            return dataAddresses.Select(_mapper.Map<CoreModel.Address>).ToList();
        }

        public async Task<CoreModel.Address> GetDefaultAddressByUser(Guid userId)
        {
            var dataAddress = await _addressRepository
                .FindAsync(x => x.UserId == userId && x.ActiveStatus && x.IsDefaultAddress);

            return _mapper.Map<CoreModel.Address>(dataAddress.FirstOrDefault());
        }

        public async Task RemoveAddressAsync(int addressId, Guid userId)
        {
            var data = (await _addressRepository
                .FindAsync(x => x.Id == addressId && x.UserId == userId && x.ActiveStatus)).FirstOrDefault();

            data!.ActiveStatus = false;

            await _addressRepository.RemoveAsync(data, false);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(CoreModel.Address address)
        {
            var data = await _addressRepository
                .FindAsync(x => x.Id == address.AddressID && x.UserId == address.UserID && x.ActiveStatus);

            var dataAddress = data.FirstOrDefault();

            var result = _mapper.Map<CoreModel.Address, DataModel.Address>(address, dataAddress);

            await _addressRepository.UpdateAsync(result);

            await _addressRepository.SaveChangesAsync();
        }
    }
}
