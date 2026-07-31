using AutoMapper;
using DataModel = CasaAsa.Data.Entities;
using CoreModel = CasaAsa.Core.BusinessModels;
using CasaAsa.Business.Component.Configuration;

namespace CasaAsa.Business.Profiles
{
    public class LockSettingMapProfile : Profile
    {
        public LockSettingMapProfile()
        {
            CreateMap<DataModel.LockOrder, CoreModel.LockOrder>()
                .ReverseMap()
                .IgnoreAuditFields();
        }
    }
}
