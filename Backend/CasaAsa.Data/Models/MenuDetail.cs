using System.Security.Principal;

namespace CasaAsa.Data.Models
{
    public class MenuDetail : IAuditEntity
    {
        public int Id { get; set; }
        public required string MenuName { get; set; }
        public string? MenuDescription { get; set; }
        public decimal Price { get; set; }
        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }
        public bool IsAvailable { get; set; }
        public int? DocumentId { get; set; }
        public Documents? Document { get; set; }

        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }        
    }
}
