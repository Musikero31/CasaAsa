namespace CasaAsa.Core.BusinessModels
{
    public class MealPlanDetail
    {
        public int Id { get; set; }
        public int MenuOfTheDayId { get; set; }
        public int MenuDetailId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
