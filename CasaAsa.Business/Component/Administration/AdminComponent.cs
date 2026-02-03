using DataModel = CasaAsa.Data.Models;
using CoreModel = CasaAsa.Core.BusinessModels;
using AutoMapper;
using CasaAsa.Data.Repository;
using CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Business.Component.Administration.Authentication;
using Microsoft.Extensions.Logging;

namespace CasaAsa.Business.Component.Administration
{
    public class AdminComponent : IAdminComponent
    {
        private readonly IRepository<DataModel.LockOrder> _lockRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authService;
        private readonly IAddressComponent _addressComponent;
        private readonly ILogger<AdminComponent> _logger;

        public AdminComponent(IRepository<DataModel.LockOrder> lockRepository,
                              IMapper mapper,
                              IAuthenticationService authService,
                              IAddressComponent addressComponent,
                              ILogger<AdminComponent> logger)
        {
            _lockRepository = lockRepository;
            _mapper = mapper;
            _authService = authService;
            _addressComponent = addressComponent;
            _logger = logger;
        }

        public async Task<CoreModel.LockOrder> GetLatestLockOrderAsync()
        {
            var data = await _lockRepository.GetAllAsync();

            var result = data.OrderByDescending(l => l.LockDate).FirstOrDefault(l => l.ActiveStatus);

            return _mapper.Map<CoreModel.LockOrder>(result);
        }

        public async Task CreateNewLockOrderDateAsync(DateOnly newDate)
        {
            var data = await _lockRepository.GetAllAsync();

            if (data != null && data.ToList().Count > 0)
            {
                var oldData = data.OrderByDescending(l => l.LockDate).FirstOrDefault(l => l.ActiveStatus);
                oldData!.ActiveStatus = false;
                _lockRepository.Update(oldData);
            }

            await _lockRepository.AddAsync(new DataModel.LockOrder
            {
                ActiveStatus = true,
                LockDate = newDate,
                CreatedDate = DateTime.Now
            });

            await _lockRepository.SaveChangesAsync();
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterRequest register)
        {
            var result = await _authService.RegisterUserAsync(register);

            foreach (var address in register.Addresses)
            {
                if (address.ContactIsSameAsUser)
                {
                    address.ContactPerson = register.FirstName + " " + register.LastName;
                    address.ContactNumber = register.PhoneNumber;
                }

                await _addressComponent.CreateAddressAsync(address, result.TokenResponse.UserId);                
            }

            _logger.LogInformation($"User {register.Email} has been registered.");

            return result;
        }
    }
}