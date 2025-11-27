namespace CasaAsa.Core.BusinessModels
{
    public class MenuCategories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
