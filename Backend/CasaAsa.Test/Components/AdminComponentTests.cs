using AutoMapper;
using CasaAsa.Data.Repository;
using DataModel = CasaAsa.Data.Models;
using Moq;
using Microsoft.Extensions.Logging;
using CasaAsa.Business.Component.Administration;

namespace CasaAsa.Test.Components
{
    public class AdminComponentTests
    {
        private readonly Mock<IRepository<DataModel.LockOrder>> _mockRepo;
        private readonly IMapper _mapper;
        private readonly IAdminComponent _component;
        private readonly ILoggerFactory _logger;

        public AdminComponentTests()
        {
            //// 1. Mock repository
            //_mockRepo = new Mock<IRepository<DataModel.LockOrder>>();

            //// 2. Configure AutoMapper with your test profile
            //var mockMapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<LockSettingProfile>();
            //}, _logger).CreateMapper();
            //// 3. Instantiate component under test
            //_component = new AdminComponent(_mockRepo.Object, _mapper!);
            //_component = new AdminComponent(_mockRepo.Object, _mapper!);
        }

        [Fact]
        public void GetLatestLockOrder_should_return_latest_lock_date()
        {

        }
    }
}