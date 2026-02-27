using CasaAsa.API.Models;

namespace CasaAsa.API.Areas.Menu.Data
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DocumentViewModel? Photo { get; set; }
    }
}
