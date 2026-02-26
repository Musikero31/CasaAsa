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

            CreateMap<CoreModel.Menu, DataModel.MenuDetail>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.MenuId))
                .ForMember(dest => dest.MenuDescription, source => source.MapFrom(src => src.Description))
                .IgnoreAuditFields()
                .ReverseMap()
                .ForMember(dest => dest.CategoryName, source => source.MapFrom(src => src.MenuCategory.CategoryName));
        }
    }
}
