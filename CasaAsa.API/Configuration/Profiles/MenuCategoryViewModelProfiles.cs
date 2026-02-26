using AutoMapper;
using CasaAsa.API.Areas.Menu.Data;
using CasaAsa.Core.BusinessModels;

namespace CasaAsa.API.Configuration.Profiles
{
    public class MenuCategoryViewModelProfiles : Profile
    {
        public MenuCategoryViewModelProfiles()
        {
            CreateMap<MenuCategoryViewModel, MenuCategories>().ReverseMap();
            CreateMap<MenuViewModel, Menu>().ReverseMap();
        }
    }
}
