using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.API.Configuration.Profiles
{
    public class AddressViewModelProfiles : Profile
    {
        public AddressViewModelProfiles()
        {
            CreateMap<AddressViewModel, Address>()
                .ForMember(dest => dest.Postcode, source => source.MapFrom(src => src.ZipCode))
                .ReverseMap();
        }
    }
}
