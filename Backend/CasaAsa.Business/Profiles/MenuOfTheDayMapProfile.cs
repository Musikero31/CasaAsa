using AutoMapper;
using CasaAsa.Business.Component.Configuration;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Entities;

namespace CasaAsa.Business.Profiles
{
    public class MenuOfTheDayMapProfile : Profile
    {
        public MenuOfTheDayMapProfile()
        {
            CreateMap<CoreModel.MealPlan, DataModel.MenuOfTheDay>()
                .IgnoreAuditFields()
                .ReverseMap();

            CreateMap<CoreModel.MealPlanDetail, DataModel.MenuOfTheDayDetail>()
                .IgnoreAuditFields()
                .ReverseMap();
        }
    }
}
