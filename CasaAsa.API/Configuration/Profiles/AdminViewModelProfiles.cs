using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using Auth = CasaAsa.Core.BusinessModels.Authentication;
using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.API.Configuration.Profiles
{
    public class AdminViewModelProfiles : Profile
    {
        public AdminViewModelProfiles()
        {
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<CustomerViewModel, Auth.RegisterRequest>();
        }
    }
}
