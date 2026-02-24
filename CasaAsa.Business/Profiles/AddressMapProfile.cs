using AutoMapper;
using CasaAsa.Business.Component.Configuration;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Profiles
{
    public class AddressMapProfile : Profile
    {
        public AddressMapProfile()
        {
            CreateMap<CoreModel.UserProfile.Address, DataModel.Address>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.AddressID))
                .IgnoreAuditFields()
                .ReverseMap();
        }
    }
}
