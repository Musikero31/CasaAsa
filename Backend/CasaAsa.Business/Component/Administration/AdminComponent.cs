using DataModel = CasaAsa.Data.Models;
using CoreModel = CasaAsa.Core.BusinessModels;
using AutoMapper;
using CasaAsa.Data.Repository;
using Microsoft.Extensions.Logging;

namespace CasaAsa.Business.Component.Administration
{
    public class AdminComponent : IAdminComponent
    {
        private readonly IRepository<DataModel.LockOrder> _lockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminComponent> _logger;

        public AdminComponent(IRepository<DataModel.LockOrder> lockRepository,
                              IMapper mapper,
                              ILogger<AdminComponent> logger)
        {
            _lockRepository = lockRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CoreModel.LockOrder> GetLatestLockOrderAsync()
        {
            var data = await _lockRepository.GetAllAsync();

            var result = data.SingleOrDefault(l => l.ActiveStatus);

            return _mapper.Map<CoreModel.LockOrder>(result);
        }

        public async Task CreateNewLockOrderDateAsync(DateOnly newDate)
        {
            var data = await _lockRepository.GetAllAsync();

            if (data != null && data.ToList().Count > 0)
            {
                _logger.LogDebug("Multiple Active Dates...");

                foreach (var item in data)
                {
                    item.ActiveStatus = false;
                    await _lockRepository.UpdateAsync(item);
                }
            }

            await _lockRepository.AddAsync(new DataModel.LockOrder
            {
                LockDate = newDate,
                CreatedDate = DateTime.Now
            });

            await _lockRepository.SaveChangesAsync();
        }
    }
}