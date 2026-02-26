using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component.Menu
{
    public interface IMenuComponent
    {
        Task CreateMenuDetailAsync(Core.BusinessModels.Menu menu);
        Task CreateNewMenuCategoryAsync(MenuCategories category);
        Task DeleteMenuCategoryAsync(int categoryId);
        Task<List<Core.BusinessModels.Menu>> GetMenuListByCategoryAsync(int categoryId);
        Task<List<MenuCategories>> GetMenuCategoriesAsync();
        Task<Core.BusinessModels.Menu> GetMenuDetailAsync(int menuId);
        Task RemoveMenuDetailAsync(int menuId);
        Task UpdateMenuCategoryAsync(MenuCategories category);
        Task UpdateMenuDetailAsync(Core.BusinessModels.Menu menu);
        Task UpdateMenuAvailabilityStatusAsync(int menuId, bool isAvailable);
    }
}