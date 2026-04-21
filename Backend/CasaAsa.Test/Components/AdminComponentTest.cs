using AutoMapper;
using CasaAsa.Business.Component.Administration;
using CasaAsa.Business.Component.Administration.Authentication;
using CasaAsa.Data.Repository;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaAsa.Test.Components
{
    public class AdminComponentTest
    {
        private readonly Mock<IRepository<Data.Models.LockOrder>> _lockOrderRepoMock;
        private readonly AdminComponent _adminCompMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAuthenticationService> _authSvcMock;
        private readonly Mock<IAddressComponent> _addressCompMock;
        private readonly Mock<ILogger<AdminComponent>> _loggerAdminCompMock;

        public AdminComponentTest()
        {
            _lockOrderRepoMock = new Mock<IRepository<Data.Models.LockOrder>>();
            _mapperMock = new Mock<IMapper>();
            _authSvcMock = new Mock<IAuthenticationService>();
            _addressCompMock = new Mock<IAddressComponent>();
            _loggerAdminCompMock = new Mock<ILogger<AdminComponent>>();

            _adminCompMock = new AdminComponent(_lockOrderRepoMock.Object,
                                                _mapperMock.Object,
                                                _authSvcMock.Object,
                                                _addressCompMock.Object,
                                                _loggerAdminCompMock.Object);

        }
    }
}
