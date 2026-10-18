using AutoMapper;
using CasaAsa.Data.Repository;
using Microsoft.Extensions.Logging;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Entities;

namespace CasaAsa.Business.Component.MenuOfTheDay
{
    public class MenuOfTheDayComponent : IMenuOfTheDayComponent
    {
        private readonly IRepository<DataModel.MenuOfTheDay> _mealPlanRepo;
        private readonly IRepository<DataModel.MenuOfTheDayDetail> _mealPlanDetailRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuOfTheDayComponent> _logger;

        public MenuOfTheDayComponent(IRepository<DataModel.MenuOfTheDay> mealPlanRepo,
                                     IRepository<DataModel.MenuOfTheDayDetail> mealPlanDetailRepo,
                                     IMapper mapper,
                                     ILogger<MenuOfTheDayComponent> logger)
        {
            _mealPlanRepo = mealPlanRepo;
            _mealPlanDetailRepo = mealPlanDetailRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CoreModel.MealPlan>> GetMealPlansByCategory(int category)
        {
            return null;
        }

        public async Task CreateNewMenuOfTheDay(CoreModel.MealPlan mealPlan)
        {

        }

        public async Task UpdateNewMenuOfTheDay(CoreModel.MealPlan mealPlan)
        {

        }

        public async Task ActivateMenuOfTheDay(int menuOfTheDayId)
        {

        }
    }
}
