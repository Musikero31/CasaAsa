namespace CasaAsa.Data.Models
{
    public class LockOrder : IAuditEntity
    {
        public int Id { get; set; }
        public DateOnly LockDate { get; set; }
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
