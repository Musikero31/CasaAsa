using AutoMapper;
using CasaAsa.Data.Repository;
using CoreModel = CasaAsa.Core.BusinessModels.UserProfile;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Component
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
    }
}
