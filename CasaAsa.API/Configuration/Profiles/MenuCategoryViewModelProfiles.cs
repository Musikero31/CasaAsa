using AutoMapper;
using CasaAsa.API.Areas.Menu.Data;
using CasaAsa.API.Areas.Menu.Models;
using CasaAsa.Core.BusinessModels;
using CasaAsa.Data.Models;

namespace CasaAsa.API.Configuration.Profiles
{
    public class MenuCategoryViewModelProfiles : Profile
    {
        public MenuCategoryViewModelProfiles()
        {
            CreateMap<MenuCategoryRequest, MenuCategory>().ReverseMap();
            CreateMap<MenuCategoryViewModel, MenuCategories>().ReverseMap();
            CreateMap<MenuViewModel, Menu>().ReverseMap();
        }
    }
}
