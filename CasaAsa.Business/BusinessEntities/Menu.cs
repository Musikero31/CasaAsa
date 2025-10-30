namespace CasaAsa.Business.BusinessEntities
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public byte[]? FileContent { get; set; }
        public int TotalOrders { get; set; }
    }
}
