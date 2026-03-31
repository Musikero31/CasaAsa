namespace CasaAsa.Data.Models
{
    public class RevokedToken : IAuditEntity
    {
        public int Id { get; set; }

        public string Jti { get; set; } = default!;

        public DateTime ExpiryDate { get; set; }

        public DateTime RevokedAt { get; set; } = DateTime.UtcNow;
        public bool ActiveStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
