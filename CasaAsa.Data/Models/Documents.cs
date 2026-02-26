namespace CasaAsa.Data.Models
{
    public class Documents : IAuditEntity
    {
        public int Id { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string FileType { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
