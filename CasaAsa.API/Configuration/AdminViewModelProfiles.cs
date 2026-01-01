using AutoMapper;
using CasaAsa.API.Areas.Administrator.Models;
using CasaAsa.Core.BusinessModels.UserProfile;

namespace CasaAsa.API.Configuration
{
    public class AdminViewModelProfiles : Profile
    {
        public AdminViewModelProfiles()
        {
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}
