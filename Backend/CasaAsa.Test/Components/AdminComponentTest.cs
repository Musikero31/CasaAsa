using CasaAsa.Data.Repository;
using DataModel = CasaAsa.Data.Models;
using AutoMapper;
using CasaAsa.Business.Component.Administration;
using NSubstitute;
using CasaAsa.Test.Mappings;
using CasaAsa.Business.Component.Administration.Authentication;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace CasaAsa.Test.Components
{
    public class AdminComponentTest
    {
        private readonly IRepository<DataModel.LockOrder> _lockRepo;
        private readonly IMapper _mapper;
        private readonly IAdminComponent _adminComp;
        private readonly IAuthenticationService _authSvc;
        private readonly IAddressComponent _addressComp;
        private readonly ILogger<AdminComponent> _logger;

        public AdminComponentTest()
        {
            _lockRepo = Substitute.For<IRepository<DataModel.LockOrder>>();
            _authSvc = Substitute.For<IAuthenticationService>();
            _addressComp = Substitute.For<IAddressComponent>();
            _logger = Substitute.For<ILogger<AdminComponent>>();

            var mapping = new TestMappingProfiles();
            _mapper = mapping.Mapper;

            _adminComp = new AdminComponent(_lockRepo, _mapper, _authSvc, _addressComp, _logger);
        }

        [Fact]
        public async Task GetLatestLockOrderAsync_Should_Return_Latest_Order_Date()
        {
            // Arrange
            var lockOrders = new List<DataModel.LockOrder>
            {
                new DataModel.LockOrder
                {
                    Id = 3,
                    LockDate = DateOnly.FromDateTime(DateTime.Now),
                    ActiveStatus = true
                },
                new DataModel.LockOrder
                {
                    Id = 1,
                    LockDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2)),
                    ActiveStatus = false
                },
                new DataModel.LockOrder
                {
                    Id = 2,
                    LockDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                    ActiveStatus = false
                }
            };

            _lockRepo.GetAllAsync().Returns(lockOrders);

            // Act
            var result = await _adminComp.GetLatestLockOrderAsync();

            // Assert
            result.Should().NotBeNull();
            result.ActiveStatus.Should().BeTrue();
            result.LockDate.Should().Be(DateOnly.FromDateTime(DateTime.Now));
            
        }
    }
}
