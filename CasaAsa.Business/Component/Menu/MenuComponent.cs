using AutoMapper;
using CasaAsa.Data.Repository;
using Microsoft.Extensions.Logging;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Menu
{
    public class MenuComponent : IMenuComponent
    {
        private readonly IRepository<DataModel.MenuCategory> _menuCategoryRepo;
        private readonly IRepository<DataModel.MenuDetail> _menuRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuComponent> _logger;

        public MenuComponent(IRepository<DataModel.MenuCategory> repository,
                             IMapper mapper,
                             IRepository<DataModel.MenuDetail> menuRepo,
                             ILogger<MenuComponent> logger)
        {
            _menuCategoryRepo = repository;
            _mapper = mapper;
            _menuRepo = menuRepo;
            _logger = logger;
        }

        public async Task<List<CoreModel.MenuCategories>> GetMenuCategoriesAsync()
        {
            var data = await _menuCategoryRepo.GetAllAsync();

            return data.Where(x => x.ActiveStatus)
                       .Select(_mapper.Map<CoreModel.MenuCategories>)
                       .ToList();
        }

        public async Task CreateNewMenuCategoryAsync(CoreModel.MenuCategories category)
        {
            var data = _mapper.Map<DataModel.MenuCategory>(category);

            await _menuCategoryRepo.AddAsync(data);
            await _menuCategoryRepo.SaveChangesAsync();
        }

        public async Task UpdateMenuCategoryAsync(CoreModel.MenuCategories category)
        {
            var data = (await _menuCategoryRepo.FindAsync(x => x.Id == category.CategoryId && x.ActiveStatus))
                .FirstOrDefault();

            var result = _mapper.Map(category, data!);

            await _menuCategoryRepo.UpdateAsync(result);

            await _menuCategoryRepo.SaveChangesAsync();
        }

        public async Task DeleteMenuCategoryAsync(int categoryId)
        {
            var data = (await _menuCategoryRepo.FindAsync(x => x.Id == categoryId && x.ActiveStatus))
                .FirstOrDefault();

            data!.ActiveStatus = false;

            await _menuCategoryRepo.RemoveAsync(data, false);
            await _menuCategoryRepo.SaveChangesAsync();
        }

        public async Task<List<CoreModel.Menu>> GetMenuListByCategoryAsync(int categoryId)
        {
            var category = await _menuCategoryRepo.FindAsync(x => x.Id == categoryId && x.ActiveStatus);
            var data = await _menuRepo.FindAsync(x => x.MenuCategoryId == categoryId && x.ActiveStatus);

            var result = data.Select(_mapper.Map<CoreModel.Menu>).ToList();
            result.ForEach(x => x.CategoryName = category.FirstOrDefault()!.CategoryName);

            return result;
        }

        public async Task<CoreModel.Menu> GetMenuDetailAsync(int menuId)
        {
            var result = await _menuRepo.GetByIdAsync(menuId);

            return _mapper.Map<CoreModel.Menu>(result);
        }

        public async Task CreateMenuDetailAsync(CoreModel.Menu menu)
        {
            var data = _mapper.Map<DataModel.MenuDetail>(menu);
            
            await _menuRepo.AddAsync(data);
            await _menuRepo.SaveChangesAsync();
        }

        public async Task UpdateMenuDetailAsync(CoreModel.Menu menu)
        {
            var data = await _menuRepo.GetByIdAsync(menu.MenuId) 
                ?? throw new ArgumentNullException($"Menu id {menu.MenuId} not found.");

            var result = _mapper.Map(menu, data);

            await _menuRepo.UpdateAsync(result);
            await _menuRepo.SaveChangesAsync();
        }

        public async Task RemoveMenuDetailAsync(int menuId)
        {
            var data = await _menuRepo.GetByIdAsync(menuId)
                ?? throw new ArgumentNullException($"Menu id {menuId} not found.");

            await _menuRepo.RemoveAsync(data, false);
            await _menuRepo.SaveChangesAsync();

            _logger.LogInformation($"Menu id {menuId} has been deleted");
        }

        public async Task UpdateMenuAvailabilityStatusAsync(int menuId, bool isAvailable)
        {
            var data = await _menuRepo.GetByIdAsync(menuId)
                ?? throw new ArgumentNullException($"Menu id {menuId} not found.");

            data.IsAvailable = isAvailable;

            await _menuRepo.UpdateAsync(data);
            await _menuRepo.SaveChangesAsync();

            var status = isAvailable ? "Available" : "Sold-out";

            _logger.LogInformation($"Changed menu {data.MenuName} to {status}");
        }
    }
}
