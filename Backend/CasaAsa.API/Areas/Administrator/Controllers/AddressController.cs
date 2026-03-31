using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Business.Component.Administration;
using CasaAsa.Core.BusinessModels.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAsa.API.Areas.Administrator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressComponent _addressComponent;
        private readonly IMapper _mapper;

        public AddressController(IAddressComponent addressComponent,
                                 IMapper mapper)
        {
            _addressComponent = addressComponent;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetDefaultAddress(Guid userId)
        {
            var result = await _addressComponent.GetDefaultAddressByUser(userId);

            return Ok(_mapper.Map<AddressViewModel>(result));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressViewModel model, Guid userId)
        {
            var address = _mapper.Map<Address>(model);
            await _addressComponent.CreateAddressAsync(address, userId);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressViewModel model, Guid userId)
        {
            var address = _mapper.Map<Address>(model);
            address.UserID = userId;
            await _addressComponent.UpdateAddressAsync(address);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteAddress(int addressId, Guid userId)
        {
            await _addressComponent.RemoveAddressAsync(addressId, userId);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllAddresses(Guid userId)
        {
            var result = await _addressComponent.GetAllAddressesByUserAsync(userId);
            return Ok(result.Select(_mapper.Map<AddressViewModel>).ToList());
        }
    }
}
