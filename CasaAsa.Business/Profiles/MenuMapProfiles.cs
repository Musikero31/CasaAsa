using AutoMapper;
using CasaAsa.Business.Component.Configuration;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Profiles
{
    public class MenuMapProfiles : Profile
    {
        public MenuMapProfiles()
        {
            CreateMap<CoreModel.MenuCategories, DataModel.MenuCategory>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.CategoryId))
                .IgnoreAuditFields()
                .ReverseMap();
        }
    }
}
