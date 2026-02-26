using System.ComponentModel.DataAnnotations;

namespace CasaAsa.Data.Models
{
    public class Documents : IAuditEntity
    {
        public int Id { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentName { get; set; }
        [MaxLength(10)]
        public string DocumentType { get; set; }
        [MaxLength(10)]
        public string FileExtension { get; set; }
        [MaxLength(30)]
        public string MimeType { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
