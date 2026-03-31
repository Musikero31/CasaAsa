using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaAsa.Test
{
    public abstract class ComponentTestBase
    {
        public IMapper SetupMapping<TProfile>(ILogger<TProfile> logger)
    where TProfile : Profile
        {
            var profile = (TProfile)Activator.CreateInstance(typeof(TProfile), logger)!;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            });

            return config.CreateMapper();
        }
    }
}
