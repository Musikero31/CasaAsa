namespace CasaAsa.Core.BusinessModels
{
    public class MealPlan
    {
        public int Id { get; set; }
        public DateTime MenuDate { get; set; }
        public DateTime LockDate { get; set; }
        public bool IsActivated { get; set; }
        public List<MealPlanDetail>? MenuOfTheDayDetails { get; set; }
    }
}
