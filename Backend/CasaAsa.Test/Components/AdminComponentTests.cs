using CasaAsa.Data.Repository;
using DataModel = CasaAsa.Data.Models;
using CoreModel = CasaAsa.Core.BusinessModels;
using AutoMapper;
using CasaAsa.Business.Component.Administration;
using NSubstitute;
using CasaAsa.Test.Mappings;
using CasaAsa.Business.Component.Administration.Authentication;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace CasaAsa.Test.Components
{
    public class AdminComponentTests
    {
        private readonly IRepository<DataModel.LockOrder> _lockRepo;
        private readonly IMapper _mapper;
        private readonly IAdminComponent _adminComp;
        private readonly IAuthenticationService _authSvc;
        private readonly IAddressComponent _addressComp;
        private readonly ILogger<AdminComponent> _logger;

        public AdminComponentTests()
        {
            _lockRepo = Substitute.For<IRepository<DataModel.LockOrder>>();
            _authSvc = Substitute.For<IAuthenticationService>();
            _addressComp = Substitute.For<IAddressComponent>();
            _logger = Substitute.For<ILogger<AdminComponent>>();

            var mapping = new TestMappingProfiles();
            _mapper = mapping.Mapper;

            _adminComp = new AdminComponent(_lockRepo, _mapper, _authSvc, _addressComp, _logger);
        }

        #region Latest Lock Order Tests
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

        [Fact]
        public async Task GetLatestLockOrderAsync_ShouldReturnNull_WhenNoActiveRecordsExist()
        {
            // Arrange
            var data = new List<DataModel.LockOrder>
            {
                new DataModel.LockOrder
                {
                    ActiveStatus = false
                }
            };

            _lockRepo.GetAllAsync().Returns(data);

            // Act
            var result = await _adminComp.GetLatestLockOrderAsync();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetLatestLockOrderAsync_ShouldReturnNull_WhenNoRecords()
        {
            // Arrange
            var data = new List<DataModel.LockOrder>();

            // Act
            var result = await _adminComp.GetLatestLockOrderAsync();

            // Assert
            result.Should().BeNull();

        }

        [Fact]
        public async Task GetLatestLockOrderAsync_ShouldThrow_WhenMultipleRecords()
        {
            // Arrange
            var data = new List<DataModel.LockOrder>
            {
                new DataModel.LockOrder
                {
                    ActiveStatus = true
                },
                new DataModel.LockOrder
                {
                    ActiveStatus = true
                }
            };

            _lockRepo.GetAllAsync().Returns(data);

            // Act
            Func<Task> result = _adminComp.GetLatestLockOrderAsync;

            // Assert
            await result.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task CreateNewLockOrderDateAsync_ShouldInsert_WhenNoExistingRecords()
        {
            // Arrange
            _lockRepo.GetAllAsync().Returns(new List<DataModel.LockOrder>());

            var newDate = DateOnly.FromDateTime(DateTime.Today);

            // Act
            await _adminComp.CreateNewLockOrderDateAsync(newDate);

            // Assert
            await _lockRepo.Received(1)
                .AddAsync(Arg.Is<DataModel.LockOrder>(x => x.LockDate == newDate && x.CreatedDate != default));

            await _lockRepo.Received(1).SaveChangesAsync();

            await _lockRepo.DidNotReceive().UpdateAsync(Arg.Any<DataModel.LockOrder>());

        }

        [Fact]
        public async Task CreateNewLockOrderDateAsync_ShouldDeactivateAllExistingRecords()
        {
            // Arrange
            var existing = new List<DataModel.LockOrder>
            {
                new DataModel.LockOrder
                {
                    ActiveStatus = true
                },
                new DataModel.LockOrder
                {
                    ActiveStatus = true
                }
            };

            _lockRepo.GetAllAsync().Returns(existing);

            var newDate = DateOnly.FromDateTime(DateTime.Today);

            // Act
            await _adminComp.CreateNewLockOrderDateAsync(newDate);

            // Assert
            existing.ForEach(x => x.ActiveStatus.Should().BeFalse());

            await _lockRepo.Received(existing.Count).UpdateAsync(Arg.Any<DataModel.LockOrder>());
        }

        [Fact]
        public async Task CreateNewLockOrderDateAsync_ShouldInsertNewRecord_WhenExistingRecordsExist()
        {
            // Arrange
            _lockRepo.GetAllAsync().Returns(new List<DataModel.LockOrder> {
                new DataModel.LockOrder()
            });

            var newDate = DateOnly.FromDateTime(DateTime.Today);

            // Act
            await _adminComp.CreateNewLockOrderDateAsync(newDate);

            // Assert
            await _lockRepo.Received(1)
                .AddAsync(Arg.Is<DataModel.LockOrder>(x => x.LockDate == newDate));

            await _lockRepo.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateNewLockOrderDateAsync_ShouldInsert_WhenRepositoryReturnsNull()
        {
            // Arrange
            _lockRepo.GetAllAsync().Returns(Enumerable.Empty<DataModel.LockOrder>());

            var newDate = DateOnly.FromDateTime(DateTime.Today);

            // Act

            await _adminComp.CreateNewLockOrderDateAsync(newDate);

            // Assert
            await _lockRepo.DidNotReceive().UpdateAsync(Arg.Any<DataModel.LockOrder>());
            await _lockRepo.Received(1).AddAsync(Arg.Any<DataModel.LockOrder>());
            await _lockRepo.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateNewLockOrderDateAsync_ShouldSetCreatedDate()
        {
            // Arrange
            _lockRepo.GetAllAsync().Returns(new List<DataModel.LockOrder>());

            var newDate = DateOnly.FromDateTime(DateTime.Today);

            // Act
            await _adminComp.CreateNewLockOrderDateAsync(newDate);

            // Assert
            await _lockRepo.Received(1).AddAsync(Arg.Is<DataModel.LockOrder>(x => x.CreatedDate > DateTime.MinValue));

        }

        #endregion

        #region RegisterAsync Tests

        #endregion
    }
}
