using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component.Menu
{
    public interface IMenuComponent
    {
        Task CreateNewMenuCategoryAsync(MenuCategories category);
        Task DeleteMenuCategoryAsync(int categoryId);
        Task<List<MenuCategories>> GetMenuCategoriesAsync();
        Task UpdateMenuCategoryAsync(MenuCategories category);
    }
}