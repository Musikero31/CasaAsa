namespace CasaAsa.Data.Models
{
    public class MenuCategory : IAuditEntity
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
