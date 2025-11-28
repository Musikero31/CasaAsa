using AutoMapper;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Profiles
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<CoreModel.UserProfile.Customer, DataModel.ApplicationUser>()
                .ForMember(d => d.UserRoles, s => s.Ignore())
                .ForMember(d => d.Addresses, s => s.Ignore())
                .ForMember(d => d.EmailConfirmed, s => s.Ignore())
                .ForMember(d => d.PhoneNumberConfirmed, s => s.Ignore())
                .ForMember(d => d.AccessFailedCount, s => s.Ignore())
                .ForMember(d => d.ConcurrencyStamp, s => s.Ignore())
                .ForMember(d => d.EmailConfirmed, s => s.Ignore())
                .ForMember(d => d.FullName, s => s.Ignore())
                .ForMember(d => d.LockoutEnabled, s => s.Ignore())
                .ForMember(d => d.LockoutEnd, s => s.Ignore())
                .ForMember(d => d.NormalizedEmail, s => s.MapFrom(src => src.Email))
                .ForMember(d => d.NormalizedUserName, s => s.MapFrom(src => src.Email))
                .ForMember(d => d.PhoneNumberConfirmed, s => s.Ignore())
                .ForMember(d => d.SecurityStamp, s => s.Ignore())
                .ForMember(d => d.UserName, s => s.MapFrom(src => src.Email));
        }
    }
}
