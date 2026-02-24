using AutoMapper;
using CasaAsa.Data.Repository;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Menu
{
    public class MenuComponent : IMenuComponent
    {
        private readonly IRepository<DataModel.MenuCategory> _repository;
        private readonly IMapper _mapper;

        public MenuComponent(IRepository<DataModel.MenuCategory> repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CoreModel.MenuCategories>> GetMenuCategoriesAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Where(x => x.ActiveStatus)
                       .Select(_mapper.Map<CoreModel.MenuCategories>)
                       .ToList();
        }

        public async Task CreateNewMenuCategoryAsync(CoreModel.MenuCategories category)
        {
            var data = _mapper.Map<DataModel.MenuCategory>(category);

            await _repository.AddAsync(data);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateMenuCategoryAsync(CoreModel.MenuCategories category)
        {
            var data = (await _repository.FindAsync(x => x.Id == category.CategoryId && x.ActiveStatus))
                .FirstOrDefault();

            var result = _mapper.Map(category, data!);

            await Task.Run(() => _repository.Update(result));

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteMenuCategoryAsync(int categoryId)
        {
            var data = (await _repository.FindAsync(x => x.Id == categoryId && x.ActiveStatus))
                .FirstOrDefault();

            data!.ActiveStatus = false;

            await Task.Run(() => _repository.Remove(data, false));
            await _repository.SaveChangesAsync();
        }
    }
}
