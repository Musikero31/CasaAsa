using AutoMapper;
using CoreProfiles = CasaAsa.Business.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

namespace CasaAsa.Test.Mappings
{
    public class TestMappingProfiles
    {
        public IMapper Mapper { get; set; }

        public TestMappingProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {                
                cfg.AddProfile<CoreProfiles.AddressMapProfile>();
                cfg.AddProfile<CoreProfiles.CustomerMapProfile>();
                cfg.AddProfile<CoreProfiles.DocumentsMapProfiles>();
                cfg.AddProfile<CoreProfiles.LockSettingMapProfile>();
                cfg.AddProfile<CoreProfiles.MenuMapProfiles>();

            }, NullLoggerFactory.Instance);

            Mapper = config.CreateMapper();
        }
    }
}
