using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component.MenuOfTheDay
{
    public interface IMenuOfTheDayComponent
    {
        Task ActivateMenuOfTheDay(int menuOfTheDayId);
        Task CreateNewMenuOfTheDay(MealPlan mealPlan);
        Task<List<MealPlan>> GetMealPlansByCategory(int category);
        Task UpdateNewMenuOfTheDay(MealPlan mealPlan);
    }
}