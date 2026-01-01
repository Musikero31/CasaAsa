using DataModel = CasaAsa.Data.Models;
using CoreModel = CasaAsa.Core.BusinessModels;
using AutoMapper;
using CasaAsa.Data.Repository;
using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.Business.Component
{
    public class AdminComponent : IAdminComponent
    {
        private readonly IRepository<DataModel.LockOrder> _lockRepository;
        private readonly IRepository<DataModel.ApplicationUser> _userRepository;
        private readonly IMapper _mapper;

        public AdminComponent(IRepository<DataModel.LockOrder> lockRepository,
                              IMapper mapper,
                              IRepository<DataModel.ApplicationUser> userRepository)
        {
            _lockRepository = lockRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CoreModel.LockOrder> GetLatestLockOrder()
        {
            var data = await _lockRepository.GetAllAsync();

            var result = data.OrderByDescending(l => l.LockDate).FirstOrDefault(l => l.ActiveStatus);

            return _mapper.Map<CoreModel.LockOrder>(result);
        }

        public async Task CreateNewLockOrderDate(DateOnly newDate)
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
    }
}