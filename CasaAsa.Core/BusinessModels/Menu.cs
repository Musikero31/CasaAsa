namespace CasaAsa.Core.BusinessModels
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuCategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public Documents? Photo { get; set; }
    }
}
