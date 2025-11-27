using CasaAsa.Data.Models;
using CasaAsa.Data.Repository;

namespace CasaAsa.Business.Component
{
    public class MenuComponent
    {
        private readonly IRepository<MenuDetail> _menuDetailRepository;
        private readonly IRepository<MenuCategory> _menuCategoryRepository;

        public MenuComponent(IRepository<MenuDetail> menuDetailRepository,
                             IRepository<MenuCategory> menuCategoryRepository)
        {
            _menuDetailRepository = menuDetailRepository;
            _menuCategoryRepository = menuCategoryRepository;
        }

        //public async Task<List<MenuCategories>> GetMenuCategoriesAsync()
        //{
        //    return await _menuCategoryRepository.GetAllAsync();
        //}
    }
}
